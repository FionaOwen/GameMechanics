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
        InvokeRepeating("SwitchOrbType", 0.1f, 0.2f);
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

    private void Update()
    {
        TestScore();
    }

    public void TestScore()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            // Call the AddScore method on the current active orb
            GameManager.Instance.AddScore(goodOrbPoints);
            Debug.Log("G key is pressed");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.Instance.AddScore(negativeOrb1Points);
        }
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
