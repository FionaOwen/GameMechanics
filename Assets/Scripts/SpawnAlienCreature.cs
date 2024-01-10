using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAlienCreature : MonoBehaviour
{
    public GameObject firstObject; // Assign in the Inspector
    public GameObject secondObject; // Assign in the Inspector
    public float delayInSeconds = 2f;

    public MeshRenderer waterRenderer;
    public GameObject spyWaterCube;
    public Animator waterAnimator;
    public OrbManager orbManagerToDisable;
    public Rigidbody orbManagerRigidbody;

    public bool hasCollided = false;


    private void OnTriggerEnter(Collider other)
    {
        // Check if the collision involves the specified GameObject
        if (other.gameObject.tag == "SpaceAircraft" && !hasCollided)
        {
            waterAnimator.enabled = false;
            spyWaterCube.SetActive(false);
            orbManagerRigidbody.isKinematic = true;
            orbManagerToDisable.enabled = false;
            //waterRenderer.enabled = false;
            // Enable the first GameObject
            firstObject.SetActive(true);
            hasCollided = true;

            // Start a coroutine to enable the second GameObject after a delay
            StartCoroutine(EnableSecondObjectAfterDelay());
        }
    }
    private IEnumerator EnableSecondObjectAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        firstObject.SetActive(false);

        // Enable the second GameObject after the specified delay
        secondObject.SetActive(true);
    }
}
