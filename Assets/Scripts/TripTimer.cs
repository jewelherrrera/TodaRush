using UnityEngine;

public class TripTimer : MonoBehaviour
{
    public int requiredPassengerCount = 3;
    public float tripTimeLimit = 180f;
    private float timeRemaining;
    private bool timerStarted = false;
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    void Start()
    {
        timeRemaining = tripTimeLimit;
    }

    void Update()
    {
        if (gameManager == null)
        {
            return;
        }

        if (!timerStarted && gameManager.passengersOnboard >= requiredPassengerCount)
        {
            timerStarted = true;
        }

        if (timerStarted && !gameManager.gameWon && !gameManager.timeExpired && gameManager.health > 0)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                gameManager.timeExpired = true;

                if (gameManager.gameOverText != null)
                {
                    gameManager.gameOverText.SetActive(true);
                }

                if (gameManager.restartButton != null)
                {
                    gameManager.restartButton.SetActive(true);
                }

                enabled = false;
                return;
            }
        }
    }

    public bool IsTimerStarted()
    {
        return timerStarted;
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }
}