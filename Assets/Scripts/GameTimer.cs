using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText; // Reference to a UI Text component to display the timer
    //public GameObject gameOverScreen; // Reference to the Game Over screen

    private float elapsedTime = 0f;
    private bool isTimerRunning = true;

    private void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay(elapsedTime, true);

            PlayerPrefs.SetFloat("Score", elapsedTime);
        }
    }

    public void GameOver()
    {
        isTimerRunning = false;
        //gameOverScreen.SetActive(true);
        UpdateTimerDisplay(elapsedTime, true);
    }

    private void UpdateTimerDisplay(float time, bool showMilliseconds)
    {
        int hours = Mathf.FloorToInt(time / 3600);
        int minutes = Mathf.FloorToInt((time % 3600) / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time % 1) * 1000);

        if (showMilliseconds)
        {
            timerText.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}.{milliseconds:D3}";
        }
        else if (hours > 0)
        {
            timerText.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }
        else if (minutes > 0)
        {
            timerText.text = $"{minutes:D2}:{seconds:D2}";
        }
        else
        {
            timerText.text = $"{seconds:D2}s";
        }
    }
}
