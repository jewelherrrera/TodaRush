using UnityEngine;

public class Obstacle : Interactable
{
    private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Player") && gameManager != null)
    {
        gameManager.health -= gameManager.damage;
    }
}
}