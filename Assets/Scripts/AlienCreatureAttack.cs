using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienCreatureAttack : MonoBehaviour
{
    public int attackScoreReductionValue;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.Instance.AddScore(attackScoreReductionValue);
        }
    }
}
