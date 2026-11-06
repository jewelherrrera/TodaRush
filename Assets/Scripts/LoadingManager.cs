
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Slider progressBar;
    public TMP_Text progressText;
    public GameObject continueHint;

    private static string targetScene = "LevelSelect";

    public static void LoadThroughLoadingScreen(string sceneName)
    {
        targetScene = sceneName;
        SceneManager.LoadScene("LoadingScreen");
    }

    async void Start()
    {
        await LoadSelectedScene();
    }

    private async Task LoadSelectedScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(targetScene);

        operation.allowSceneActivation = false;

        for (int progress = 10; progress <= 90; progress += 10)
        {
            progressBar.value = progress / 100f;
            progressText.text = progress.ToString() + "%";

            await Task.Delay(1000);
        }

        while (operation.progress < 0.9f)
        {
            await Task.Yield();
        }

        progressBar.value = 0.99f;
        progressText.text = "99%";

        continueHint.SetActive(true);

        while (!Input.anyKeyDown)
        {
            await Task.Yield();
        }

        operation.allowSceneActivation = true;
    }
}
