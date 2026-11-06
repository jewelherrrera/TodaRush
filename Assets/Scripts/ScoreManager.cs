using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public int highScore = 0;

    private const string HighScoreKey = "TODA_RUSH_HighScore_v1";

    void Awake()
    {
        score = 0;
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    public void AddScore(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        score += amount;

        if (score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
        }
    }
}