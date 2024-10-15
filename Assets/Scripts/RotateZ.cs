using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RotateZ : MonoBehaviour
{
    // Start is called before the first frame update
    // private GameObject map;
    // public Text text;
    int _rotationSpeed = -45;
    bool start = false;
    // void Start()
    // {
    //     //map.transform.position.ToString();
    //     globalPositionX = map.transform.rotation.x;
    //     globalPositionY = map.transform.rotation.y;
    //     globalPositionZ = map.transform.rotation.z;
    // }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            start = !start;
        }
        if (start)
        {
            transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        }
    }
        
}
