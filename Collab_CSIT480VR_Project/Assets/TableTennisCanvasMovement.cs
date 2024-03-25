using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTennisCanvasMovement : MonoBehaviour
{
    public float moveSpeed = 0.1f; // Adjust this value to control the speed of the cube.

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0,0) * moveSpeed * Time.deltaTime;
       transform.Translate(movement);
    }
}