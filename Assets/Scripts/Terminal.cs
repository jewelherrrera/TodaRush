using UnityEngine;

public class Terminal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            if (gameManager.allDestinationsDone)
            {
                gameManager.gameWon = true;
                print("YOU WIN!");
                gameManager.winText.SetActive(true);
                gameManager.restartButton.SetActive(true);
            }
        }
    }
}