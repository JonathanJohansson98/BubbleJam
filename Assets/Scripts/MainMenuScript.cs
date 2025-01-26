using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Button startGame;
    [SerializeField] private Button exitGame;
    [SerializeField] private Button options;
    //[SerializeField] private Button credits;


    private void Awake()
    {
        startGame.onClick.AddListener(StartGame);
        exitGame.onClick.AddListener(ExitGame);
        //credits.onClick.AddListener(Credits);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void Options()
    {
        Debug.LogError("Options not implemented");
    }
    public void Credits()
    {
        Debug.LogError("Credits not implemented");
    }
}
