using UnityEngine;

public class BouncyObject : MonoBehaviour
{
    public float bounceForce = 10f;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Add an initial force to make the object bounce
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
    }
}