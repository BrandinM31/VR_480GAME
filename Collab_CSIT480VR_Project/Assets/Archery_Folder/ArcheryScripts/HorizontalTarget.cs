using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalTarget : MonoBehaviour
{
    public float length = 6f;
    public float speed = 2f;
    private Vector3 center;
    private Vector3 offset;

    void Start()
    {
        center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        offset = new Vector3(speed / 100f, 0f, 0f);
    }

    void Update()
    {

        if (transform.position.x > (center.x + length / 2f))
        {
            transform.position = new Vector3(center.x + length / 2f, transform.position.y, transform.position.z);
            offset.Set(-speed / 100f, 0f, 0f);
        }
        
        if (transform.position.x < (center.x - length/2f))
        {
            transform.position = new Vector3(center.x - length / 2f, transform.position.y, transform.position.z);
            offset.Set(speed / 100f, 0f, 0f);
        }

        transform.position += offset;
    }
}