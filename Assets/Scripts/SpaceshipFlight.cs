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
    private XRGrabInteractable xrGrabInteractable; // Reference to the XR Grab Interactable component.
    [SerializeField]
    private GameObject xrRig; // Reference to the XR Rig component.

    private int currentWaypointIndex = 0; // Index of the current waypoint.

    void Start()
    {
        //spaceshipTransform = transform.Find("Spaceship"); // Adjust the name accordingly.
        //xrGrabInteractable = GetComponent<XRGrabInteractable>();
        //xrRig = FindObjectOfType<XRRig>(); // Assuming there's only one XR Rig in the scene.

        // Set the spaceship as kinematic initially to prevent physics interference.
        //xrGrabInteractable.GetComponent<Rigidbody>().isKinematic = true;
    }

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

    void LandOnStation()
    {
        // Calculate the direction to the landing station.
        Vector3 direction = landingStation.position - spaceshipTransform.position;

        // Move the spaceship towards the landing station.
        spaceshipTransform.position += direction.normalized * movementSpeed * Time.deltaTime;

        // Smoothly rotate the spaceship towards the landing station.
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        spaceshipTransform.rotation = Quaternion.Slerp(spaceshipTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if the spaceship has reached the landing station.
        float distanceToStation = Vector3.Distance(spaceshipTransform.position, landingStation.position);
        if (distanceToStation < 0.1f)
        {
            // Landing completed. Optionally, you can perform actions or transition to a new scene.
            Debug.Log("Spaceship has landed on the station!");
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