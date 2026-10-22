
using UnityEngine;
using UnityEngine.Events;

public class PassengerPickup : MonoBehaviour
{
    public UnityEvent Passenger;
    public Destination targetDestination;
    public GameManager gameManager;
    private bool isPickedUp = false;
    public bool hasBoarded = false;
    public Transform boardingPoint;
    public float boardingSpeed = 3f;
    private bool isApproaching = false;
    public float pickupDistance = 6f;
    private Transform playerTransform;

    void Update()
{

        if (!isPickedUp && !isApproaching && gameManager != null && boardingPoint != null)
{
    if (playerTransform == null)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= pickupDistance)
    {
        for (int i = 0; i < gameManager.passengers.Length; i++)
        {
            if (gameManager.passengers[i] == null)
            {
                isPickedUp = true;
                isApproaching = true;
                gameManager.passengers[i] = this;
                break;
            }
        }
    }
}

        if (!isApproaching || boardingPoint == null || gameManager == null)
    {
        return;
    }

    transform.position = Vector3.MoveTowards(transform.position, boardingPoint.position, boardingSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, boardingPoint.position) <= 0.1f)
    {
        isApproaching = false;
        hasBoarded = true;
        gameManager.passengersOnboard++;
        gameManager.GetComponent<Queue>().ShowNextTerminalPassenger();
        Passenger.Invoke();

    }
}
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
