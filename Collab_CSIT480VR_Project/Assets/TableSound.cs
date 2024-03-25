using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSound : MonoBehaviour
{
    private AudioSource audioSource2; // Change type to AudioSource
    private void Start()
    {
        audioSource2 = GetComponent<AudioSource>(); // Get the AudioSource component
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ball")) // if the ball hits a wall
        {
            audioSource2.Play(); // Plays the audio 
        }
    }
}