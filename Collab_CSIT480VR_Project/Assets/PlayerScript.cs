using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public Transform aimTarget; // the target where we aim to land the ball
    float speed = 32;//move speed
  
    bool hitting; // boolea5n to know if we are hitting the ball or not 
    public Transform ball; // the ball 
    Animator animator; //refrence to the animator for use 
    public XRController xrController;
    ShotManager shotManager;
    Shot currentShot;

    // allows the player start on the left or right side of the table (continously)
    [SerializeField] Transform serveRight;
    [SerializeField] Transform serveLeft;
    bool servedRight = true;

    private void Start()
    {
        animator = GetComponent<Animator>(); // referennce out animator
        xrController = GetComponent<XRController>();

        //hitting the ball 
        shotManager = GetComponent<ShotManager>();
        currentShot = shotManager.topSpin;

    }
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // get the horizontal axis of the keyboard
        float v = Input.GetAxisRaw("Vertical"); // get the vertical axis of the keyboard

        // Getting user input
        //Controller.Device device = Controller.Input((int)trackedObj.index);


        if (Input.GetKeyDown(KeyCode.F))
        {
            hitting = true; // we are trying to hit the ball and aim where to make it land
            currentShot = shotManager.topSpin;

        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            hitting = false; // we let go of the key so we are not hitting anymore and this 
        }                    // is used to alternate between moving the aim target and ourself


        if (Input.GetKeyDown(KeyCode.E)) //IF USER IS CLICKING e
        {
            hitting = true; 
            currentShot = shotManager.flat;

        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            hitting = false; 
        }

        //Serving the ball
        if (Input.GetKeyDown(KeyCode.R))//USER has option to serve
        {
            hitting = true;
            currentShot = shotManager.flatServe;
            GetComponent<BoxCollider>().enabled = false; //gets rid of collider on the paddle. So we can give our own force to serve

        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            hitting = false;
            GetComponent<BoxCollider>().enabled = true;
            ball.transform.position = transform.position + new Vector3(0.3f, 1, 0);
            Vector3 direction = aimTarget.position - transform.position;
            ball.GetComponent<Rigidbody>().velocity = direction.normalized * currentShot.hitforce + new Vector3(0, currentShot.upForce, 0);
        }


        if (Input.GetKeyDown(KeyCode.T))// has option to serve
        {
            hitting = true;
            currentShot = shotManager.kickServe;
            GetComponent<BoxCollider>().enabled = false; //gets rid of collider on the paddle. So we can give our own force to serve

        }
        else if (Input.GetKeyUp(KeyCode.T))
        {
            hitting = false;
            GetComponent<BoxCollider>().enabled = true;
            ball.transform.position = transform.position + new Vector3(0.3f, 1, 0);
            Vector3 direction = aimTarget.position - transform.position;
            ball.GetComponent<Rigidbody>().velocity = direction.normalized * currentShot.hitforce + new Vector3(0, currentShot.upForce, 0);

            ball.GetComponent<Ball>().hitter = "UserPaddle";
        }

        if (hitting)
        {
            aimTarget.Translate(new Vector3(h,0,0) * speed * 2 * Time.deltaTime);
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
            other.GetComponent<Rigidbody>().velocity = direction.normalized * currentShot.hitforce + new Vector3(0, currentShot.upForce ,0);            // allows ball to move up when hit

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

            ball.GetComponent<Ball>().hitter = "UserPaddle";
            ball.GetComponent<Ball>().playing = true;
            // maybe use animation to make it do nothing, but the user will be doing the actual movement. Animate backhand for user 
            //Try to get direction of ball. Left or right? Backhand or forehand hit?
        }
    }

    public void Reset() // after each "out" there will be a reset of position
    {
        if (servedRight)
        transform.position = serveLeft.position;
        else
            transform.position = serveRight.position;
        servedRight = !servedRight;

    }
}


