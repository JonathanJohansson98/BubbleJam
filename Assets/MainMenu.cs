using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGame;
    [SerializeField] private Button exitGame;
    [SerializeField] private Button options;

    private void Awake()
    {
        startGame.onClick.AddListener(StartGame);
        exitGame.onClick.AddListener(ExitGame);
        options.onClick.AddListener(Options);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
       Application.Quit();
    }

    public void Options()
    {
        Debug.LogError("Options not implemented");
    }
}
