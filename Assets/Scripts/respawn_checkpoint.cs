using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceBasedRespawn : MonoBehaviour
{
    public GameObject targetObject;
    private Vector3 currentRespawnPoint;
    private float detectionRange = 0.5f;
    Boolean reached_once=false;

    void Start()
    {
        currentRespawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
            CheckDistanceToTarget();
    }

     private void CheckDistanceToTarget()
    {
        if (targetObject != null)
        {
            float distance = Vector3.Distance(transform.position, targetObject.transform.position);

            // If the player is within the detection range, update the respawn point
            if (distance <= detectionRange)
            {
                SetNewRespawnPoint(targetObject.transform.position);           
            }
        }
    }

    private void SetNewRespawnPoint(Vector3 newRespawnPosition)
    {
        currentRespawnPoint = newRespawnPosition;
        GetComponent<Renderer>().material.color = Color.blue;
        Debug.Log("New respawn point set at: " + newRespawnPosition);
        reached_once = true;
    }

    public Boolean HasReached()
    {
        return reached_once;
    }
    public Vector3 GetPosition()
    {
        return currentRespawnPoint;
    }

}
