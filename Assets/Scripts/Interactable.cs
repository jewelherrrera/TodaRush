using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected GameManager gameManager;

    protected virtual void Awake()
    {
        GameObject managerObject = GameObject.Find("GameManager");

        if (managerObject != null)
        {
            gameManager = managerObject.GetComponent<GameManager>();
        }
    }
}