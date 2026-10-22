using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int health = 100;
    public int score = 0;
    public int highScore = 0;
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

    void Update()
    {
        if (health <= 0)
        {
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
            enabled = false;
        }

        if (destinationCount >= 3)
        {
            allDestinationsDone = true;
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