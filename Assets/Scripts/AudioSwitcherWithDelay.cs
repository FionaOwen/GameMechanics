using UnityEngine;
using UnityEngine.UI;

public class AudioSwitcherWithDelay : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public Button switchButton;
    public float delayInSeconds = 2f;

    private int currentClipIndex = 0;

    void Start()
    {

        // Set the initial audio clip
        audioSource.clip = audioClips[currentClipIndex];
        audioSource.Play();

        PlayNextAudio();
    }

    void PlayNextAudio()
    {
        // Increment the index or wrap around to the beginning
        currentClipIndex = (currentClipIndex + 1) % audioClips.Length;

        // Invoke a method to play the audio with a delay
        Invoke("PlayDelayedAudio", delayInSeconds);
    }

    void PlayDelayedAudio()
    {
        // Set the current audio clip
        audioSource.clip = audioClips[currentClipIndex];

        // Play the updated audio clip
        audioSource.Play();
    }

}