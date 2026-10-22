using UnityEngine;
using UnityEngine.Events;

public class Destination : Interactable
{
    public UnityEvent onDestinationReached;

    public void DeliverPassenger()
{
    if (gameManager != null && gameManager.DeliverPassenger(this))
    {
        gameManager.score += 100;
        gameManager.destinationCount++;
    }
}
    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        onDestinationReached.Invoke();
    }
}

}