using UnityEngine;

using UnityEngine.XR.Interaction.Toolkit;
 
public class GravityGun : XRGrabInteractable

{

    private bool isActivated = false;

    protected override void OnSelectEntered(SelectEnterEventArgs args)

    {

        base.OnSelectEntered(args);

        isActivated = true;

        // Activate the gravitational field when the trigger is pressed.

        ActivateGravitationalField(true);

    }

    protected override void OnSelectExited(SelectExitEventArgs args)

    {

        base.OnSelectExited(args);

        isActivated = false;

        // Deactivate the gravitational field when the trigger is released.

        ActivateGravitationalField(false);

    }

    private void ActivateGravitationalField(bool activate)

    {

        // Implement the activation of the gravitational field at the gun's tip.

        // You may use particle effects, change materials, or manipulate the visual appearance.

        // Implement this based on your design preferences.

    }

}
