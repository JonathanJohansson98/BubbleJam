using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public int score;
}

public class Leaderboard : MonoBehaviour
{
    public List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();

    public Transform leaderBoardRoot;
    public LeaderboardEntryUI leaderboardEntry;

    [ContextMenu("Get")]
    public void SetupLeaderBoardFromPlayerprefs()
    {
        string json = PlayerPrefs.GetString("Leaderboard", "{}");
        leaderboardEntries = JsonUtility.FromJson<LeaderboardList>(json).entries;

        foreach (var item in leaderboardEntries)
        {
            Instantiate<LeaderboardEntryUI>(leaderboardEntry, leaderBoardRoot).SetupEntry(item);
        }
    }

    [ContextMenu("Save")]
    public void SaveLeaderBoardToPlayerprefs()
    {
        LeaderboardList leaderboardList = new LeaderboardList { entries = leaderboardEntries };
        string json = JsonUtility.ToJson(leaderboardList);
        PlayerPrefs.SetString("Leaderboard", json);
        PlayerPrefs.Save();
    }

    [System.Serializable]
    private class LeaderboardList
    {
        public List<LeaderboardEntry> entries;
    }
}
