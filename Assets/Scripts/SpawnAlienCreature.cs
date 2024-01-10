using System.Collections;
using UnityEngine;

public class SpawnAlienCreature : MonoBehaviour
{
    public GameObject alienCreaturePrefab; // Assign in the Inspector
    public GameObject waterSplashPrefab; // Assign in the Inspector
    public float delayInSeconds = 2f;

    public MeshRenderer waterRenderer;
    public GameObject spyWaterCube;
    public Animator waterAnimator;
    public OrbManager orbManagerToDisable;
    public Rigidbody orbManagerRigidbody;

    private bool hasCollided = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collision involves the specified GameObject
        if (other.gameObject.tag == "SpaceAircraft" && !hasCollided)
        {
            waterAnimator.enabled = false;
            spyWaterCube.SetActive(false);
            orbManagerRigidbody.isKinematic = true;
            orbManagerToDisable.enabled = false;

            // Get the position of the triggering object
            Vector3 spawnPosition = transform.position;

            // Instantiate the water splash effect at the same position
            Instantiate(waterSplashPrefab, spawnPosition, Quaternion.identity);

            // Instantiate the alien creature at the same position
//            Instantiate(alienCreaturePrefab, spawnPosition, Quaternion.identity);
            StartCoroutine(EnableAlienCreatureWithDelay());

            // Disable the water renderer if needed
            // waterRenderer.enabled = false;

            hasCollided = true;

            // Start a coroutine to disable the spawned objects after a delay
            //StartCoroutine(DisableSpawnedObjectsAfterDelay());

        }
    }
    private IEnumerator EnableAlienCreatureWithDelay()
    {
        Vector3 spawnPosition = transform.position;

        yield return new WaitForSeconds(0.2f);
        Instantiate(alienCreaturePrefab, spawnPosition, Quaternion.identity);

    }
    private IEnumerator DisableSpawnedObjectsAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);

        // Destroy the spawned water splash effect
        Destroy(GameObject.FindGameObjectWithTag("WaterSplash"));

        // Disable the second GameObject (alien creature) after the specified delay
        // You may need to modify this based on your specific alien creature logic
        GameObject alienCreature = GameObject.FindGameObjectWithTag("AlienCreature");
        if (alienCreature != null)
        {
            alienCreature.SetActive(false);
        }
    }
}
