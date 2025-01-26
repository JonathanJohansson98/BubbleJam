using TMPro;
using UnityEngine;

public class LeaderboardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetupEntry(LeaderboardEntry leaderboardEntry) 
    {
        playerNameText.text = leaderboardEntry.playerName;

        scoreText.text = $"Score: {leaderboardEntry.score}";

        gameObject.SetActive(true);
    }
}
