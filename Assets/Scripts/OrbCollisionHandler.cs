using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OrbCollisionHandler : MonoBehaviour
{
    private Transform playerTransform;

    public float attractionForce;

    public string collisionDeactivator;

    private void Start()
    {
        SetPlayerByTag("Player");
    }
    // Use this method to set the playerTransform when the orb spawns.
    public void SetPlayerByTag(string playerTag)
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player GameObject with tag " + playerTag + " not found.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == collisionDeactivator)
        {
            Destroy(this.gameObject);
        }
    }
    public void AttractTowardsPlayer()
    {
        if (playerTransform != null)
        {
            // Calculate the direction from the orb to the player.
            Vector3 attractionDirection = playerTransform.position - transform.position;

            // Apply a force to attract the orb towards the player.
            GetComponent<Rigidbody>().AddForce(attractionDirection.normalized * attractionForce);
        }
        else
        {
            Debug.LogWarning("Player transform is not set. Orb attraction aborted.");
        }
    }
}
