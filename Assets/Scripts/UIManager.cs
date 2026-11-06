
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI damagePopupText;
    public TextMeshProUGUI lapText;
    public TextMeshProUGUI lapNotificationText;
    public TextMeshProUGUI missionSummaryText;

    public GameObject gameplayHUD;
    public GameObject victoryDimOverlay;
    public GameObject victorySummaryPanel;

    public GameManager gameManager;
    public TripTimer tripTimer;
    public PlayerController playerController;
    public TextMeshProUGUI speedText;

    public float popupDuration = 1.3f;
    public float popupRiseDistance = 50f;
    public float popupStartScale = 1.2f;
    public float notificationDuration = 2.5f;

    private Coroutine popupCoroutine;
    private Coroutine notificationCoroutine;

    private RectTransform popupRect;
    private Vector2 popupStartPosition;
    private Vector3 popupNormalScale;
    private Color popupColor;

    private int previousDisplayedLap = -1;
    private int previousCompletedLaps = 0;
    private bool summaryShown = false;

    void Start()
    {
        CachePopup();

        if (damagePopupText != null)
        {
            damagePopupText.gameObject.SetActive(false);
        }

        if (lapNotificationText != null)
        {
            lapNotificationText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (gameManager == null)
        {
            return;
        }

        if (gameManager.gameWon)
        {
            UpdateMissionSummary();
            return;
        }

        healthText.text = "Health: " + gameManager.health.ToString();
        scoreText.text = "Score: " + gameManager.scoreManager.score.ToString();
        highScoreText.text = "High Score: " + gameManager.scoreManager.highScore.ToString();

        UpdateTimerText();
        UpdateLapText();
        UpdateLapNotification();

        speedText.text = playerController.currentSpeed.ToString("0");
    }

    private void UpdateLapText()
    {
        if (lapText == null)
        {
            return;
        }

        int currentLap = Mathf.Min(gameManager.completedLaps + 1, gameManager.requiredLaps);

        if (currentLap == previousDisplayedLap)
        {
            return;
        }

        previousDisplayedLap = currentLap;

        lapText.text = "LAP " + currentLap.ToString() + " / " + gameManager.requiredLaps.ToString();
    }

    public void ShowDamagePopup(int damageAmount)
    {
        if (damagePopupText == null)
        {
            return;
        }

        CachePopup();

        if (popupCoroutine != null)
        {
            StopCoroutine(popupCoroutine);
        }

        popupRect.anchoredPosition = popupStartPosition;
        popupRect.localScale = popupNormalScale * popupStartScale;
        damagePopupText.color = popupColor;
        damagePopupText.text = "-" + damageAmount.ToString() + " DAMAGE!";
        damagePopupText.gameObject.SetActive(true);

        popupCoroutine = StartCoroutine(AnimatePopup());
    }

    private IEnumerator AnimatePopup()
    {
        float timer = 0f;

        while (timer < popupDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / popupDuration);

            popupRect.anchoredPosition = popupStartPosition + Vector2.up * (popupRiseDistance * t);

            popupRect.localScale = popupNormalScale * Mathf.Lerp(popupStartScale, 1f, Mathf.Clamp01(t * 4f));

            Color c = popupColor;
            c.a = popupColor.a * (1f - t);
            damagePopupText.color = c;

            yield return null;
        }

        damagePopupText.gameObject.SetActive(false);
        popupCoroutine = null;
    }

    private void CachePopup()
    {
        if (popupRect != null || damagePopupText == null)
        {
            return;
        }

        popupRect = damagePopupText.rectTransform;
        popupStartPosition = popupRect.anchoredPosition;
        popupNormalScale = popupRect.localScale;
        popupColor = damagePopupText.color;
    }

    private void UpdateTimerText()
    {
        if (tripTimer == null || timerText == null)
        {
            return;
        }

        if (gameManager.timeExpired)
        {
            timerText.text = "TIME'S UP!";
        }
        else if (!tripTimer.IsTimerStarted())
        {
            timerText.text = "Waiting for passengers (" + gameManager.passengersOnboard.ToString() + "/" + tripTimer.requiredPassengerCount.ToString() + ")";
        }
        else
        {
            float remainingTime = Mathf.Max(0f, tripTimer.GetTimeRemaining());

            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);

            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    private void UpdateLapNotification()
    {
        if (lapNotificationText == null)
        {
            return;
        }

        if (gameManager.completedLaps > previousCompletedLaps)
        {
            previousCompletedLaps = gameManager.completedLaps;

            if (notificationCoroutine != null)
            {
                StopCoroutine(notificationCoroutine);
            }

            if (gameManager.completedLaps >= gameManager.requiredLaps)
            {
                lapNotificationText.text = "RACE COMPLETE!";
            }
            else
            {
                lapNotificationText.text = "LAP " + gameManager.completedLaps.ToString() + " COMPLETE!";
            }

            lapNotificationText.gameObject.SetActive(true);
            notificationCoroutine = StartCoroutine(HideLapNotification());
        }
    }

    private IEnumerator HideLapNotification()
    {
        yield return new WaitForSeconds(notificationDuration);

        lapNotificationText.gameObject.SetActive(false);
        notificationCoroutine = null;
    }

    private void UpdateMissionSummary()
    {
        if (summaryShown)
        {
            return;
        }

        if (gameplayHUD == null || victoryDimOverlay == null || victorySummaryPanel == null || missionSummaryText == null || tripTimer == null || gameManager.scoreManager == null)
        {
            return;
        }

        summaryShown = true;

        gameplayHUD.SetActive(false);
        victoryDimOverlay.SetActive(true);
        victorySummaryPanel.SetActive(true);

        float remainingTime = tripTimer.GetTimeRemaining();

        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        missionSummaryText.text = "LAPS COMPLETED: " + gameManager.completedLaps.ToString() + " / " + gameManager.requiredLaps.ToString() + "\n" + "PASSENGERS DELIVERED: " + gameManager.destinationCount.ToString() + " / " + gameManager.requiredLaps.ToString() + "\n" + "FINAL SCORE: " + gameManager.scoreManager.score.ToString() + "\n" + "TIME REMAINING: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
