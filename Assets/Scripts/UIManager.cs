using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public GameManager gameManager;

    void Update()
    {
        healthText.text = "Health: " + gameManager.health;
        scoreText.text = "Score: " + gameManager.score;
        highScoreText.text = "High Score: " + gameManager.highScore;
    }
}