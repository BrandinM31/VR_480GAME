using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    private Transform target; // Variable to hold the target transform

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Check if hand model GameObject is assigned
        if (handModel == null)
        {
            Debug.LogError("Hand model GameObject is not assigned to HandPresencePhysics script!");
            return; // Exit Start method to prevent further execution
        }

        // Set the hand model's transform as the target
        target = handModel.transform;
    }

    public GameObject handModel; // Public variable to hold the hand model GameObject

    void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogError("Target transform is not assigned!");
            return; // Exit FixedUpdate method to prevent further execution
        }

        //position
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        //rotation
        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
