using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    Vector3 initialPos; // ball's initial position
    private AudioSource audioSource; // Change type to AudioSource

    //track who hits the ball. (BOT or human)
    public string hitter;


    //scoring
    int playerScore;
    int botScore;

    [SerializeField] Text playerScoreText; // So you can see it in the ball component options
    [SerializeField] Text botScoreText; // So you can see it in the ball component options

    //we have to stop giving points unitil the ball is back into play 

    public bool playing = true; // public so we can access it it in the palyerScript

    private void Start()
    {
        initialPos = transform.position; // default it to where we first place it in the scene
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        //Starting score
        playerScore = 0;
        botScore = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall")) // if the ball hits a wall
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero; // reset it's velocity to 0 so it doesn't move anymore
            transform.position = initialPos; // reset it's position 
            
            GameObject.Find("UserPaddle").GetComponent<Player>().Reset(); //when the ball hits the "wall" it will call the reset function cretaed in the player script
            if (playing)
            {
                if (hitter == "player") // if the player is the last person to hit the ball out then... 
                {
                    botScore++;
                }
                else if (hitter == "BOT") // if the player is the last person to hit the ball out then... 
                {
                    playerScore++;
                }
                playing = false;
                updateScores();
            }
            else if (collision.transform.CompareTag("out") && collision.transform.CompareTag("Wall")) // if the ball hits a wall
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero; // reset it's velocity to 0 so it doesn't move anymore
                transform.position = initialPos; // reset it's position 

                GameObject.Find("UserPaddle").GetComponent<Player>().Reset(); //when the ball hits the "wall" it will call the reset function cretaed in the player script

                if (playing)
                {
                    if (hitter == "player") // if the player is the last person to hit the ball out then... 
                    {
                        botScore++;
                    }
                    else if (hitter == "BOT") // if the player is the last person to hit the ball out then... 
                    {
                        playerScore++;
                    }
                    playing = false;
                    updateScores();
                }

            }
        }
        //AUDIO
        if (collision.transform.CompareTag("UserPaddle") && collision.transform.CompareTag("TableTennisFloor")) // if the ball hits a wall
        {
            audioSource.Play(); // Plays the audio 
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("out") && playing )
        {

            if (hitter == "player") // if the player is the last person to hit the ball out then... 
            {
                playerScore++;
            }
            else if (hitter == "BOT") // if the player is the last person to hit the ball out then... 
            {
                botScore++;
            }
            playing = false;
            updateScores();

        }

    }
    //function to update live scores 
    void updateScores()
    {
        playerScoreText.text = "player : " + playerScore;
        botScoreText.text = "BOT : " + botScore;
    }

    public void gameOver()
    {
        //Play trophy animation or loser animation
        //Them
        // Make UI come up for player options. Copy and paste UI from 


    }
}