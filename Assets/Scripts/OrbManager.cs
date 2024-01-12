using UnityEngine.Events;
using UnityEngine;
using System.Collections;
using System;

public class OrbManager : MonoBehaviour
{

    public int goodOrbPoints = 10;

    public int negativeOrb1Points = -2;

    public int negativeOrb2Points = -5;

    public float maxOrbCHangingTime;

    private int lastSelectedOrbType = 0; // 0: None, 1: Good Orb, 2: Negative Orb 1, 3: Negative Orb 2

    public Transform[] childOrbs;

    private int currentActiveOrbIndex = 0;

    void OnEnable()
    {
        // Find all child orbs.

        int childCount = transform.childCount;

        childOrbs = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            // Keep all the child orb in the array
            childOrbs[i] = transform.GetChild(i);

        }

        // Enable the first orb initially.

        EnableCurrentOrb();

    }

    private void Start()
    {
        InvokeRepeating("SwitchOrbType", 0.1f, 0.2f);
    }


    public void SelectedOrbtype()
    {
        int currentOrbType = GetOrbType();
        Debug.Log("Current Orb Type" + currentOrbType);

        if (currentOrbType == lastSelectedOrbType)
        {

            HandleConsecutiveSelectionDilemma(currentOrbType);
            

        }
        else
        {
            HandleOrbSelection(currentOrbType);
        }

        lastSelectedOrbType = currentOrbType;

        //SwitchOrbType();
    }
    private void Update()
    {
        TestScore();
    }
    public void TestScore()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            GameManager.Instance.AddScore(goodOrbPoints);
            Debug.Log("G ket is pressed");
        }
    }

    int GetOrbType()
    {
        if (CompareTag("GoodOrb"))
        {
            Debug.Log("Caught Good Orb");
            return 1; // Good Orb

        }
        else if (CompareTag("NegativeOrb1"))
        {
            Debug.Log("Caught Negative Orb 01");
            return 2; // Negative Orb 1

        }
        else if (CompareTag("NegativeOrb2"))
        {
            Debug.Log("Caught Negative Orb 01");
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
        // Logic for consecutive selection dilemma.
        // You can deduct additional points or reset the score here.
        GameManager.Instance.ResetScore();
    }

    private void SwitchOrbType()
    {
        //// Disable the current orb and enable the next one in the cycle.

        //DisableCurrentOrb();

        //MoveToNextOrb();

        //EnableCurrentOrb();

        StartCoroutine(SwithTheOrbType());
    }

    public IEnumerator SwithTheOrbType()
    {
        float waitingTime = maxOrbCHangingTime;
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
        // Move to the next orb in a cyclic manner.

        currentActiveOrbIndex = (currentActiveOrbIndex + 1) % childOrbs.Length;
    }

    public void DestroySelf()
    {
        // Destroy the orb after the specified time.
        Destroy(gameObject);
    }
}
