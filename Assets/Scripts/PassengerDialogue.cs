
using TMPro;
using UnityEngine;

public class PassengerRequestDialogue : MonoBehaviour
{
    public GameObject requestBubbleCanvas;
    public TMP_Text requestText;
    public Camera mainCamera;
    private PassengerPickup passengerPickup;

    void Awake()
    {
        passengerPickup = GetComponent<PassengerPickup>();
    }

    void Update()
    {
        bool showDialogue = !passengerPickup.hasBoarded && !passengerPickup.IsApproaching && passengerPickup.targetDestination != null;
        requestBubbleCanvas.SetActive(showDialogue);

        if (!showDialogue)
        {
            return;
        }

        requestText.text = "Pahatid po sa " + passengerPickup.targetDestination.destinationName + "!";
        requestBubbleCanvas.transform.LookAt(mainCamera.transform);
        requestBubbleCanvas.transform.Rotate(0f, 180f, 0f);
    }
}
