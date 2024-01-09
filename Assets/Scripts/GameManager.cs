using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public int playerScore = 0;
    public TextMeshProUGUI playerScoreTextMeshPro;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject("GameManagerOrb");
                    _instance = singleton.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int score) 
    { 
        playerScore += score;
        playerScoreTextMeshPro.text = playerScore.ToString();
    }

    public void ResetScore()
    {
        playerScore = 0;
        playerScoreTextMeshPro.text = playerScore.ToString();
    }
}