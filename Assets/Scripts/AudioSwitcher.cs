using UnityEngine;
using UnityEngine.UI;

public class AudioSwitcher : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public Button switchButton;

    private int currentClipIndex = 0;

    void Start()
    {
        // Attach the button click listener
        switchButton.onClick.AddListener(OnSwitchButtonClick);

        // Set the initial audio clip
        audioSource.clip = audioClips[currentClipIndex];
        audioSource.Play();
    }

    void OnSwitchButtonClick()
    {
        // Increment the index or wrap around to the beginning
        currentClipIndex = (currentClipIndex + 1) % audioClips.Length;

        // Set the current audio clip
        audioSource.clip = audioClips[currentClipIndex];

        // Play the updated audio clip
        audioSource.Play();
    }
}