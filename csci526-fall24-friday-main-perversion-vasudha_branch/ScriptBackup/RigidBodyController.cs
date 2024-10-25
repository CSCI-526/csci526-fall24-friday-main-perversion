using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RigidBodyController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 PlayerMovementInput;
    private Vector3 MoveVector;
    [SerializeField] private GameObject CameraZ;
    [SerializeField] private GameObject CameraX;
    [SerializeField] private GameObject CameraY;
    [SerializeField] private float Speed;
    [SerializeField] private Rigidbody rb;

    private char cameraIndex = 'z';
    private char preCamera = 'x';
    private Matrix4x4 gravity = new Matrix4x4();
    private Matrix4x4 z90Rot = Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90));
    private Matrix4x4 _z90Rot = Matrix4x4.Rotate(Quaternion.Euler(0, 0, -90));
    private Matrix4x4 x90Rot = Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0));
    private Matrix4x4 _x90Rot = Matrix4x4.Rotate(Quaternion.Euler(-90, 0, 0));
    private Matrix4x4 y90Rot = Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0));
    private Matrix4x4 _y90Rot = Matrix4x4.Rotate(Quaternion.Euler(0, -90, 0));

    void Start()
    {
        gravity[0, 3] = 0; // x component
        gravity[1, 3] = -9.8f;     // y component
        gravity[2, 3] = 0;     // z component
        gravity[3, 3] = 1;
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

                // if (preCamera == 'x')
                // {
                //     CameraY.transform.Rotate(0,0,90);
                // } else
                // {
                //     CameraY.transform.Rotate(0,0,90);
                // }
                // CameraX.transform.localPosition = (z90Rot * Matrix4x4.Translate(CameraX.transform.localPosition)).GetColumn(3);
                // CameraX.transform.Rotate(90,0,0);
                // CameraY.transform.localPosition = (z90Rot * Matrix4x4.Translate(CameraY.transform.localPosition)).GetColumn(3);
                // CameraY.transform.Rotate(0,90,0);
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
                // CameraZ.transform.Rotate(0,0,-90);
                // if (preCamera == 'x')
                // {
                //     CameraY.transform.Rotate(0,0,-90);
                // } else
                // {
                //     CameraX.transform.Rotate(0,0,-90);
                // }
                // CameraZ.transform.Rotate(0,0,-90);
                // CameraX.transform.localPosition = (_z90Rot * Matrix4x4.Translate(CameraX.transform.localPosition)).GetColumn(3);
                // CameraX.transform.Rotate(-90,0,0);
                // CameraY.transform.localPosition = (_z90Rot * Matrix4x4.Translate(CameraY.transform.localPosition)).GetColumn(3);
                // CameraY.transform.Rotate(0,-90,0);
            } else if (cameraIndex == 'y')
            {
                gravity = y90Rot * gravity;
                // CameraY.transform.Rotate(0,0,-90);
                // if (preCamera == 'z')
                // {
                //     CameraX.transform.Rotate(0,0,-90);
                // } else
                // {
                //     CameraZ.transform.Rotate(0,0,-90);
                // }
            } else if (cameraIndex == 'x')
            {
                gravity = x90Rot * gravity;
                // CameraX.transform.Rotate(0,0,-90);
                // if (preCamera == 'y')
                // {
                //     CameraZ.transform.Rotate(0,0,90);
                // } else
                // {
                //     CameraY.transform.Rotate(0,0,-90);
                // }
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
                } else if ((int)gravity[0, 3] == 9)
                {
                    CameraZ.SetActive(false);
                    CameraY.SetActive(true);
                    cameraIndex = 'y';
                    CameraX.transform.Rotate(0,0,-90);
                }

            } else if (cameraIndex == 'y')
            {
                if ((int)gravity[2, 3] == -9)
                {
                    CameraY.SetActive(false);
                    CameraX.SetActive(true);
                    cameraIndex = 'x';
                    CameraZ.transform.Rotate(0,0,-90);
                } else if ((int)gravity[0, 3] == -9)
                {
                    CameraY.SetActive(false);
                    CameraZ.SetActive(true);
                    cameraIndex = 'z';
                    CameraX.transform.Rotate(0,0,90);
                }
                
            } else if (cameraIndex == 'x')
            {
                if ((int)gravity[2, 3] == 9)
                {
                    CameraX.SetActive(false);
                    CameraY.SetActive(true);
                    cameraIndex = 'y';
                    CameraZ.transform.Rotate(0,0,90);
                } else if ((int)gravity[1, 3] == 9)
                {
                    CameraX.SetActive(false);
                    CameraZ.SetActive(true);
                    cameraIndex = 'z';
                    CameraY.transform.Rotate(0,0,-90);
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
                } else if ((int)gravity[0, 3] == -9)
                {
                    CameraZ.SetActive(false);
                    CameraY.SetActive(true);
                    cameraIndex = 'y';
                    CameraX.transform.Rotate(0,0,-90);
                }
                preCamera = 'z';

            } else if (cameraIndex == 'y')
            {
                if ((int)gravity[2, 3] == 9)
                {
                    CameraY.SetActive(false);
                    CameraX.SetActive(true);
                    cameraIndex = 'x';
                    CameraZ.transform.Rotate(0,0,-90);
                } else if ((int)gravity[0, 3] == 9)
                {
                    CameraY.SetActive(false);
                    CameraZ.SetActive(true);
                    cameraIndex = 'z';
                    CameraX.transform.Rotate(0,0,90);
                }
                
            } else if (cameraIndex == 'x')
            {
                if ((int)gravity[2, 3] == -9)
                {
                    CameraX.SetActive(false);
                    CameraY.SetActive(true);
                    cameraIndex = 'y';
                    CameraZ.transform.Rotate(0,0,90);
                } else if ((int)gravity[1, 3] == -9)
                {
                    CameraX.SetActive(false);
                    CameraZ.SetActive(true);
                    cameraIndex = 'z';
                    CameraY.transform.Rotate(0,0,-90);
                }

            }
        // } else if (Input.GetKeyDown(KeyCode.W))
        // {
        //     if (cameraIndex == 'z')
        //     {
        //         gravity = x90Rot * gravity;
        //         CameraZ.SetActive(false);
        //         CameraY.SetActive(true);
        //         cameraIndex = 'y';
        //     } else if (cameraIndex == 'x')
        //     {

        //     }
        // } else if (Input.GetKeyDown(KeyCode.S))
        // {
        //     if (cameraIndex == 'y')
        //     {
        //         gravity = _x90Rot * gravity;
        //         CameraZ.SetActive(true);
        //         CameraY.SetActive(false);
        //         cameraIndex = 'z';
        //     }
        } else
        {
            MovePlayer();
            return; 
        }
        Physics.gravity = new Vector3(gravity[0, 3], gravity[1, 3], gravity[2, 3]);
        //Debug.Log("Gravity: " + (int)gravity[0, 3] + (int)gravity[1, 3] + (int)gravity[2, 3]);

        MovePlayer();
    }

    void MovePlayer()
    {
        if ((int)gravity[0, 3] == -9) 
        {
            PlayerMovementInput = new Vector3(0f, -Input.GetAxis("Horizontal"), 0f);
        } else if (((int)gravity[0, 3] == 9) && (cameraIndex == 'z'))
        {
            PlayerMovementInput = new Vector3(0f, Input.GetAxis("Horizontal"), 0f);
            //Debug.Log("Reached!");
        } else if ((int)gravity[1, 3] == -9) 
        {
            PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        } else if ((int)gravity[1, 3] == 9)
        {
            PlayerMovementInput = new Vector3(-Input.GetAxis("Horizontal"), 0f, 0f);
        } else if ((int)gravity[2, 3] == -9)
        {
            PlayerMovementInput = new Vector3(0f, 0f, -Input.GetAxis("Horizontal"));
        } else if (((int)gravity[2, 3] == 9) || (((int)gravity[0, 3] == 9) && (cameraIndex == 'x')))
        {
            PlayerMovementInput = new Vector3(0f, 0f, Input.GetAxis("Horizontal"));
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
}
