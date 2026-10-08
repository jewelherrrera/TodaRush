using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int health = 100;
    public int score = 0;
    public int highScore = 0;
    public int currentLevel = 1;
    public int destinationCount = 0;
    public int damage = 10;
    void Update()
    {
        if (health <= 0)
        {
            print("Game Over");
            enabled = false;
        }

        if (destinationCount >= 3)
        {
            print("YOU WIN!");
            enabled = false;
        }
    }
}