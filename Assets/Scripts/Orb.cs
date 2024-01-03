using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Orb : XRGrabInteractable
{
    public int pointValue = 10; // Set the point value for each orb.

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        if (args.interactor is XRDirectInteractor)
        {
            // Implement the capturing logic when the grip button is released.
            // You may destroy the orb, increment the player's score, etc.
            CaptureOrb();
        }
    }

    private void CaptureOrb()
    {
        // Implement the logic for capturing the orb.
        // You may add points to the player's score, play sound effects, etc.
        // Adjust this based on your design preferences.
    }
}