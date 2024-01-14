using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnableComponentsWithDelay : MonoBehaviour
{
    [SerializeField]
    private float _delayTimeToStartPlay;
    public TextMeshProUGUI startTimer;
    public GameObject startTimerCanvas;
    public GameObject scoreandTimerCanvas;

    public OrbSpawner orbSpawnerToStart;
    public Timer timerForTheGame;

    private void Update()
    {
        _delayTimeToStartPlay -= Time.deltaTime;
        startTimer.text = _delayTimeToStartPlay.ToString("0") + " sec";
        if (_delayTimeToStartPlay <= 0)
        {
            startTimerCanvas.SetActive(false);
            scoreandTimerCanvas.SetActive(true);
            StartCoroutine(EnableComponentsToPlay());

        }
    }
    //public void StartTheGame()
    //{
    //    StartCoroutine(EnableComponentsToPlay());
    //}
    IEnumerator EnableComponentsToPlay()
    {
        orbSpawnerToStart.enabled = true;   
        timerForTheGame.enabled = true;
        yield return new WaitForSeconds(0);
    }
}
