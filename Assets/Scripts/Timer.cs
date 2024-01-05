using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float gameTime = 180f; // 3 minutes
    public TextMeshProUGUI timeTextMeshPro;
    public GameObject[] objectsToActivateOnEnd;
    public GameObject[] objectsToDeactivateOnEnd;

    private void Update()
    {
        gameTime -= Time.deltaTime;
        timeTextMeshPro.text = gameTime.ToString("0") + " sec";
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