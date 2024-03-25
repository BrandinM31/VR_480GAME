using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTennisBotScript : MonoBehaviour
{
    float speed = 40;
    Animator animator;
    //float force = 13;
    public Transform ball;
    public Transform aimTarget;

    Vector3 targetPosition;
    ShotManager shotManager;
    Vector3 aimTargetInitialPosition;
    public Transform[] targets;

    void Start()
    {
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
       // aimTargetInitialPosition = aimTarget.position;
        shotManager = GetComponent<ShotManager>(); // created a refrence to the 
    }

    void Update()
    {
        Move();
    }


    void Move()
    {
        targetPosition.x = ball.position.x; // update the target position to the ball's x position so the bot only moves on the x axis
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

    }

    // The pick Target function will randomly allow the bot to pick a place to return the ball too 
    Vector3 PickTarget()
    {
        int randomValue = Random.Range(0, targets.Length);
        return targets[randomValue].position;
    }

    Shot PickShot() // allows the bot to choose what type of shot it wants to do against the user 
    {
        int randomValue = Random.Range(0, 2);
        if (randomValue == 0)
        
            return shotManager.topSpin;
        else 
        
            return shotManager.flat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Shot currentShot=PickShot();


            Vector3 direction = PickTarget() - transform.position;
            other.GetComponent<Rigidbody>().velocity = direction.normalized * currentShot.hitforce+new Vector3(0, currentShot.upForce,0);            // allows ball to move up when hit

            //try to get direction of aimTarget 
            //The way to het the direction is to subtract the target psition by our transfrom position (player)
            Vector3 ballDirection = ball.position - transform.position;
            if (ballDirection.x > 0)
            {
               animator.Play("Forehand");
            }
            else
            {
               animator.Play("Backhand");
            }
            ball.GetComponent<Ball>().hitter = "BOT";
            aimTarget.position = aimTargetInitialPosition;


        }
    }
}

