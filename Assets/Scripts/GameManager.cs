using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int health = 100;
    public int score = 0;
    public int highScore = 0;
    public int currentLevel = 1;
    public int destinationCount = 0;
    public int damage = 10;

    public bool allDestinationsDone = false;
    public bool gameWon = false;
    public bool timeExpired = false;
    public GameObject winText;
    public GameObject gameOverText;
    public GameObject restartButton;

    public PassengerPickup[] passengers = new PassengerPickup[5];
    public int passengersOnboard = 0;
    public int passengersDelivered = 0;

    public int requiredPassengerCount = 3;
    public float tripTimeLimit = 180f;
    private float timeRemaining;
    private bool timerStarted = false;
    public TMP_Text timerText;

    void Start()
{
    timeRemaining = tripTimeLimit;

    if (timerText != null)
    {
        timerText.text = "Waiting for passengers (0/" + requiredPassengerCount + ")";
    }
}

    void Update()
    {
        if (health <= 0)
        {
            print("Game Over");
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
            enabled = false;
        }



    if (!timerStarted && passengersOnboard >= requiredPassengerCount)
{
    timerStarted = true;
}

    if (!timerStarted)
{
    if (timerText != null)
    {
        timerText.text = "Waiting for passengers (" + passengersOnboard + "/" + requiredPassengerCount + ")";
    }
}
    else if (!gameWon && health > 0)
{
    timeRemaining -= Time.deltaTime;

    if (timeRemaining <= 0f)
    {
        timeRemaining = 0f;
        timeExpired = true;

        if (timerText != null)
        {
            timerText.text = "TIME'S UP!";
        }

        gameOverText.SetActive(true);
        restartButton.SetActive(true);
        enabled = false;
        return;
    }

    int minutes = Mathf.FloorToInt(timeRemaining / 60f);
    int seconds = Mathf.FloorToInt(timeRemaining % 60f);

    if (timerText != null)
    {
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}


        if (destinationCount >= 3)
        {
            allDestinationsDone = true;
        }
    }

    public bool DeliverPassenger(Destination destination)
    {
        foreach (PassengerPickup passenger in passengers)
        {
            if (passenger != null && passenger.targetDestination == destination)
            {
                passengersDelivered++;
                passengersOnboard--;

                passenger.targetDestination = null;
                passenger.gameObject.SetActive(false);

                return true;
            }
        }

        return false;
    }
}