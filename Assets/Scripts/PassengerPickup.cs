
using UnityEngine;
using UnityEngine.Events;

public class PassengerPickup : MonoBehaviour
{
    public UnityEvent Passenger;
    public Destination targetDestination;
    public GameManager gameManager;
    private bool isPickedUp = false;

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player") && !isPickedUp)
    {
        for (int i = 0; i < gameManager.passengers.Length; i++)
        {
            if (gameManager.passengers[i] == null)
            {
                isPickedUp = true;
                gameManager.passengers[i] = this;
                gameManager.passengersOnboard++;
                Passenger.Invoke();
                return;
            }
        }
    }
}
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
