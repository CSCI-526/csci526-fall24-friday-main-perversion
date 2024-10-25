using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject Screen;
    public TMP_Text endText;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        Screen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 currentPosition = transform.position;
        // Debug.Log("end position: " + currentPosition);
        Vector3 targetPosition = targetObject.transform.position;
        // Debug.Log("target position: " + targetPosition);
        float distance = Vector3.Distance(currentPosition, targetPosition);
        // Debug.Log("distence: " + distance);
        if (distance<=0.25){
            GetComponent<Renderer>().material.color = Color.red;
            Screen.SetActive(true);
            endText.SetText("Success!" + "\n\n" + "Time Spent:" + '\n' + timer.ToString("0.00"));
            Time.timeScale = 0; // Pause the game
        }
    }
}
