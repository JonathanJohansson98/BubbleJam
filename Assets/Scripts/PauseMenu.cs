using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenu;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button exitButton;

    public void Open()
    {
        gameObject.SetActive(true);

        pauseMenu.alpha = 1;
    }

    public void Resume() 
    {
        resumeButton.interactable = false;

        GameManager.instance.ResumeGame();
        resumeButton.onClick.RemoveListener(Resume);
        Destroy(gameObject);
    }

    public void Exit()
    {
        GameManager.instance.EndGame();
        exitButton.onClick.RemoveListener(Exit);
        exitButton.interactable = false;
        Destroy(gameObject);

    }
}
