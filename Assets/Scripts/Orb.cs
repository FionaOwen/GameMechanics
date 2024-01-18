using UnityEngine;

public class Orb : MonoBehaviour
{
    public int basePointValue = 10; // Set the base point value for each orb.
    //public float timeToLive = 10f;   // Time in seconds before the orb automatically destroys itself.
    //public float floatingYValue;

    //private bool isScaledDown = false;
    //private bool isCaptured = false;
    //private float initialYPosition;

    //void Start()
    //{
    //    // Store initial Y position.
    //    initialYPosition = transform.position.y;

    //    // Automatically destroy the orb after a certain time.
    //    Invoke("DestroySelf", timeToLive);
    //}

    //void Update()
    //{
    //    if (!isCaptured)
    //    {
    //        // Implement floating movement for the orb when it's not captured.        
    //        // Use the initial Y position as the base for the floating effect.
    //        float floatingHeight = Mathf.Sin(Time.time) * 0.2f + 2f;         
    //        transform.position = new Vector3(transform.position.x, initialYPosition + floatingHeight, transform.position.z);
            
            
    //        //transform.position = new Vector3(
    //        //    transform.position.x,
    //        //    Mathf.Sin(Time.time) * 0.2f + floatingYValue, // Adjust the floating height as needed.
    //        //    transform.position.z
    //        //);
    //    }
    //}

    //public void Grab()
    //{
    //    // Set the orb state based on its scale.
    //    isScaledDown = transform.localScale.x < 1.0f;
    //}

    //public void Release()
    //{
    //    if (!isCaptured)
    //    {
    //        CaptureOrb();
    //    }
    //}

    public void CaptureOrb()
    {
        // Calculate the score based on the orb state.
        //int score = CalculateScore();
        GameManager.Instance.AddScore(basePointValue);

    }

    //private void DestroySelf()
    //{
    //    // Destroy the orb after the specified time.
    //    Destroy(gameObject);
    //}

    //private int CalculateScore()
    //{
    //    // Calculate the score based on the orb state.
    //    int score = isScaledDown ? basePointValue * 2 : basePointValue;
    //    return score;
    //}
}