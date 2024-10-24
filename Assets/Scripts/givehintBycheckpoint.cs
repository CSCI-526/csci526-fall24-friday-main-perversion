using UnityEngine;
using UnityEngine.UI;

public class HintSystem : MonoBehaviour
{
    private Text hintText;  // Drag your Text UI element here in the inspector
    private float hintDuration = 5f; // How long the hint should appear
    private float hintTimer;

    public GameObject targetObject;

    void Start()
    {
        hintText.text = "";  // Start with an empty hint
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        //Debug.Log("end position: " + currentPosition);
        Vector3 targetPosition = targetObject.transform.position;
        //Debug.Log("target position: " + targetPosition);
        float distance = Vector3.Distance(currentPosition, targetPosition);
        //Debug.Log("distence: " + distance);
        if (distance<=0.25){
            hintText.text="press w/s to rotate the gravity";
            hintTimer = hintDuration;
        }

        if (hintTimer > 0)
        {
            hintTimer -= Time.deltaTime;
            if (hintTimer <= 0)
            {
                hintText.text = "";  // Clear the hint when the timer runs out
            }
        }
    }
}