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

    [SerializeField]
    private int score1, score2, score3, score4, score5;

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

            switch (collision.gameObject.name)
            {
                case "Bullseye":
                    Debug.Log("Bullseye: " + score1.ToString());
                    AddToScore(score1);
                    break;
                case "One":
                    Debug.Log("Ring One: " + score2.ToString());
                    AddToScore(score2);
                    break;
                case "Two":
                    Debug.Log("Ring Two: " + score3.ToString());
                    AddToScore(score3);
                    break;
                case "Three":
                    Debug.Log("Ring Three: " + score4.ToString());
                    AddToScore(score4);
                    break;
                case "Four":
                    Debug.Log("Ring Four: " + score5.ToString());
                    AddToScore(score5);
                    break;
            }
        }
        else
        {
            Destroy(arrow);
        }


        Destroy(gameObject);
    }

    void AddToScore(int score)
    {
        FindObjectOfType<UpdateScore>().AddScore(score);
    }

}
