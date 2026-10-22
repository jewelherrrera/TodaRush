using UnityEngine;
using System.Collections;

public class Queue : MonoBehaviour
{


    public PassengerPickup[] terminalPassengers;
    public float nextPassengerArrivalDelay = 2f;

    private int nextTerminalPassengerIndex = 0;

void Start()
{
    if (terminalPassengers == null || terminalPassengers.Length == 0)
    {
        return;
    }

    for (int i = 0; i < terminalPassengers.Length; i++)
    {
        if (terminalPassengers[i] != null)
        {
            terminalPassengers[i].gameObject.SetActive(false);
        }
    }

    if (terminalPassengers[0] != null)
    {
        terminalPassengers[0].gameObject.SetActive(true);
        nextTerminalPassengerIndex = 1;
    }
}

public void ShowNextTerminalPassenger()
{
    StartCoroutine(ShowNextTerminalPassengerAfterDelay());
}

private IEnumerator ShowNextTerminalPassengerAfterDelay()
{
    yield return new WaitForSeconds(nextPassengerArrivalDelay);

    if (terminalPassengers == null || nextTerminalPassengerIndex >= terminalPassengers.Length)
    {
        yield break;
    }

    PassengerPickup nextPassenger = terminalPassengers[nextTerminalPassengerIndex];

    nextTerminalPassengerIndex++;

    if (nextPassenger != null)
    {
        nextPassenger.gameObject.SetActive(true);
    }
}


}