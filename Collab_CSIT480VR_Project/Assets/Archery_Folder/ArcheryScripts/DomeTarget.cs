using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomeTarget : MonoBehaviour
{
    public float radius = 5f;
    public float speed = 2f;
    private float angle = 0f;
    private Vector3 center;
    private Vector3 offset;

    void Start()
    {
        center = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        offset = new Vector3(0f, 0f, 0f);
    }

    void Update()
    {
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        offset.Set(x, 0f, z);

        transform.position = center + offset;

        angle += (speed * Time.deltaTime);

        if (angle > 2 * Mathf.PI)
        {
            angle -= 2 * Mathf.PI;
        }
    }
}