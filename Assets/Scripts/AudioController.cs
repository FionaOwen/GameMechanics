using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour

{

    public AudioClip[] audioClips;

    private AudioSource audioSource;

    private int currentClipIndex;

    private bool isMuted;

    public Sprite muteSprite;

    public Sprite unmuteSprite;

    public Image buttonSpriteRenderer;

    void Start()

    {

        audioSource = GetComponent<AudioSource>();

        currentClipIndex = 0;

        isMuted = false;
        //audioSource.Play();

        // Set the initial audio clip

        audioSource.clip = audioClips[currentClipIndex];

        // Assuming the mute/unmute button has a SpriteRenderer component

        //buttonSpriteRenderer = GetComponent<SpriteRenderer>();

    }

  

    public void AudioTransition()
    {
        // Increment the index or wrap around to the beginning
        currentClipIndex = (currentClipIndex + 1) % audioClips.Length;

        // Set the current audio clip
        audioSource.clip = audioClips[currentClipIndex];

        // Play the updated audio clip
        audioSource.Play();
    }

    public void ToggleMute()
    {

        isMuted = !isMuted;

        // Mute or unmute the audio

        audioSource.mute = isMuted;

        // Change the button sprite based on mute/unmute state

        if (buttonSpriteRenderer != null)

        {

            buttonSpriteRenderer.sprite = isMuted ? muteSprite : unmuteSprite;

        }

    }

}
