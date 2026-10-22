using UnityEngine;

public class Terminal : Interactable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager.allDestinationsDone && !gameManager.timeExpired && gameManager.health > 0 && !gameManager.gameWon)
            {
                gameManager.gameWon = true;
                gameManager.winText.SetActive(true);
                gameManager.restartButton.SetActive(true);
            }
        }
    }
}   