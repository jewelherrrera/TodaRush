using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

void Update()
{
    float move = Input.GetAxis("Vertical");
    float turn = Input.GetAxis("Horizontal");

    transform.Translate(Vector3.forward * move * moveSpeed * Time.deltaTime);
    transform.Rotate(Vector3.up * turn * 100f * Time.deltaTime);
}
}