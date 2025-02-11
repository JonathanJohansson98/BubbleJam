using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Required for TextMeshPro

public class GameUI : MonoBehaviour
{
    // --- Fan/Visual Settings ---
    [SerializeField] Image fanImage;
    [SerializeField] Image airStreaksImage;
    public FanScript fanScript;
    [SerializeField] Sprite fanOFF;
    [SerializeField] Sprite fanON1;
    [SerializeField] Sprite fanON2;
    [SerializeField] Sprite airStreaks1;
    [SerializeField] Sprite airStreaks2;

    // --- Audio Settings ---
    [Header("Audio")]
    [SerializeField] private AudioSource fanAudioSource;
    [SerializeField] private AudioClip fanClick;  // Click sound when turning on
    [SerializeField] private AudioClip fanLoop;   // Looping fan sound
    [SerializeField] private AudioClip fanOff;    // Sound when turning off

    private bool isSwitchingSprites = false;
    private bool isFanRunning = false;
    private Coroutine spriteSwitchCoroutine = null;  // Reference for the fan sprite coroutine

    // --- Restart Text Settings ---
    [Header("Restart Text Settings")]
    [SerializeField] private TextMeshProUGUI restartText; // "Press R to restart" text
    [SerializeField] private float restartTextFadeInDelay = 16f;      // Time to wait before beginning the fade in
    [SerializeField] private float restartTextFadeInDuration = 2f;      // Duration over which the text fades in
    [SerializeField] private float restartTextVisibleDuration = 8f;     // Time the text remains fully visible
    [SerializeField] private float restartTextFadeOutDuration = 2f;     // Duration over which the text fades out

    // Store the coroutine reference so it can be cancelled
    private Coroutine restartTextFadeCoroutine = null;

    private void Start()
    {
        // Determine the platform and set the restart text accordingly.
        if (Application.platform == RuntimePlatform.Android)
        {
            restartText.text = "Tap 'BACK' to pause";
        }
        else
        {
            restartText.text = "Press 'R' to restart";
        }

        // Ensure the restart text starts invisible
        if (restartText != null)
        {
            Color c = restartText.color;
            c.a = 0f;
            restartText.color = c;
            restartTextFadeCoroutine = StartCoroutine(FadeRestartText());
        }
    }


    private void Update()
    {
        if (fanScript.playerAlive)
        {
            fanImage.enabled = true;
            fanImage.transform.position = Input.mousePosition;

            if (Input.GetMouseButton(0))
            {
                if (!isSwitchingSprites)
                {
                    // Start only the fan sprite coroutine and keep its reference
                    spriteSwitchCoroutine = StartCoroutine(SwitchSprites());
                    PlayFanSound();
                }
            }
            else
            {
                // Stop only the fan sprite coroutine instead of all coroutines
                if (spriteSwitchCoroutine != null)
                {
                    StopCoroutine(spriteSwitchCoroutine);
                    spriteSwitchCoroutine = null;
                }
                isSwitchingSprites = false;
                fanImage.sprite = fanOFF;
                airStreaksImage.enabled = false;
                StopFanSound();
            }

            // Rotate the fan to face the center of the screen
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Vector3 direction = screenCenter - fanImage.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            fanImage.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
        else
        {
            fanImage.enabled = false;
            airStreaksImage.enabled = false;
            StopFanSound();
        }
    }

    private IEnumerator SwitchSprites()
    {
        isSwitchingSprites = true;
        airStreaksImage.enabled = true;

        while (true)
        {
            fanImage.sprite = fanON1;
            airStreaksImage.sprite = airStreaks1;
            yield return new WaitForSeconds(0.1f);
            fanImage.sprite = fanON2;
            airStreaksImage.sprite = airStreaks2;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void PlayFanSound()
    {
        if (!isFanRunning)
        {
            isFanRunning = true;
            fanAudioSource.Stop(); // Ensure any currently playing audio is stopped
            fanAudioSource.PlayOneShot(fanClick); // Play the click sound

            // After the click sound, start the looping sound.
            Invoke(nameof(StartLoopingSound), fanClick.length);
        }
    }

    private void StartLoopingSound()
    {
        if (isFanRunning)
        {
            fanAudioSource.clip = fanLoop;
            fanAudioSource.loop = true;
            fanAudioSource.Play();
        }
    }

    private void StopFanSound()
    {
        if (isFanRunning)
        {
            isFanRunning = false;
            fanAudioSource.Stop();
            fanAudioSource.PlayOneShot(fanOff); // Play the turn-off sound
        }
    }

    private IEnumerator FadeRestartText()
    {
        // Wait for the specified delay before beginning the fade in.
        yield return new WaitForSeconds(restartTextFadeInDelay);

        // Fade in: gradually increase the alpha from 0 to 1.
        float timer = 0f;
        while (timer < restartTextFadeInDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / restartTextFadeInDuration);
            if (restartText != null)
            {
                Color c = restartText.color;
                c.a = alpha;
                restartText.color = c;
            }
            yield return null;
        }

        // Ensure the text is fully visible.
        if (restartText != null)
        {
            Color c = restartText.color;
            c.a = 1f;
            restartText.color = c;
        }

        // Wait while the text remains fully visible.
        yield return new WaitForSeconds(restartTextVisibleDuration);

        // Fade out: gradually decrease the alpha from 1 to 0.
        timer = 0f;
        while (timer < restartTextFadeOutDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / restartTextFadeOutDuration);
            if (restartText != null)
            {
                Color c = restartText.color;
                c.a = alpha;
                restartText.color = c;
            }
            yield return null;
        }

        // Ensure the text is completely transparent.
        if (restartText != null)
        {
            Color c = restartText.color;
            c.a = 0f;
            restartText.color = c;
        }
    }

    public void DisableRestartTextLayer()
    {
        // Stop the fade coroutine if it's running
        if (restartTextFadeCoroutine != null)
        {
            StopCoroutine(restartTextFadeCoroutine);
            restartTextFadeCoroutine = null;
        }
        
        restartText.gameObject.SetActive(false);
    }
}
