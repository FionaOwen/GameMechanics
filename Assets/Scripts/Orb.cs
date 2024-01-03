using UnityEngine;

public class Orb : MonoBehaviour
{
    public int basePointValue = 10; // Set the base point value for each orb.
    public Transform snapPoint; // Reference to the snapping point on the panel.

    private bool isScaledDown = false;
    private bool isCaptured = false;

    void Update()
    {
        if (isCaptured)
        {
            // Implement any logic for the floating movement of the orb when it's captured.
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
            // Check if the orb is placed on the snapping point on the panel.
            if (IsOnSnapPoint())
            {
                CaptureOrb();
            }
        }
    }

    private bool IsOnSnapPoint()
    {
        // Implement logic to check if the orb is on the snapping point.
        // You might use Collider or distance checks, depending on your setup.
        // Return true if on the snap point, false otherwise.
        return true; // Placeholder, modify based on your actual implementation.
    }

    private void CaptureOrb()
    {
        // Calculate the score based on the orb state.
        int score = CalculateScore();
        GameManager.Instance.AddScore(score);

        // Other capture logic...

        // Set the orb as captured to prevent further scoring.
        isCaptured = true;
        // Destroy the orb or deactivate it after capturing.
        gameObject.SetActive(false);
    }

    private int CalculateScore()
    {
        // Calculate the score based on the orb state.
        int score = isScaledDown ? basePointValue * 2 : basePointValue;
        return score;
    }
}