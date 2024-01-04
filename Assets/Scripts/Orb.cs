using UnityEngine;

public class Orb : MonoBehaviour
{
    public int basePointValue = 10; // Set the base point value for each orb.
    public float timeToLive = 10f;   // Time in seconds before the orb automatically destroys itself.

    private bool isScaledDown = false;
    private bool isCaptured = false;

    void Start()
    {
        // Automatically destroy the orb after a certain time.
        Invoke("DestroySelf", timeToLive);
    }

    void Update()
    {
        if (!isCaptured)
        {
            // Implement floating movement for the orb when it's not captured.
            // You may use Mathf.Sin or other methods to create a floating effect.
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Sin(Time.time) * 0.2f + 2f, // Adjust the floating height as needed.
                transform.position.z
            );
        }
    }

    public void Grab()
    {
        // Set the orb state based on its scale.
        isScaledDown = transform.localScale.x < 1.0f;
    }

    public void Release()
    {
        if (!isCaptured)
        {
            CaptureOrb();
        }
    }

    private void CaptureOrb()
    {
        // Calculate the score based on the orb state.
        int score = CalculateScore();
        GameManager.Instance.AddScore(score);

        // Other capture logic...

        // Destroy the orb after capturing.
        Destroy(gameObject);
    }

    private void DestroySelf()
    {
        // Destroy the orb after the specified time.
        Destroy(gameObject);
    }

    private int CalculateScore()
    {
        // Calculate the score based on the orb state.
        int score = isScaledDown ? basePointValue * 2 : basePointValue;
        return score;
    }
}