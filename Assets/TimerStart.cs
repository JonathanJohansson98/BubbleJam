using System.Collections;
using UnityEngine;
using TMPro;

public class TimerStart : MonoBehaviour
{
    public GameObject Timer;
    public float fadeDuration = 1.0f;
    public float startAlpha = 0f;
    public float endAlpha = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!Timer.activeSelf)
            {
                Timer.SetActive(true);
                TMP_Text textComponent = Timer.GetComponentInChildren<TMP_Text>();
                if (textComponent != null)
                {
                    Color c = textComponent.color;
                    c.a = startAlpha;
                    textComponent.color = c;
                    StartCoroutine(FadeTextIn(textComponent, fadeDuration));
                }
            }
        }
    }

    IEnumerator FadeTextIn(TMP_Text textComponent, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            Color c = textComponent.color;
            c.a = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            textComponent.color = c;
            yield return null;
        }
        Color finalColor = textComponent.color;
        finalColor.a = endAlpha;
        textComponent.color = finalColor;
    }
}
