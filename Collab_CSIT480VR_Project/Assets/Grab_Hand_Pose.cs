using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Grab_Hand_Pose : MonoBehaviour
{
    public grip_hand_data rigthHandPose;

 
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(SetupPose);

        rigthHandPose.gameObject.SetActive(false);
    }

    public void SetupPose(BaseInteractionEventArgs arg)
    { 
        if(arg.interactableObject is XRDirectInteractor)
        {
            grip_hand_data handData = arg.interactableObject.transform.GetComponent<grip_hand_data>();
            handData.animator.enabled = false;
        }

    }
}
