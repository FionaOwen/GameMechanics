using UnityEngine;

public class OrbCollisionHandler : MonoBehaviour
{
    public OrbParentManager orbParentManager;



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player")) // Assuming VR headset is represented by a tag (adjust as needed).
        {
            // Handle orb collision with the VR headset (e.g., play a sound, provide haptic feedback, etc.).

            // Destroy the orb using the reference to the OrbParentManager.
            if (orbParentManager != null)
            {
                orbParentManager.DestroySelf();
            }
            else
            {
                Debug.LogWarning("OrbParentManager reference is missing.");
            }
        }
    }
}
