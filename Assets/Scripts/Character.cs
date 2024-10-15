using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController characterController;
    private int gravityZ = 0;
    public char cameraIndex;
    private List<Vector3> ZRotate = new List<Vector3>
    {new Vector3(0, -2, 0), new Vector3(2, 0, 0), new Vector3(0, 2, 0), new Vector3(-2, 0, 0)};
    
    public float Speed = 5f;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraIndex = 'z';
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (cameraIndex == 'z')
            {
                gravityZ ++;
                if (gravityZ == ZRotate.Count)
                {
                    gravityZ = 0;
                }
            }
        } 
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (cameraIndex == 'z')
            {
                gravityZ --;
                if (gravityZ == -1)
                {
                    gravityZ = ZRotate.Count - 1;
                }
            }
        }


        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), -2, 0);
        characterController.Move(move * Time.deltaTime * Speed);
    }
}
