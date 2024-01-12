using UnityEngine;
using System.Collections;

public class AlternatingBouncing : MonoBehaviour
{
    public GameObject objectToBounce1;
    public GameObject objectToBounce2;
    public float bounceForce = 10f;
    public float timeBetweenBounces = 2f;

    private Rigidbody rb1;
    private Rigidbody rb2;

    void Start()
    {
        rb1 = objectToBounce1.GetComponent<Rigidbody>();
        rb2 = objectToBounce2.GetComponent<Rigidbody>();

        if (rb1 == null || rb2 == null)
        {
            Debug.LogError("Rigidbody components not found on the child objects.");
            return;
        }

        // Start the bouncing coroutine
        StartCoroutine(AlternatingBounceCoroutine());
    }

    IEnumerator AlternatingBounceCoroutine()
    {
        while (true)
        {
            // Enable the first object and apply an upward force to simulate bouncing
            objectToBounce1.SetActive(true);
            rb1.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

            // Wait for the specified time before disabling the first object
            yield return new WaitForSeconds(timeBetweenBounces);

            // Record the position of the first object before disabling
            Vector3 startPosition = objectToBounce1.transform.position;

            // Disable the first object
            objectToBounce1.SetActive(false);

            // Wait for a short time before enabling the second object
            yield return new WaitForSeconds(0.1f);

            // Enable the second object at the position of the first object
            objectToBounce2.transform.position = startPosition;
            objectToBounce2.SetActive(true);
            rb2.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

            // Wait for the specified time before disabling the second object
            yield return new WaitForSeconds(timeBetweenBounces);

            // Disable the second object
            objectToBounce2.SetActive(false);

            // Wait for a short time before repeating the process
            yield return new WaitForSeconds(0.1f);
        }
    }
}