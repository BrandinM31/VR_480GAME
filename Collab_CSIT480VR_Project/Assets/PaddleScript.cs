using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class UserPaddleController : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private XRBaseInteractor interactor;

    private void Start()
    {
        // Get the XRGrabInteractable component attached to the paddle
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Subscribe to the select enter event to know when the paddle is grabbed
        grabInteractable.onSelectEntered.AddListener(OnGrabbed);

        // Subscribe to the select exit event to know when the paddle is released
        grabInteractable.onSelectExited.AddListener(OnReleased);
    }

    private void OnGrabbed(XRBaseInteractor interactor)
    {
        // Store the interactor (the hand) that grabbed the paddle
        this.interactor = interactor;

        // Attach the paddle to the interactor's attach transform for smooth movement
        grabInteractable.attachTransform = interactor.attachTransform;
    }

    private void OnReleased(XRBaseInteractor interactor)
    {
        // Reset the stored interactor when the paddle is released
        this.interactor = null;
    }

    private void Update()
    {
        // If the paddle is currently grabbed by an interactor (hand)
        if (interactor != null)
        {
            // Get the position of the interactor (hand)
            Vector3 handPosition = interactor.transform.position;

            // Set the position of the paddle to the hand's position
            transform.position = handPosition;
        }
    }
}
