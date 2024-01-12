using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponentsWithDelay : MonoBehaviour
{
    [SerializeField]
    private float _delayTimeToStartPlay;

    public OrbSpawner orbSpawnerToStart;
    public Timer timerForTheGame;


    public void StartTheGame()
    {
        StartCoroutine(EnableComponentsToPlay());
    }
    IEnumerator EnableComponentsToPlay()
    {
        yield return new WaitForSeconds(_delayTimeToStartPlay);
        orbSpawnerToStart.enabled = true;   
        timerForTheGame.enabled = true;
    }
}
