using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collider col)
    {
        if (col.tag == "Player") 
        {
            
        }
    }
}
