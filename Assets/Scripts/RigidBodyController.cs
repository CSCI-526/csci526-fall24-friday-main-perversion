using System;
using UnityEngine;

public class RigidBodyController : MonoBehaviour
{

    // Camera for 3 axis
    [SerializeField] private GameObject CameraX;
    [SerializeField] private GameObject CameraY;
    [SerializeField] private GameObject CameraZ;

    // Invisible obstacle objects
    [SerializeField] private GameObject IllusionX;
    [SerializeField] private GameObject IllusionY;
    [SerializeField] private GameObject IllusionZ;

    // movement parameter
    [SerializeField] private float Speed;
    private char moveDir;
    private Vector3 PlayerMovementInput;
    
    // rigid body
    [SerializeField] private Rigidbody rb;


    private char cameraIndex = 'z';
    private Matrix4x4 gravity = new Matrix4x4();
    
    // matrix transformation for rotation
    private Matrix4x4 z90Rot = Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90));
    private Matrix4x4 _z90Rot = Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90));
    private Matrix4x4 x90Rot = Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0));
    private Matrix4x4 _x90Rot = Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0));
    private Matrix4x4 y90Rot = Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0));
    private Matrix4x4 _y90Rot = Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0));

    // rotation process parameter
    [SerializeField] float rotSpeed;
    private bool isRotating = false;
    private float targetAngle;
    private char keyPressed;
    private Quaternion initialRotation;
    private string gravityDirection;
    private float process;
    private bool isFalling = false;

    // checkpoint parameter
    public Vector3 respawnPoint;
    
    void Start()
    {
        gravity[0, 3] = 0; // x component
        gravity[1, 3] = -9.8f;     // y component
        gravity[2, 3] = 0;     // z component
        gravity[3, 3] = 1;
        gravityDirection = "_y";
        moveDir = '3';
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling)
        {
            SlowMotionEffect();
        }
        //Debug.Log(isRotating);
        if (isRotating)
        {
            CameraRotation(keyPressed, gravityDirection);
            //return;
        }

        else if (Input.GetKeyDown(KeyCode.S) && !isRotating)
        {
            // Trigger slow-motion rotation for downward rotation (S key)
            isRotating = true;
            keyPressed = 'S';
            initialRotation = transform.rotation;
            targetAngle = transform.eulerAngles.x + 90f;
            gravityDirection = "vertical_down";
        } else if (Input.GetKeyDown(KeyCode.W) && !isRotating)
        {
            // Trigger slow-motion rotation for upward rotation (W key)
            isRotating = true;
            keyPressed = 'W';
            initialRotation = transform.rotation;
            targetAngle = transform.eulerAngles.x - 90f;
            gravityDirection = "vertical_up";
        } 
        
        // TODO: assign new value to gravityDirection should be done in keyPress W and S
        //       current assignment to gravityDirection is just for testing.

        else if (Input.GetKeyDown(KeyCode.E) && (!isRotating))
        {
            Debug.Log("E enter");
            isRotating = true;
            keyPressed = 'E';
            initialRotation = transform.rotation;
            if (cameraIndex == 'z')
            {
                if ((int)gravity[1, 3] == -9)
                {
                    gravityDirection = "_y";
                    targetAngle = transform.eulerAngles.y - 90f;
                } else if ((int)gravity[0, 3] == 9)
                {
                    gravityDirection = "x";
                    targetAngle = transform.eulerAngles.x + 90f;
                }

            } else if (cameraIndex == 'y')
            {
                if ((int)gravity[2, 3] == -9)
                {
                    gravityDirection = "_z";
                    targetAngle = transform.eulerAngles.z - 90f;                    
                } else if ((int)gravity[0, 3] == -9)
                {
                    gravityDirection = "_x";
                    targetAngle = transform.eulerAngles.x - 90f;
                }
                
            } else if (cameraIndex == 'x')
            {
                if ((int)gravity[2, 3] == 9)
                {
                    gravityDirection = "z";
                    targetAngle = transform.eulerAngles.z + 90f;  
                } else if ((int)gravity[1, 3] == 9)
                {
                    gravityDirection = "y";
                    targetAngle = transform.eulerAngles.y + 90f;  
                }

            } 
        } else if (Input.GetKeyDown(KeyCode.Q) && (!isRotating))
        {
            Debug.Log("Q enter");
            isRotating = true;
            keyPressed = 'Q';
            initialRotation = transform.rotation;
            if (cameraIndex == 'z')
            {
                if ((int)gravity[1, 3] == 9)
                {
                    gravityDirection = "y";
                    targetAngle = transform.eulerAngles.y - 90f;
                } else if ((int)gravity[0, 3] == -9)
                {
                    gravityDirection = "x";
                    targetAngle = transform.eulerAngles.x + 90f;
                }

            } else if (cameraIndex == 'y')
            {
                if ((int)gravity[2, 3] == 9)
                {
                    gravityDirection = "_z";
                    targetAngle = transform.eulerAngles.z - 90f; 
                } else if ((int)gravity[0, 3] == 9)
                {
                    gravityDirection = "_x";
                    targetAngle = transform.eulerAngles.x - 90f;
                }
                
            } else if (cameraIndex == 'x')
            {
                if ((int)gravity[2, 3] == -9)
                {
                    gravityDirection = "z";
                    targetAngle = transform.eulerAngles.z + 90f; 
                } else if ((int)gravity[1, 3] == -9)
                {
                    gravityDirection = "y";
                    targetAngle = transform.eulerAngles.y + 90f; 
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
        
        //TODO: Make following code a function called MoveDirectionSwitch(), call this function after
        //      CameraSwitch() and GravityRotation().

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

    void SlowMotionEffect()
    {
        Time.timeScale = 0.5f; 
        Time.fixedDeltaTime = Time.timeScale * 0.02f; 
    }

    void CameraRotation(char keyPressed, string gravityDirection)
    {
        float step = rotSpeed * Time.deltaTime;
        if (gravityDirection == "x" || gravityDirection == "_x")
        {
            process = Mathf.MoveTowardsAngle(transform.eulerAngles.x, targetAngle, step);
            transform.eulerAngles = new Vector3(process, 0, 0);
        }
        else if (gravityDirection == "y" || gravityDirection == "_y")
        {
            process = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, step);
            transform.eulerAngles = new Vector3(0, process, 0);
        }
        else if (gravityDirection == "vertical_up" || gravityDirection == "vertical_down")
        {
            process = Mathf.MoveTowardsAngle(transform.eulerAngles.x, targetAngle, step);
            transform.eulerAngles = new Vector3(process, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        else
        {
            process = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, step);
            transform.eulerAngles = new Vector3(0, 0, process);
        }

        if (Mathf.Approximately(process, targetAngle))
        {
            isRotating = false;
            CameraSwitch(keyPressed);
        }

    }

    void CameraSwitch(char keyPressed) 
    {
        targetAngle = 0f;
        if (keyPressed == 'E')
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
        } else if (keyPressed == 'Q') 
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
        if (rb.velocity.y < 0 && !isFalling)
        {
            isFalling = true;
            SlowMotionEffect();
        }
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
            isFalling = false;
            //back to normal time
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
        if (other.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
        }
    }
}