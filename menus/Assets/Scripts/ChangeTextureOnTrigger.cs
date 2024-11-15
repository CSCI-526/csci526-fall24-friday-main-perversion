using UnityEngine;

public class ChangeTextureOnTrigger : MonoBehaviour
{
    public Texture newTexture;  // The texture to display when the player is in the trigger area
    private Material blockMaterial;  // The material of the block

    private void Start()
    {
        // Get the material of the block (MeshRenderer component)
        blockMaterial = GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger area (using Player tag)
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger, changing texture.");
            // Change the texture of the block when the player enters
            blockMaterial.mainTexture = newTexture;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exits the trigger area (using Player tag)
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger, resetting texture.");
            // Reset the texture to the default one when the player exits
            blockMaterial.mainTexture = null;  // Or set it to another default texture if you have one
        }
    }
}
