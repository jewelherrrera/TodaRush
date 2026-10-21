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

    public GameObject winText;
    public GameObject gameOverText;
    public GameObject restartButton;

    public PassengerPickup[] passengers = new PassengerPickup[5];
    public int passengersOnboard = 0;
    public int passengersDelivered = 0;

    void Update()
    {
        if (health <= 0)
        {
            print("Game Over");
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
        foreach (PassengerPickup passenger in passengers)
        {
            if (passenger != null &&
                passenger.targetDestination == destination)
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