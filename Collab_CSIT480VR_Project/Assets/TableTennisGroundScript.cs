using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTennisGround : MonoBehaviour
{    private AudioSource GroundAudioSource; // Change type to AudioSource

    private void Start()
    {
        GroundAudioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        //AUDIO
        if (collision.transform.CompareTag("TableTennisFloor") && collision.transform.CompareTag("out") && collision.transform.CompareTag("Wall")) // if the ball hits a Ground
        {
            GroundAudioSource.Play(); // Plays the audio 
        }

    }
}