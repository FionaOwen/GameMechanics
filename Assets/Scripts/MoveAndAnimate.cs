using UnityEngine;

public class MoveAndAnimate : MonoBehaviour
{
    public string targetTag ; // Tag of the GameObject to move towards
    public float movementSpeed; // Speed at which the GameObject moves
    public float stoppingDistance; // Distance at which the animation should play

    private Transform target;
    public Animator animator;
    private bool hasReachedTarget = false;

    private void Start()
    {
        // Find the target GameObject based on the specified tag
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);

        // Ensure the target GameObject has a transform component
        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            Debug.LogError("Target GameObject with tag '" + targetTag + "' not found.");
        }

        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target == null)
            return;

        // Calculate the distance target position and this gameobject
        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;

        // Check if the distance is within the stopping distance
        if (distance <= stoppingDistance)
        {
            // Stop moving and play the animation
            hasReachedTarget = true;
            if (hasReachedTarget)
            {
                animator.Play("Tail Swipe"); // Set the animation name to be played once it reaches destination.
            }
        }
        else
        {
            // Move towards the target
            transform.Translate(direction.normalized * movementSpeed * Time.deltaTime, Space.World);
            // Look at the target
            transform.LookAt(target);
        }
    }
}
