using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StringController : MonoBehaviour
{
    [SerializeField]
    private String bowStringRenderer;

    private XRGrabInteractable interactable;

    [SerializeField]
    private Transform midPointGrabObject, midPointVisualObject, midPointParent;

    [SerializeField]
    private float bowStringStretchLimit = 0.3f;

    private Transform interactor;

    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        interactable.selectEntered.AddListener(PrepareBowString);
        interactable.selectExited.AddListener(ResetBowString);
    }

    private void ResetBowString(SelectExitEventArgs arg0)
    {
        interactor = null;
        midPointGrabObject.localPosition = Vector3.zero;
        midPointVisualObject.localPosition = Vector3.zero;
        bowStringRenderer.CreateString(null);
    }

    private void PrepareBowString(SelectEnterEventArgs arg0)
    {
        interactor = arg0.interactorObject.transform;
    }

    private void Update()
    {
        if (interactable != null)
        {
            Vector3 midPointLocalSpace = midPointParent.InverseTransformPoint(midPointGrabObject.position);

            float midPointLocalZAbs = Mathf.Abs(midPointLocalSpace.z);

            HandleStringPushedBackToStart(midPointLocalSpace);
            HandleStringPulledBackToLimit(midPointLocalZAbs, midPointLocalSpace);
            HandlePullingString(midPointLocalZAbs, midPointLocalSpace);

            bowStringRenderer.CreateString(midPointVisualObject.position);
        }
    }

    private void HandleStringPushedBackToStart(Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.z >= 0)
        {
            midPointVisualObject.localPosition = Vector3.zero;
        }
    }

    private void HandleStringPulledBackToLimit(float midPointLocalZAbs, Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.z < 0 && midPointLocalZAbs >= bowStringStretchLimit)
        {
            midPointVisualObject.localPosition = new Vector3(0, 0, -bowStringStretchLimit);
        }
    }

    private void HandlePullingString(float midPointLocalZAbs, Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.z < 0 && midPointLocalZAbs < bowStringStretchLimit)
        {
            midPointVisualObject.localPosition = new Vector3(0, 0, midPointLocalSpace.z);
        }
    }
}