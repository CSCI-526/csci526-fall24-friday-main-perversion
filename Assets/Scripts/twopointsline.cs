using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class twopointsline : MonoBehaviour
{
    public Transform p1;
    public Transform p2;

    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.01f;  // Set start width
        lineRenderer.endWidth = 0.01f;    // Set end width
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount=2;
        lineRenderer.SetPosition(0,p1.position);
        lineRenderer.SetPosition(1,p2.position);
    }
}
