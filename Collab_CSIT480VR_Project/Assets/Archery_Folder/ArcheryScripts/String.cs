using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class String : MonoBehaviour
{
    [SerializeField]
    private Transform endpt1, endpt2;

    private LineRenderer lineRend;

    private void Awake()
    {
        lineRend = GetComponent<LineRenderer>();
    }

    public void CreateString(Vector3? midPosition)
    {
        Vector3[] linePts = new Vector3[midPosition == null ? 2 : 3];
        linePts[0] = endpt1.localPosition;

        if(midPosition != null)
        {
            linePts[1] = transform.InverseTransformPoint(midPosition.Value);
        }

        linePts[^1] = endpt2.localPosition;

        lineRend.positionCount = linePts.Length;
        lineRend.SetPositions(linePts);
    }

    void Start()
    {
        CreateString(null);
    }
}





