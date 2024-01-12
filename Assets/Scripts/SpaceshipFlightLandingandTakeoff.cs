using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpaceshipFlightLandingandTakeoff : MonoBehaviour
{
    public Transform[] takeoffWaypoints; // Waypoints for the spaceship's take-off.
    public Transform[] traversalWaypoints; // Waypoints for the spaceship's traversal.
    public Transform[] landingWaypoints; // Waypoints for the spaceship's landing.
    public float waitDurationAfterLanding = 5f; // Duration to wait after landing.
    public float takeoffSpeed = 5f; // Speed of the spaceship take-off.
    public float movementSpeed = 5f; // Speed of the spaceship movement.
    public float rotationSpeed = 5f; // Speed of the spaceship rotation.

    public GameObject spaceshipFrontMirror;
    public GameObject spaceShipLadder;

    public Transform spaceshipTransform; // Reference to the spaceship's transform.

    private int currentWaypointIndex = 0; // Index of the current waypoint.
    private bool inWaitPhase = false; // Flag indicating whether the spaceship is in the wait phase.

    void Start()
    {
        //spaceshipTransform = transform.Find("Spaceship"); // Adjust the name accordingly.

        // Start the sequence.
        StartCoroutine(StartSpaceshipSequence());
    }

    // Coroutine for the spaceship sequence.
    IEnumerator StartSpaceshipSequence()
    {
        MoveSpaceship(landingWaypoints);

        // Wait for the specified duration after landing.
        yield return new WaitForSeconds(waitDurationAfterLanding);

        // Transition to take-off phase.
        inWaitPhase = false;
    }

    void Update()
    {
        if (currentWaypointIndex < takeoffWaypoints.Length && !inWaitPhase)
        {
            MoveSpaceship(takeoffWaypoints);
        }
        else if (currentWaypointIndex < traversalWaypoints.Length)
        {
            MoveSpaceship(traversalWaypoints);
        }
        else
        {
            // Spaceship has completed its journey.
            spaceshipFrontMirror.SetActive(false);
            Debug.Log("Spaceship has completed its journey!");
        }

        
    }

    void MoveSpaceship(Transform[] waypoints)
    {
        if (currentWaypointIndex < waypoints.Length)
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
        else
        {
            // If all waypoints are reached, enter the wait phase.
            inWaitPhase = true;
        }
    }

}
