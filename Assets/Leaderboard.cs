using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public float score;
}

public class Leaderboard : MonoBehaviour
{
    public List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();
    public Transform leaderBoardRoot;
    public LeaderboardEntryUI leaderboardEntry;
    public TextMeshProUGUI playerScore;

    private void Awake()
    {
        playerScore.text = "Your Time: " + PlayerPrefs.GetFloat("Score");
    }

    [ContextMenu("Get")]
    public void SetupLeaderBoardFromPlayerprefs()
    {
        GetFromPlayerPrefs();

        Instantiate<LeaderboardEntryUI>(leaderboardEntry, leaderBoardRoot).SetupTitleEntry();

        foreach (var item in leaderboardEntries.OrderBy(p=>p.score).Take(6))
        {
            Instantiate<LeaderboardEntryUI>(leaderboardEntry, leaderBoardRoot).SetupEntry(item);
        }
    }

    private void GetFromPlayerPrefs()
    {
        string json = PlayerPrefs.GetString("Leaderboard", "{}");
        leaderboardEntries = JsonUtility.FromJson<LeaderboardList>(json).entries;
    }

    [ContextMenu("Save")]
    public void SaveLeaderBoardToPlayerprefs()
    {
        LeaderboardList leaderboardList = new LeaderboardList { entries = leaderboardEntries };
        string json = JsonUtility.ToJson(leaderboardList);
        PlayerPrefs.SetString("Leaderboard", json);
        PlayerPrefs.Save();
    }

    public void SaveFromInput(TMP_InputField inputField) 
    {
        GetFromPlayerPrefs();

        leaderboardEntries.Add(new LeaderboardEntry() {playerName = inputField.text, score = PlayerPrefs.GetFloat("Score")});

        SaveLeaderBoardToPlayerprefs();
    }

    [System.Serializable]
    private class LeaderboardList
    {
        public List<LeaderboardEntry> entries;
    }
}
