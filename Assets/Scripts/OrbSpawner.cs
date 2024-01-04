using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject[] orbPrefabs; // Array of different orb prefabs to spawn.
    public Transform[] spawnPoints;  // Array of spawn points where orbs will be spawned.
    public float spawnInterval = 5f; // Time interval between orb spawns.
    public float orbLifetime = 10f;  // Time in seconds before the spawned orbs automatically destroy.
    public int maxOrbsPerSpawn = 2;   // Maximum number of orbs to spawn in a single convergence event.

    private int orbsSpawned = 0;

    void Start()
    {
        InvokeRepeating("SpawnOrbs", 0f, spawnInterval);
    }

    void SpawnOrbs()
    {
        if (orbsSpawned < maxOrbsPerSpawn)
        {
            // Randomly select an orb prefab from the array.
            GameObject randomOrbPrefab = orbPrefabs[Random.Range(0, orbPrefabs.Length)];

            // Choose a random spawn point.
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Check if there's already an orb at the spawn point.
            if (!IsOrbPresentAtSpawnPoint(spawnPoint))
            {
                // Get the exact position of the spawn point.
                Vector3 spawnPosition = spawnPoint.position;

                // Instantiate the selected orb at the spawn point's position.
                GameObject newOrb = Instantiate(randomOrbPrefab, spawnPosition, Quaternion.identity);

                // Automatically destroy the spawned orb after a certain time.
                Destroy(newOrb, orbLifetime);

                orbsSpawned++;
            }
        }
    }

    bool IsOrbPresentAtSpawnPoint(Transform spawnPoint)
    {
        Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, 0.1f); // Adjust the radius as needed.

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Orb"))
            {
                return true;
            }
        }

        return false;
    }
}