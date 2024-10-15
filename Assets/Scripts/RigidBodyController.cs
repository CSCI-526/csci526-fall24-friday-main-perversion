using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class RigidBodyController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 PlayerMovementInput;
    private char moveDir;
    [SerializeField] private GameObject CameraX;
    [SerializeField] private GameObject CameraY;
    [SerializeField] private GameObject CameraZ;

    [SerializeField] private GameObject IllusionX;
    [SerializeField] private GameObject IllusionY;
    [SerializeField] private GameObject IllusionZ;

    [SerializeField] private float Speed;
    [SerializeField] private Rigidbody rb;


    private char cameraIndex = 'z';
    private Matrix4x4 gravity = new Matrix4x4();
    private Matrix4x4 z90Rot = Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90));
    private Matrix4x4 _z90Rot = Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90));
    private Matrix4x4 x90Rot = Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0));
    private Matrix4x4 _x90Rot = Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0));
    private Matrix4x4 y90Rot = Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0));
    private Matrix4x4 _y90Rot = Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0));

    public Vector3 respawnPoint;

    void Start()
    {
        gravity[0, 3] = 0; // x component
        gravity[1, 3] = -9.8f;     // y component
        gravity[2, 3] = 0;     // z component
        gravity[3, 3] = 1;
        moveDir = '3';
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CameraX.transform.localPosition);
        if (Input.GetKeyDown(KeyCode.S))
        {
            CameraZ.transform.Rotate(0,0,90);
            CameraY.transform.Rotate(0,0,90);
            CameraX.transform.Rotate(0,0,90);
            if (cameraIndex == 'z')
            {
                gravity = z90Rot * gravity;
            } else if (cameraIndex == 'y')
            {
                gravity = _y90Rot * gravity;
            } else if (cameraIndex == 'x')
            {
                gravity = _x90Rot * gravity;
            }
        } else if (Input.GetKeyDown(KeyCode.W))
        {
            CameraZ.transform.Rotate(0,0,-90);
            CameraY.transform.Rotate(0,0,-90);
            CameraX.transform.Rotate(0,0,-90);
            if (cameraIndex == 'z')
            {
                gravity = _z90Rot * gravity;
            } else if (cameraIndex == 'y')
            {
                gravity = y90Rot * gravity;
            } else if (cameraIndex == 'x')
            {
                gravity = x90Rot * gravity;
            }
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            if (cameraIndex == 'z')
            {
                if ((int)gravity[1, 3] == -9)
                {
                    CameraZ.SetActive(false);
                    CameraX.SetActive(true);
                    cameraIndex = 'x';
                    CameraY.transform.Rotate(0,0,90);
                    IllusionX.SetActive(true);
                    IllusionY.SetActive(false);
                    IllusionZ.SetActive(false);
                } else if ((int)gravity[0, 3] == 9)
                {
                    CameraZ.SetActive(false);
                    CameraY.SetActive(true);
                    cameraIndex = 'y';
                    CameraX.transform.Rotate(0,0,-90);
                    IllusionY.SetActive(true);
                    IllusionX.SetActive(false);
                    IllusionZ.SetActive(false);
                }

            } else if (cameraIndex == 'y')
            {
                if ((int)gravity[2, 3] == -9)
                {
                    CameraY.SetActive(false);
                    CameraX.SetActive(true);
                    cameraIndex = 'x';
                    CameraZ.transform.Rotate(0,0,-90);
                    IllusionX.SetActive(true);
                    IllusionZ.SetActive(false);
                    IllusionY.SetActive(false);
                } else if ((int)gravity[0, 3] == -9)
                {
                    CameraY.SetActive(false);
                    CameraZ.SetActive(true);
                    cameraIndex = 'z';
                    CameraX.transform.Rotate(0,0,90);
                    IllusionZ.SetActive(true);
                    IllusionZ.SetActive(false);
                    IllusionY.SetActive(false);
                }
                
            } else if (cameraIndex == 'x')
            {
                if ((int)gravity[2, 3] == 9)
                {
                    CameraX.SetActive(false);
                    CameraY.SetActive(true);
                    cameraIndex = 'y';
                    CameraZ.transform.Rotate(0,0,90);
                    IllusionY.SetActive(true);
                    IllusionZ.SetActive(false);
                    IllusionX.SetActive(false);
                } else if ((int)gravity[1, 3] == 9)
                {
                    CameraX.SetActive(false);
                    CameraZ.SetActive(true);
                    cameraIndex = 'z';
                    CameraY.transform.Rotate(0,0,-90);
                    IllusionZ.SetActive(true);
                    IllusionY.SetActive(false);
                    IllusionX.SetActive(false);
                }

            } 
        } else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (cameraIndex == 'z')
            {
                if ((int)gravity[1, 3] == 9)
                {
                    CameraZ.SetActive(false);
                    CameraX.SetActive(true);
                    cameraIndex = 'x';
                    CameraY.transform.Rotate(0,0,90);
                    IllusionX.SetActive(true);
                    IllusionY.SetActive(false);
                    IllusionZ.SetActive(false);
                } else if ((int)gravity[0, 3] == -9)
                {
                    CameraZ.SetActive(false);
                    CameraY.SetActive(true);
                    cameraIndex = 'y';
                    CameraX.transform.Rotate(0,0,-90);
                    IllusionY.SetActive(true);
                    IllusionX.SetActive(false);
                    IllusionZ.SetActive(false);
                }

            } else if (cameraIndex == 'y')
            {
                if ((int)gravity[2, 3] == 9)
                {
                    CameraY.SetActive(false);
                    CameraX.SetActive(true);
                    cameraIndex = 'x';
                    CameraZ.transform.Rotate(0,0,-90);
                    IllusionX.SetActive(true);
                    IllusionZ.SetActive(false);
                    IllusionY.SetActive(false);
                } else if ((int)gravity[0, 3] == 9)
                {
                    CameraY.SetActive(false);
                    CameraZ.SetActive(true);
                    cameraIndex = 'z';
                    CameraX.transform.Rotate(0,0,90);
                    IllusionZ.SetActive(true);
                    IllusionX.SetActive(false);
                    IllusionY.SetActive(false);
                }
                
            } else if (cameraIndex == 'x')
            {
                if ((int)gravity[2, 3] == -9)
                {
                    CameraX.SetActive(false);
                    CameraY.SetActive(true);
                    cameraIndex = 'y';
                    CameraZ.transform.Rotate(0,0,90);
                    IllusionY.SetActive(true);
                    IllusionZ.SetActive(false);
                    IllusionX.SetActive(false);
                } else if ((int)gravity[1, 3] == -9)
                {
                    CameraX.SetActive(false);
                    CameraZ.SetActive(true);
                    cameraIndex = 'z';
                    CameraY.transform.Rotate(0,0,-90);
                    IllusionZ.SetActive(true);
                    IllusionY.SetActive(false);
                    IllusionX.SetActive(false);
                }

            }
        } else
        {
            MovePlayer();
            return; 
        }
        Physics.gravity = new Vector3(gravity[0, 3], gravity[1, 3], gravity[2, 3]);
        //Debug.Log("Gravity: " + (int)gravity[0, 3] + (int)gravity[1, 3] + (int)gravity[2, 3]);

        // If gravity or camera has been changed, check which move direction should be apply.
        if (((int)gravity[0, 3] == -9 && (cameraIndex == 'z')) || ((int)gravity[2, 3] == -9 && (cameraIndex == 'x')))
        {
            moveDir = '1';
        } else if (((int)gravity[0, 3] == 9 && (cameraIndex == 'z')) || ((int)gravity[2, 3] == 9 && (cameraIndex == 'x')))
        {
            moveDir = '2';
        } else if (((int)gravity[1, 3] == -9 && (cameraIndex == 'z')) || ((int)gravity[2, 3] == -9 && (cameraIndex == 'y'))) 
        {
            moveDir = '3';
        } else if (((int)gravity[1, 3] == 9 && (cameraIndex == 'z')) || ((int)gravity[2, 3] == 9 && (cameraIndex == 'y')))
        {
            moveDir = '4';
        } else if (((int)gravity[1, 3] == -9 && (cameraIndex == 'x')) || ((int)gravity[0, 3] == 9 && (cameraIndex == 'y')))
        {
            moveDir = '5';
        } else if (((int)gravity[1, 3] == 9 && (cameraIndex == 'x')) || ((int)gravity[0, 3] == -9 && (cameraIndex == 'y')))
        {
            moveDir = '6';
        }
    }

    void MovePlayer()
    {
        if (moveDir == '1')
        {
            PlayerMovementInput = new Vector3(0f, -Input.GetAxis("Horizontal"), 0f);
        } else if (moveDir == '2')
        {
            PlayerMovementInput = new Vector3(0f, Input.GetAxis("Horizontal"), 0f);
        } else if (moveDir == '3')
        {
            PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        } else if (moveDir == '4')
        {
            PlayerMovementInput = new Vector3(-Input.GetAxis("Horizontal"), 0f, 0f);
        } else if (moveDir == '5')
        {
            PlayerMovementInput = new Vector3(0f, 0f, Input.GetAxis("Horizontal"));
        } else if (moveDir == '6')
        {
            PlayerMovementInput = new Vector3(0f, 0f, -Input.GetAxis("Horizontal"));
        }

        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        Debug.Log("MoveVector: " + MoveVector);
        if (MoveVector.x != 0f)
        {
            rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, rb.velocity.z);
        } else if (MoveVector.y != 0f)
        {
            rb.velocity = new Vector3(rb.velocity.x, MoveVector.y, rb.velocity.z);
        } else if (MoveVector.z != 0f)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, MoveVector.z);
        } 
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Fall Detector")
        {
            transform.position = respawnPoint;
        }
        if (other.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
        }
    }
}
