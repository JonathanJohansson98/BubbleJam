using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup pauseMenu;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    public void Open()
    {
        Cursor.visible = true;
        gameObject.SetActive(true);

        pauseMenu.alpha = 1;
    }

    public void Resume() 
    {
        resumeButton.interactable = false;

        GameManager.instance.ResumeGame();
        resumeButton.onClick.RemoveListener(Resume);
        Destroy(gameObject);
        Cursor.visible = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Resume();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
