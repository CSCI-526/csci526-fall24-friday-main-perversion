using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointController : MonoBehaviour
{
    // Start is called before the first frame update
    public Material color1;
    public Image targetImage;
    //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //Material sphereMaterial = sphere.GetComponent<Renderer>().material;
    public bool checkpointReached;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Renderer>().material.color = Color.blue;
            targetImage.color = Color.blue;
            checkpointReached = true;
        }
    }
}
