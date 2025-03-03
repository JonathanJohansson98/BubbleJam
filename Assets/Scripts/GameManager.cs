using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PauseMenu pauseOverlay;

    private bool isGamePaused = false;
    private bool isGameOver = false;

    public float currentTime = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!isGamePaused && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            PauseGame();
        }
    }

    public void StartGame()
    {
        isGameOver = false;
        Time.timeScale = 1f;

        SceneManager.LoadScene("GameScene");
    }

    [ContextMenu("Pause")]
    public void PauseGame()
    {
        if(!isGamePaused)
            Instantiate<PauseMenu>(pauseOverlay, Vector3.zero, Quaternion.identity).Open();

        Cursor.visible = true;
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        Application.Quit();
    }

    public void RestartGame()
    {
        isGameOver = false;
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
