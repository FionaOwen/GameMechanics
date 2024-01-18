using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public float delayInSeconds;
    private void Start()
    {

        Invoke("PlayDelayedAudio", delayInSeconds);
    }


    void PlayDelayedAudio()
    {
        // Play the updated audio clip
        audioSource.Play();
    }
}

