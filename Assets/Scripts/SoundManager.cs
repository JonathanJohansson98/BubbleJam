using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //serialize stuff edit prefab to add sounds
    [System.Serializable]
    public class SoundCategory
    {
        public string categoryName;
        public List<AudioClip> audioClips = new List<AudioClip>();
    }

    public static SoundManager Instance;

    [Header("Sound Settings")]
    public List<SoundCategory> soundCategories = new List<SoundCategory>();
    public AudioSource audioSource;

    private Dictionary<string, List<int>> usedIndices = new Dictionary<string, List<int>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LoadUsedIndices();
    }

    private void Start()
    {
        //play first song on start game
        //PlaySound("Music", 0);

    }

    public void PlayRandomSound(string category)
    {
        //commentary sounds are played using random sound implementation
        if (!audioSource) return;

        SoundCategory soundCategory = soundCategories.Find(sc => sc.categoryName == category);
        if (soundCategory == null || soundCategory.audioClips.Count == 0)
        {
            Debug.LogWarning($"Category '{category}' not found or empty.");
            return;
        }

        if (!usedIndices.ContainsKey(category))
            usedIndices[category] = new List<int>();

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < soundCategory.audioClips.Count; i++)
        {
            if (!usedIndices[category].Contains(i))
                availableIndices.Add(i);
        }

        if (availableIndices.Count == 0)
        {
            usedIndices[category].Clear();
            availableIndices.AddRange(new List<int>(Enumerable.Range(0, soundCategory.audioClips.Count)));
        }

        int randomIndex = availableIndices[Random.Range(0, availableIndices.Count)];
        usedIndices[category].Add(randomIndex);

        audioSource.PlayOneShot(soundCategory.audioClips[randomIndex]);

        SaveUsedIndices();
    }

    public void PlaySound (string category, int id)
    {
        //Some sounds are played without random sound implementation
        if (!audioSource) return;

        SoundCategory soundCategory = soundCategories.Find(sc => sc.categoryName == category);
        if (soundCategory == null || soundCategory.audioClips.Count == 0)
        {
            Debug.LogWarning($"Category '{category}' not found or empty.");
            return;
        }

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < soundCategory.audioClips.Count; i++)
        {
            availableIndices.Add(i);
        }

        audioSource.PlayOneShot(soundCategory.audioClips[id]);

        Debug.Log("Playing sound: " + soundCategory.audioClips[id]);
    }

    private void SaveUsedIndices()
    {
        //save to playerprefs what sounds have already been used
        foreach (var category in usedIndices)
        {
            PlayerPrefs.SetString($"SoundManager_{category.Key}", string.Join(",", category.Value));
        }
        PlayerPrefs.Save();
    }

    private void LoadUsedIndices()
    {
        //load available sounds and clear saved sounds if no loads are available
        foreach (var category in soundCategories)
        {
            string key = $"SoundManager_{category.categoryName}";
            if (PlayerPrefs.HasKey(key))
            {
                string[] indices = PlayerPrefs.GetString(key).Split(',');
                List<int> indexList = new List<int>();
                foreach (var index in indices)
                {
                    if (int.TryParse(index, out int result))
                        indexList.Add(result);
                }
                usedIndices[category.categoryName] = indexList;
            }
        }
    }

    public void StopAllSound()
    {
        //stop all sound
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
