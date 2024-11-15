using UnityEngine;

public class ApplyFrontFaceTexture : MonoBehaviour
{
    public Material frontFaceMaterial;  // The material for the front face (with the image)
    

    void Start()
    {
        // Get the MeshRenderer component attached to the cube
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // Check if the MeshRenderer is found
        if (meshRenderer != null)
        {
            // Create an array of 6 materials, one for each face of the cube
            Material[] materials = new Material[1];

            // Assign the front face material to the front face (index 0)
            materials[0] = frontFaceMaterial;

            // Optionally assign the side face material to the other faces (indices 1-5)

            // Apply the materials to the cube
            meshRenderer.materials = materials;
        }
        else
        {
            Debug.LogError("MeshRenderer component is missing from the cube.");
        }
    }
}
