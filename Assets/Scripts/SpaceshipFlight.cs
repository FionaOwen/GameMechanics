using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpaceshipFlight : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints for the spaceship's path.
    public Transform landingStation; // Landing station where the spaceship will land.
    public float movementSpeed = 5f; // Speed of the spaceship movement.
    public float rotationSpeed = 5f; // Speed of the spaceship rotation.

    [SerializeField]
    private Transform spaceshipTransform; // Reference to the spaceship's transform.

    [SerializeField]
    private GameObject xrRig; // Reference to the XR Rig component.

    private int currentWaypointIndex = 0; // Index of the current waypoint.

    public GameObject ShipFrontCollider;


    void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            MoveSpaceship();
        }
        else
        {
            // If waypoints are done, land on the station.
            LandOnStation();
            ShipFrontCollider.SetActive(false);
        }

        // Move the XR Rig along with the spaceship.
        MoveXRPlayer();
    }

    void MoveSpaceship()
    {
        // Calculate the direction to the current waypoint.
        Vector3 direction = waypoints[currentWaypointIndex].position - spaceshipTransform.position;

        // Move the spaceship towards the current waypoint.
        spaceshipTransform.position += direction.normalized * movementSpeed * Time.deltaTime;

        // Smoothly rotate the spaceship towards the current waypoint.
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        spaceshipTransform.rotation = Quaternion.Slerp(spaceshipTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if the spaceship has reached the current waypoint.
        float distanceToWaypoint = Vector3.Distance(spaceshipTransform.position, waypoints[currentWaypointIndex].position);
        if (distanceToWaypoint < 0.1f)
        {
            // Move to the next waypoint.
            currentWaypointIndex++;
        }
    }

    public float landingThreshold; // Adjust this value based on your preference.
    void LandOnStation()
    {
        // Calculate the direction to the landing station.
        Vector3 direction = landingStation.localPosition - spaceshipTransform.localPosition;

        // Move the spaceship towards the landing station.
        spaceshipTransform.position += direction.normalized * movementSpeed * Time.deltaTime;

        // Smoothly rotate the spaceship towards the landing station.
        //Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        //spaceshipTransform.rotation = Quaternion.Slerp(spaceshipTransform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
        spaceshipTransform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        
       

        // Check if the spaceship has reached the landing station within a threshold.
        float distanceToStation = Vector3.Distance(spaceshipTransform.localPosition, landingStation.localPosition);

       
        if (distanceToStation < landingThreshold)
        {
            // Landing completed. Optionally, you can perform actions or transition to a new scene.
            Debug.Log("Spaceship has landed on the station!");
            //this.enabled = false;
            
        }
    }

    void MoveXRPlayer()
    {
        // Calculate the offset between the spaceship and XR Rig positions.
        Vector3 offset = xrRig.transform.position - spaceshipTransform.position;

        // Move the XR Rig along with the spaceship.
        xrRig.transform.position = spaceshipTransform.position + offset;
    }
}