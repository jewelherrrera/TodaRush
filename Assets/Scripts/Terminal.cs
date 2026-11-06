
using UnityEngine;

public class Terminal : Interactable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager.CompleteLap())
            {
                if (!gameManager.gameWon)
                {
                    gameManager.GetComponent<Queue>().ShowNextTerminalPassenger();
                }
            }
        }
    }
}
