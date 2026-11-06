using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Destination : Interactable
{
    public UnityEvent onDestinationReached;

    public GameObject markerRoot;
    public TMP_Text markerText;
    public Transform player;
    public Camera mainCamera;
    public string destinationName;
    private Vector3 markerBaseScale;

    void Start()
{
    markerBaseScale = markerRoot.transform.localScale;
    markerText.isOverlay = true;
}

    void Update()
    {
        if (gameManager == null || markerRoot == null) return;

        bool showMarker = false;

        foreach (PassengerPickup passenger in gameManager.passengers)
        {
            if (passenger != null && passenger.hasBoarded && passenger.targetDestination == this)
            {
                showMarker = true;
                break;
            }
        }

        markerRoot.SetActive(showMarker);

        if (!showMarker) return;

        int distance = Mathf.RoundToInt(Vector3.Distance(player.position, transform.position));

        markerText.text = destinationName + "\n" + distance.ToString() + "m";
        markerText.transform.LookAt(mainCamera.transform);
        markerText.transform.Rotate(0f, 180f, 0f);
        float cameraDistance = Vector3.Distance(mainCamera.transform.position, markerRoot.transform.position);
        float markerScale = Mathf.Clamp(cameraDistance / 50f, 1f, 3f);
        markerRoot.transform.localScale = markerBaseScale * markerScale;
        
    }

    public void DeliverPassenger()
    {
        if (gameManager != null && gameManager.DeliverPassenger(this))
        {
            gameManager.scoreManager.AddScore(100);
            gameManager.destinationCount++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onDestinationReached.Invoke();
        }
    }
}