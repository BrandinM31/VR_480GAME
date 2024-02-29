using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowToSurface : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private SphereCollider myCollider;

    [SerializeField]
    private GameObject stickingArrow;

    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
        myCollider.isTrigger = true;

        GameObject arrow = Instantiate(stickingArrow);
        arrow.transform.position = transform.position;
        arrow.transform.forward = transform.forward;

        if (collision.collider.gameObject.CompareTag("Target") == true)
        {
            arrow.transform.parent = collision.transform;
        }
        else
        {
            Destroy(arrow);
        }


        Destroy(gameObject);
    }

}