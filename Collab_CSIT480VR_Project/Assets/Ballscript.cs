using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{

    public int playerServeCount = 0; // Counter to keep track of player's serves
    public int botServeCount = 0; // Counter to keep track of bot's serves

    Vector3 initialPos; // ball's initial position
    private AudioSource audioSource; // Change type to AudioSource

    //track who hits the ball. (BOT or human)
    public string hitter;

    [SerializeField] Transform BotServeRight;
    [SerializeField] Transform BotServeLeft;
    [SerializeField] Transform PlayerserveRight;
    [SerializeField] Transform PlayerserveLeft;
    bool servedRight = true;

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
                    BotReset();
                }
                else if (hitter == "BOT") // if the player is the last person to hit the ball out then... 
                {
                    playerScore++;
                    PlayerReset();
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
                        BotReset();
                    }
                    else if (hitter == "BOT") // if the player is the last person to hit the ball out then... 
                    {
                        playerScore++;
                        PlayerReset();
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

        if (other.CompareTag("out") && playing)
        {

            if (hitter == "player") // if the player is the last person to hit the ball out then... 
            {
                playerScore++;
                PlayerReset();
            }
            else if (hitter == "BOT") // if the player is the last person to hit the ball out then... 
            {
                botScore++;
                BotReset();
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

        if (playerScore >= 12 || botScore >= 12)
        {
            gameOver();
        }
    }


    public void ServeSequence()
    {
        for (int i = 0; i < 5; i++)
        {
            // Player serves
            
            playerServeCount++;
            PlayerReset();
            if (playerServeCount == 5)
                break; // Exit the loop if the player has served three times

            // Bot serves
            
            botServeCount++;
            BotReset();
            // Check if the bot has served three times
            if (botServeCount == 5)
                break; // Exit the loop if the bot has served three times
        }
        playerServeCount = 0;
        botServeCount = 0;
    }
    public void PlayerReset() // after each "out" there will be a reset of position
    {
        if (PlayerserveRight)
            transform.position = PlayerserveLeft.position;
        else
            transform.position = PlayerserveRight.position;
        servedRight = false;
    }

    public void BotReset() // after each "out" there will be a reset of position
    {
        if (BotServeRight)
            transform.position = BotServeLeft.position;
        else
            transform.position = BotServeRight.position;
        servedRight = false;
    }

    public void gameOver()
    {
        if (playerScore >= 12 || botScore >= 12)
        {
            gameObject.SetActive(false); // Hide the ball
            GameObject canvas = GameObject.Find("scoringCanvas"); // Assuming the UI Text will be on a Canvas GameObject

            if (canvas != null)
            {
                GameObject textPrefab = new GameObject("GameOverText");
                textPrefab.transform.SetParent(canvas.transform, false);

                Text gameOverText = textPrefab.AddComponent<Text>();
                gameOverText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                gameOverText.fontSize = 55;
                gameOverText.alignment = TextAnchor.MiddleRight;
                gameOverText.color = Color.red;
                gameOverText.fontStyle = FontStyle.Bold;
                gameOverText.horizontalOverflow = HorizontalWrapMode.Wrap;
                gameOverText.verticalOverflow = VerticalWrapMode.Truncate;
                if (playerScore >= 12)
                {
                    gameOverText.text = "YOU WIN";
                }
                else
                {
                    gameOverText.text = "YOU LOSE";
                }
            }
            else
            {
                Debug.LogError("Canvas not found. Make sure you have a Canvas GameObject in your scene named 'Canvas'.");
            }
        }
    }

}