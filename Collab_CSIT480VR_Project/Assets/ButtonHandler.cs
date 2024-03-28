using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

using UnityEngine.XR.Interaction.Toolkit;

public class InputTester : MonoBehaviour
{
     [SerializeField] XRController controller; // Reference to your XR Controller in the scene

    private bool triggerPressed = false;

    void start()
    {

    }
    void Update()
    {
        // Check if the trigger button is pressed
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerValue) && triggerValue)
        {
            if (!triggerPressed)
            {
                triggerPressed = true;
                OnTriggerPressed();
            }
        }
        else
        {
            if (triggerPressed)
            {
                triggerPressed = false;
                OnTriggerReleased();
            }
        }
 
    }

    // Called when the trigger button is pressed
    private void OnTriggerPressed()
    {
        Debug.Log("Trigger button pressed.");
    }

    // Called when the trigger button is released
    private void OnTriggerReleased()
    {
        Debug.Log("Trigger button released.");
    }
}
