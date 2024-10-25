using System;
using UnityEngine;
[System.Serializable]
public class TransformData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}
public class RigidBodyController : MonoBehaviour
{

    // Camera for 3 axis
    [SerializeField] private GameObject CameraX;
    [SerializeField] private GameObject CameraY;
    [SerializeField] private GameObject CameraZ;

     private Quaternion xRotation;
    private Quaternion yRotation;
    private Quaternion zRotation;
    private char moveDirCheck;

    // Invisible obstacle objects
    [SerializeField] private GameObject IllusionX;
    [SerializeField] private GameObject IllusionY;
    [SerializeField] private GameObject IllusionZ;


    // movement parameter
    [SerializeField] private float Speed;
    private char moveDir;
    private Vector3 PlayerMovementInput;
    //private DistanceBasedRespawn respawn_checkpoint_script;

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
    private Transform transformRotation;

    // checkpoint parameter
    public Vector3 respawnPoint;
    private char checkpointCameraIndex;

    // analytics parameter

    private Matrix4x4 checkpointGravity = new Matrix4x4();

    void Start()
    {
        gravity[0, 3] = 0; // x component
        gravity[1, 3] = -9.8f;     // y component
        gravity[2, 3] = 0;     // z component
        gravity[3, 3] = 1;
        gravityDirection = "_y";
        moveDir = '3';
        moveDirCheck = moveDir;
        respawnPoint = transform.position;
        checkpointGravity = gravity;
        checkpointCameraIndex = cameraIndex;


        xRotation = CameraX.transform.rotation;
        yRotation = CameraY.transform.rotation;
        zRotation = CameraZ.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isRotating);
        if (isRotating)
        {
            rb.isKinematic = true;
            CameraRotation(keyPressed, gravityDirection, transformRotation);
            //return;
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            isRotating = true;
            keyPressed = 'S';
            // CameraZ.transform.Rotate(0,0,90);
            // CameraY.transform.Rotate(0,0,90);
            // CameraX.transform.Rotate(0,0,90);
            if (cameraIndex == 'z')
            {
                targetAngle = CameraZ.transform.eulerAngles.z + 90f;
                transformRotation = CameraZ.transform;
                CameraY.transform.Rotate(0,0,90);
                CameraX.transform.Rotate(0,0,90);
                gravity = z90Rot * gravity;
            } else if (cameraIndex == 'y')
            {
                targetAngle = CameraY.transform.eulerAngles.y - 90f;
                transformRotation = CameraY.transform;
                CameraZ.transform.Rotate(0,0,90);
                CameraX.transform.Rotate(0,0,90);
                gravity = _y90Rot * gravity;
            } else if (cameraIndex == 'x')
            {
                targetAngle = CameraX.transform.eulerAngles.z + 90f;
                transformRotation = CameraX.transform;
                CameraY.transform.Rotate(0,0,90);
                CameraZ.transform.Rotate(0,0,90);
                gravity = _x90Rot * gravity;
            }
        } else if (Input.GetKeyDown(KeyCode.W))
        {
            isRotating = true;
            keyPressed = 'W';
            // CameraZ.transform.Rotate(0,0,90);
            // CameraY.transform.Rotate(0,0,90);
            // CameraX.transform.Rotate(0,0,90);
            if (cameraIndex == 'z')
            {
                targetAngle = CameraZ.transform.eulerAngles.z - 90f;
                transformRotation = CameraZ.transform;
                CameraY.transform.Rotate(0,0,-90);
                CameraX.transform.Rotate(0,0,-90);
                gravity = _z90Rot * gravity;
            } else if (cameraIndex == 'y')
            {
                targetAngle = CameraY.transform.eulerAngles.y + 90f;
                transformRotation = CameraY.transform;
                CameraZ.transform.Rotate(0,0,-90);
                CameraX.transform.Rotate(0,0,-90);
                gravity = y90Rot * gravity;
            } else if (cameraIndex == 'x')
            {
                targetAngle = CameraX.transform.eulerAngles.z - 90f;
                transformRotation = CameraX.transform;
                CameraY.transform.Rotate(0,0,-90);
                CameraZ.transform.Rotate(0,0,-90);
                gravity = x90Rot * gravity;
            }
        } 
        
        // TODO: assign new value to gravityDirection should be done in keyPress W and S
        //       current assignment to gravityDirection is just for testing.

        else if (Input.GetKeyDown(KeyCode.E) && (!isRotating))
        {
            Debug.Log("E enter");
            isRotating = true;
            keyPressed = 'E';
            initialRotation = transform.rotation;
            transformRotation = transform;
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
            transformRotation = transform;
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
            Physics.gravity = new Vector3(gravity[0, 3], gravity[1, 3], gravity[2, 3]);
            return; 
        }

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

