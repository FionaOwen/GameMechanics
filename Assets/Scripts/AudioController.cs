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

        // Set the initial audio clip

        audioSource.clip = audioClips[currentClipIndex];

        // Assuming the mute/unmute button has a SpriteRenderer component

        //buttonSpriteRenderer = GetComponent<SpriteRenderer>();

    }

  

    public void AudioTransition()
    {
        // Play the current audio clip

        audioSource.Play();

        // Move to the next audio clip in the list

        currentClipIndex = (currentClipIndex + 1) % audioClips.Length;

        // Set the next audio clip

        audioSource.clip = audioClips[currentClipIndex];
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
