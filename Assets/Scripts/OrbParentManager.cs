using UnityEngine.Events;
using UnityEngine;
using System.Collections;
using System;

public class OrbParentManager : MonoBehaviour
{
    public int goodOrbPoints = 10;
    public int negativeOrb1Points = -2;
    public int negativeOrb2Points = -5;
    public float maxOrbChangingTime;

    private int lastSelectedOrbType = 0; // 0: None, 1: Good Orb, 2: Negative Orb 1, 3: Negative Orb 2
    private int consecutiveNegativeOrb2Count = 0; // Count of consecutive NegativeOrb2 selections
    public Transform[] childOrbs;
    private int currentActiveOrbIndex = 0;
    public int resetOrbArrayPlace;
    public float attractionForce = 500f; // Adjust the force as needed.
    public float orbDestroyTime = 1.0f;  // Time in seconds before the attracted orbs are destroyed.

    public Transform vrHeadset; // Assuming the VR headset represents the hand position.


    void OnEnable()
    {
        int childCount = transform.childCount;
        childOrbs = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            childOrbs[i] = transform.GetChild(i);
        }

        EnableCurrentOrb();
    }

    private void Start()
    {
        //InvokeRepeating("SwitchOrbType", 0.1f, 0.2f);
        vrHeadset = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void SelectedOrbtype()
    {
        int currentOrbType = GetOrbType();
        Debug.Log("Current Orb Type: " + currentOrbType);

        if (currentOrbType == lastSelectedOrbType)
        {
            HandleConsecutiveSelectionDilemma(currentOrbType);
        }
        else
        {
            HandleOrbSelection(currentOrbType);
        }

        lastSelectedOrbType = currentOrbType;
    }



    int GetOrbType()
    {
        // Get the orb type based on the tag of the current active orb
        if (childOrbs[currentActiveOrbIndex].CompareTag("GoodOrb"))
        {
            Debug.Log("Caught Good Orb");
            return 1; // Good Orb
        }
        else if (childOrbs[currentActiveOrbIndex].CompareTag("NegativeOrb1"))
        {
            Debug.Log("Caught Negative Orb 1");
            return 2; // Negative Orb 1
        }
        else if (childOrbs[currentActiveOrbIndex].CompareTag("NegativeOrb2"))
        {
            Debug.Log("Caught Negative Orb 2");
            return 3; // Negative Orb 2
        }

        return 0; // Unknown
    }

    void HandleOrbSelection(int orbType)
    {
        switch (orbType)
        {
            case 1: // Good Orb
                GameManager.Instance.AddScore(goodOrbPoints);
                break;
            case 2: // Negative Orb 1
                GameManager.Instance.AddScore(negativeOrb1Points);
                break;
            case 3: // Negative Orb 2
                GameManager.Instance.AddScore(negativeOrb2Points);
                break;
        }
    }

    //void HandleOrbSelection(int orbType)
    //{
    //    switch (orbType)
    //    {
    //        case 1: // Good Orb
    //            AttractAndDestroyOrb(childOrbs[currentActiveOrbIndex]);
    //            GameManager.Instance.AddScore(goodOrbPoints);
    //            break;
    //        case 2: // Negative Orb 1
    //            AttractAndDestroyOrb(childOrbs[currentActiveOrbIndex]);
    //            GameManager.Instance.AddScore(negativeOrb1Points);
    //            break;
    //        case 3: // Negative Orb 2
    //            AttractAndDestroyOrb(childOrbs[currentActiveOrbIndex]);
    //            GameManager.Instance.AddScore(negativeOrb2Points);
    //            break;
    //    }
    //}

    void AttractAndDestroyOrb(Transform orbTransform)
    {
        // Calculate the direction from the orb to the VR headset (simplified hand position).
        Vector3 attractionDirection = vrHeadset.position - orbTransform.position;

        // Apply a force to attract the orb towards the VR headset.
        orbTransform.GetComponent<Rigidbody>().AddForce(attractionDirection.normalized * attractionForce);
        Debug.Log("Rigidbody of " + orbTransform.name);

        // Optionally, you can also rotate the orb to face the VR headset.
        orbTransform.LookAt(vrHeadset.position);

        // Attach a script to the orb to handle collision with the VR headset.
        OrbCollisionHandler orbCollisionHandler = orbTransform.gameObject.AddComponent<OrbCollisionHandler>();
        //orbCollisionHandler.SetOrbParentManager(this); // Pass a reference to the OrbParentManager.

        // No need to destroy the orb here.
        // Destroy(orbTransform.gameObject, orbDestroyTime);
    }

    void HandleConsecutiveSelectionDilemma(int orbType)
    {
        if (orbType == 3) // Check if the current orb is Negative Orb 2
        {
            consecutiveNegativeOrb2Count++;

            if (consecutiveNegativeOrb2Count >= 2)
            {
                // Reset the entire score if NegativeOrb2 is selected two times simultaneously
                GameManager.Instance.ResetScore();
                consecutiveNegativeOrb2Count = 0; // Reset the count
            }
        }
        else
        {
            // Reset the count if a different orb is selected
            consecutiveNegativeOrb2Count = 0;
        }
    }

    private void SwitchOrbType()
    {
        StartCoroutine(SwithTheOrbType());
    }

    public IEnumerator SwithTheOrbType()
    {
        float waitingTime = maxOrbChangingTime;
        DisableCurrentOrb();
        yield return new WaitForSeconds(waitingTime);
        MoveToNextOrb();
        yield return new WaitForSeconds(waitingTime);
        EnableCurrentOrb();
        yield return new WaitForSeconds(waitingTime);
        Debug.Log("Inside coroutine");
    }

    void EnableCurrentOrb()
    {
        childOrbs[currentActiveOrbIndex].gameObject.SetActive(true);
    }

    void DisableCurrentOrb()
    {
        childOrbs[currentActiveOrbIndex].gameObject.SetActive(false);
    }

    void MoveToNextOrb()
    {
        currentActiveOrbIndex = (currentActiveOrbIndex + 1) % childOrbs.Length;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
