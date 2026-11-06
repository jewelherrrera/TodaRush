using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int health = 100;
    [HideInInspector] public int destinationCount = 0;
    public int requiredLaps = 3;
    [HideInInspector] public int completedLaps = 0;
    public int damage = 10;
    [HideInInspector] public bool allDestinationsDone = false;
    [HideInInspector] public bool gameWon = false;
    [HideInInspector] public bool timeExpired = false;
    public GameObject winText;
    public GameObject gameOverText;
    public GameObject restartButton;
    public UIManager uiManager;
    public PassengerPickup[] passengers = new PassengerPickup[5];
    [HideInInspector] public int passengersOnboard = 0;
    public ScoreManager scoreManager;

    void Awake()
    {
        if (scoreManager == null)
        {
        scoreManager = GetComponent<ScoreManager>();
        }
    }

    void Update()
    {
        if (health <= 0)
        {
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
            enabled = false;
        }

        if (destinationCount >= requiredLaps)
        {
            allDestinationsDone = true;
        }
    }

    public bool CompleteLap()
    {
        TripTimer tripTimer = GetComponent<TripTimer>();

    if (tripTimer == null || !tripTimer.IsTimerStarted() || timeExpired || health <= 0 || gameWon || destinationCount <= completedLaps || completedLaps >= requiredLaps)
        {
        return false;
        }

        completedLaps++;

    if (completedLaps >= requiredLaps)
        {
        gameWon = true;
        winText.SetActive(true);
        restartButton.SetActive(true);
        }

        return true;
    }

    public void TakeDamage()
    {
        // Ignore damage after game over, win, or time up.
        if (health <= 0 || gameWon || timeExpired)
        {
            return;
        }

        health = Mathf.Max(0, health - damage);

        if (uiManager != null)
        {
        uiManager.ShowDamagePopup(damage);
        }
    }

    public bool DeliverPassenger(Destination destination)
    {
        TripTimer tripTimer = GetComponent<TripTimer>();

        if (tripTimer == null || !tripTimer.IsTimerStarted())
        {
            return false;
        }

        foreach (PassengerPickup passenger in passengers)
        {
            if (passenger != null && passenger.hasBoarded && passenger.targetDestination == destination)
            {
                passengersOnboard--;
                passenger.targetDestination = null;
                passenger.gameObject.SetActive(false);
                return true;
            }
        }

        return false;
    }
}
