using UnityEngine;
using UnityEngine.UI;  // Needed for working with UI Image components

public class ShowInstructions : MonoBehaviour
{
    public GameObject instructionImage;  // Reference to the instruction image UI element
    public string targetTag = "Player";  // Tag of the player (set this tag in Unity)

    private void Start()
    {
        // Initially hide the instructions
        instructionImage.SetActive(false);
    }

    // Triggered when something enters the collider of this GameObject
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.Log("Player entered trigger");
            ShowInstruction(true);  // Show the instruction image when the player is on the block
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.Log("Player exited trigger");
            ShowInstruction(false);  // Hide the instruction image when the player leaves the block
        }
    }


    // Method to show or hide the instruction image
    private void ShowInstruction(bool show)
    {
        instructionImage.SetActive(show);
    }
}
