using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointController : MonoBehaviour
{
    private Vector3 currentRespawnPoint;
    private RigidBodyController playerController;
    private bool reached_once = false;
    void Start()
    {
        currentRespawnPoint = transform.position;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Debug.Log("Find the player!");
            playerController = player.GetComponent<RigidBodyController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!reached_once)
        {
            if (other.tag == "Player")
            {
                GetComponent<Renderer>().material.color = Color.blue;
                playerController.SetNewRespawnPoint();
            }
        }
    }
}