    void CameraRotation(char keyPressed, string gravityDirection, Transform transformRotation)
    {
        float step = rotSpeed * Time.deltaTime;
        
        // Rotation for Gravity Change
        if (keyPressed == 'S' || keyPressed == 'W')
        {
            if (cameraIndex == 'y')
            {
                process = Mathf.MoveTowardsAngle(transformRotation.eulerAngles.y, targetAngle, step);
                //Debug.Log("initialAngle: " + transformRotation.eulerAngles.z + "targetAngle: " + targetAngle + "process: " + process);
                transformRotation.eulerAngles = new Vector3(transformRotation.eulerAngles.x, process, transformRotation.eulerAngles.z);
            } else {
                process = Mathf.MoveTowardsAngle(transformRotation.eulerAngles.z, targetAngle, step);
                //Debug.Log("initialAngle: " + transformRotation.eulerAngles.z + "targetAngle: " + targetAngle + "process: " + process);
                transformRotation.eulerAngles = new Vector3(transformRotation.eulerAngles.x, transformRotation.eulerAngles.y, process);
            }
        } 
        
        // Rotation for Camera Switch
        else if (keyPressed == 'E' || keyPressed == 'Q')
        {
            if (gravityDirection == "x" || gravityDirection == "_x")
            {
                Debug.Log("entered gravity X");
                process = Mathf.MoveTowardsAngle(transformRotation.eulerAngles.x, targetAngle, step);
                transformRotation.eulerAngles = new Vector3(process, 0, 0);
            } else if (gravityDirection == "y" || gravityDirection == "_y")
            {
                Debug.Log("entered gravity Y");
                process = Mathf.MoveTowardsAngle(transformRotation.eulerAngles.y, targetAngle, step);
                transformRotation.eulerAngles = new Vector3(0, process, 0);
            } else
            {
                Debug.Log("entered gravity Z");
                process = Mathf.MoveTowardsAngle(transformRotation.eulerAngles.z, targetAngle, step);
                transformRotation.eulerAngles = new Vector3(0, 0, process);
            }
        }

        if (Mathf.Approximately(process, targetAngle))
        {
            isRotating = false;
            rb.isKinematic = false;
            CameraSwitch(keyPressed);
        }

    }

    void CameraSwitch(char keyPressed) 
    {
        transform.rotation = initialRotation;
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
        //Debug.Log("MoveVector: " + MoveVector);
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

    private void RestoreCheckpointStates()
    {
        cameraIndex = checkpointCameraIndex;
        CameraX.SetActive(cameraIndex == 'x');
        CameraY.SetActive(cameraIndex == 'y');
        CameraZ.SetActive(cameraIndex == 'z');

        // Update illusion objects based on camera
        IllusionX.SetActive(cameraIndex == 'x');
        IllusionY.SetActive(cameraIndex == 'y');
        IllusionZ.SetActive(cameraIndex == 'z');
        moveDir = moveDirCheck;
        gravity = checkpointGravity;
        CameraX.transform.rotation=xRotation;
        CameraY.transform.rotation=yRotation;
        CameraZ.transform.rotation=zRotation;

    }

    // private void UpdateCameraAndIllusions()
    // {
    //     CameraX.SetActive(cameraIndex == 'x');
    //     CameraY.SetActive(cameraIndex == 'y');
    //     CameraZ.SetActive(cameraIndex == 'z');

    //     IllusionX.SetActive(cameraIndex == 'x');
    //     IllusionY.SetActive(cameraIndex == 'y');
    //     IllusionZ.SetActive(cameraIndex == 'z');
    // }
    private void Respawn()
    {
        Debug.Log("New respawn");
        // Move player to respawn point
        transform.position = respawnPoint;
        //if (respawn_checkpoint_script!=null&& respawn_checkpoint_script.HasReached())
        //{

        //    transform.position = respawn_checkpoint_script.GetPosition();
        //}

        // Restore saved camera and gravity states
        RestoreCheckpointStates();

        // Reset rigid body velocity to prevent strange physics behavior
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Fall Detector")
        {
            Respawn();  
            // RestoreCameraAndIllusions(cameraIndex);
        }
        if (other.tag == "Checkpoint")
        {
            respawnPoint = other.transform.position;
            checkpointCameraIndex = cameraIndex;
            checkpointGravity = gravity;
            moveDirCheck = moveDir;
            xRotation = CameraX.transform.rotation;
            yRotation = CameraY.transform.rotation;
            zRotation = CameraZ.transform.rotation;
        }
    }


    public void SetNewRespawnPoint()
    {
        respawnPoint=transform.position;
        checkpointCameraIndex = cameraIndex;
        checkpointGravity = gravity;
        xRotation = CameraX.transform.rotation;
        yRotation = CameraY.transform.rotation;
        zRotation = CameraZ.transform.rotation;
        moveDirCheck = moveDir;
        Debug.Log("CameraX: "+ cameraIndex);
    }



}
