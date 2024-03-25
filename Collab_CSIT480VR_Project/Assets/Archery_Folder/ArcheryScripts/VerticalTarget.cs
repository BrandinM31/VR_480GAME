using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalTarget : MonoBehaviour
{
    public float length = 6f;
    public float speed = 2f;
    public float direction = 1f;
    private Vector3 center;
    private Vector3 offset;

    void Start()
    {
        center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        offset = new Vector3(0f, direction*speed / 100f, 0f);
    }

    void Update()
    {

        if (transform.position.y > (center.y + length / 2f))
        {
            transform.position = new Vector3(transform.position.x, center.y + length / 2f, transform.position.z);
            offset.Set(0f, -speed / 100f, 0f);
        }

        if (transform.position.y < (center.y - length / 2f))
        {
            transform.position = new Vector3(transform.position.x, center.y - length / 2f, transform.position.z);
            offset.Set(0f, speed / 100f, 0f);
        }

        transform.position += offset;
    }
}