using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject Screen;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        // Debug.Log("end position: " + currentPosition);
        Vector3 targetPosition = targetObject.transform.position;
        // Debug.Log("target position: " + targetPosition);
        float distance = Vector3.Distance(currentPosition, targetPosition);
        // Debug.Log("distence: " + distance);
        if (distance<=0.25){
            GetComponent<Renderer>().material.color = Color.red;
            Screen.SetActive(true);
        }
    }
}
