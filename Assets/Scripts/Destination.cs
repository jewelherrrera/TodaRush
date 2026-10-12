
using UnityEngine;
using UnityEngine.Events;

public class Destination : Interactable
{
    public UnityEvent onDestinationReached;
    bool delivered;

    public void DeliverPassenger()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.score += 100;
        gm.destinationCount++;
        print("Destination Reached!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !delivered)
        {
            delivered = true;
            onDestinationReached.Invoke();
        }
    }
}