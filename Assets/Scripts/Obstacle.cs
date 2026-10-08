using UnityEngine;

public class Obstacle : Interactable
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            gameManager.health -= gameManager.damage;
        }
    }
}