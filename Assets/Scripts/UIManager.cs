using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI timerText;
    public GameManager gameManager;

    void Update()
    {
        healthText.text = "Health: " + gameManager.health;
        scoreText.text = "Score: " + gameManager.score;
        highScoreText.text = "High Score: " + gameManager.highScore;


    if (gameManager != null && timerText != null)
{
    if (gameManager.timeExpired)
    {
        timerText.text = "TIME'S UP!";
    }
    else if (!gameManager.IsTimerStarted())
    {
        timerText.text = "Waiting for passengers (" + gameManager.passengersOnboard + "/" + gameManager.requiredPassengerCount + ")";
    }
    else
    {
        float remainingTime = Mathf.Max(
            0f, gameManager.GetTimeRemaining());

        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        timerText.text = minutes.ToString("00") + ":"
            + seconds.ToString("00");
    }
}


    }
}