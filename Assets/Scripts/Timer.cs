using UnityEngine;

public class Timer : MonoBehaviour
{
    public float gameTime = 180f; // 3 minutes

    private void Update()
    {
        gameTime -= Time.deltaTime;

        if (gameTime <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        // Implement the logic for ending the game.
        // You may display the player's final score, show a game-over screen, etc.
        // Adjust this based on your design preferences.
    }
}