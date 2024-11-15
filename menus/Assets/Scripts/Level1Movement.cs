using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level1Movement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public TextMeshProUGUI instructionText;
    public Transform target;
    public float targetReachedThreshold = 0.5f;  // Distance threshold to consider reaching the target
    // Start is called before the first frame update
    void Start()
    {
        if (instructionText != null)
        {
            instructionText.text = "Press A/D to move. Reach the green ball!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get player input for horizontal movement
        float move = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        
        transform.Translate(move, 0f, 0f);

        CheckIfPlayerReachedTarget();
    }

    void CheckIfPlayerReachedTarget()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Check if the player collided with the target (sphere)
        if (distanceToTarget <= targetReachedThreshold)
        {
            // Display success message
            if (instructionText != null)
            {
                instructionText.text = "You reached the green ball!";
            }
        }
    }

}
