using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public Transform aimTarget; // the target where we aim to land the ball
    float speed = .7f; // move speed
    float force = 6; // ball impact force

    bool hitting; // boolean to know if we are hitting the ball or not 
    public Transform ball; // the ball 

    Animator animator; //refrence to the animator for use 

    public XRController xrController;
    private void Start()
    {
        animator = GetComponent<Animator>(); // referennce out animator
        xrController = GetComponent<XRController>();

    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // get the horizontal axis of the keyboard
        float v = Input.GetAxisRaw("Vertical"); // get the vertical axis of the keyboard
//######################################################################
        // Check if the TOUCHPAD button is pressed
        //    if (Input.touchPressureSupported)
        //    {
        //        hitting = true;
        //   }
        //   else if(!Input.touchPressureSupported)

        //   {
        //     hitting = false;
        // }
//#########################################################################
        if (Input.GetKeyDown(KeyCode.F))
        {
            hitting = true; // we are trying to hit the ball and aim where to make it land
            
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            hitting = false; // we let go of the key so we are not hitting anymore and this 
        }                    // is used to alternate between moving the aim target and ourself

        if (hitting)
        {
            aimTarget.Translate(new Vector3(h,0,0) * speed * Time.deltaTime);
        }

        if ( (h != 0 || v != 0 ) && hitting ) 
        {
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Vector3 direction = aimTarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = direction.normalized * force + new Vector3(0,6,0);            // allows ball to move up when hit

            //try to get direction of aimTarget 
            //The way to het the direction is to subtract the target psition by our transfrom position (player)
            Vector3 ballDirection = ball.position - transform.position;
            if (ballDirection.x >0)
            {
                animator.Play("Forehand");
                Debug.Log("Forehand"); //testing in console
            }
            else
            {
                animator.Play("Backhand");
                Debug.Log("Backhand");
            }
            

            // maybe use animation to make it do nothing, but the user will be doing the actual movement. Animate backhand for user 
            //Try to get direction of ball. Left or right? Backhand or forehand hit?

        }


    }



}


