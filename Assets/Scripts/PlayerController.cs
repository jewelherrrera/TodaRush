using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameManager gameManager;

    void Update()
    {
        if (gameManager.health <= 0 || gameManager.destinationCount >= 3)
        {
            return;
        }

        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * move * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * turn * 100f * Time.deltaTime);
    }
}