using TMPro;
using UnityEngine;

public class LeaderboardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetupEntry(LeaderboardEntry leaderboardEntry) 
    {
        playerNameText.text = leaderboardEntry.playerName;

        scoreText.text = $"{leaderboardEntry.score}";

        gameObject.SetActive(true);
    }

    public void SetupTitleEntry()
    {
        playerNameText.text = "Leaderboard";
        scoreText.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
