using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainMap");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainMap");
    }
}