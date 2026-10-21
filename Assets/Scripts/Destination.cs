using UnityEngine;
using UnityEngine.Events;

public class Destination : Interactable
{
    public UnityEvent onDestinationReached;
    bool delivered;

    public void DeliverPassenger()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gm.DeliverPassenger(this))
        {
            delivered = true;
            gm.score += 100;
            gm.destinationCount++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !delivered)
        {
            onDestinationReached.Invoke();
        }
    }
}