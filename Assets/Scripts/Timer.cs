using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float gameTime = 180f; // 3 minutes
    public TextMeshProUGUI timeTextMeshPro;
    public OrbSpawner orbSpawnerObject;
    public GameObject[] objectsToActivateOnEnd;
    public GameObject[] objectsToDeactivateOnEnd;
    public bool isGameRunning;
    public TextMeshProUGUI finalScoreTextMeshPro;

    private void Start()
    {
        isGameRunning = true;
    }

    private void Update()
    {
        gameTime -= Time.deltaTime;
        timeTextMeshPro.text = gameTime.ToString("0") + " sec";
        if (gameTime <= 0)
        {
            EndGame();
            isGameRunning = false;
            orbSpawnerObject.gameRunningSatus = isGameRunning;
        }
        if (orbSpawnerObject.gameRunningSatus)
            finalScoreTextMeshPro.text = GameManager.Instance.playerScoreTextMeshPro.text;
    }

    private void EndGame()
    {
        // Implement the logic for ending the game.
        // You may display the player's final score, show a game-over screen, etc.
        

        gameTime = 0;
        objectsToActivateOnEnd[0].SetActive(true);
        Destroy(objectsToDeactivateOnEnd[0]);
        objectsToDeactivateOnEnd[1].SetActive(false);
        orbSpawnerObject.enabled = false;
    }
}