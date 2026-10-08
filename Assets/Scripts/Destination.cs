using UnityEngine;

public class Destination : Interactable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            gameManager.score += 100;
            gameManager.destinationCount++;

            if (gameManager.score > gameManager.highScore)
            {
                gameManager.highScore = gameManager.score;
            }

            print("Destination Reached!");

            gameObject.SetActive(false);
        }
    }
}