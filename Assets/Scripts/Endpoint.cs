using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject Screen;
    public TMP_Text endText;
    public float timer;

    public Button nextButton;

    private bool levelCompleted = false; // Track if level is completed
    // Start is called before the first frame update

    private Dictionary<string, string> levelProgression = new Dictionary<string, string>
    {
        { "level1", "level2" },
        { "level2", "level3" },
        { "level3", "level4" },
        { "level4", "level5" },
        { "level5", "level6" }
        // Add more levels as needed
    };

    void Start()
    {
        timer = 0.0f;
        Screen.SetActive(false);

        levelCompleted = false; // Reset the completion flag
        Time.timeScale = 1; // Ensure time scale is normal at the start of each level

        nextButton.gameObject.SetActive(true);

        nextButton.onClick.AddListener(LoadNextLevel);

    }

    // Update is called once per frame
    void Update()
    {
        if (levelCompleted) return;
        timer += Time.deltaTime;
        Vector3 currentPosition = transform.position;
        // Debug.Log("end position: " + currentPosition);
        Vector3 targetPosition = targetObject.transform.position;
        // Debug.Log("target position: " + targetPosition);
        float distance = Vector3.Distance(currentPosition, targetPosition);
        // Debug.Log("distence: " + distance);
        if (distance <= 0.25)
        {
            GetComponent<Renderer>().material.color = Color.red;
            Screen.SetActive(true);
            endText.SetText("Success!" + "\n\n" + "Time Spent:" + '\n' + timer.ToString("0.00"));
            Time.timeScale = 0; // Pause the game
            levelCompleted = true;
        }
        //Time.timeScale = 1;

    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1; // Resume time before loading the next level
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Check if the current scene has a defined next level
        if (levelProgression.ContainsKey(currentSceneName))
        {
            string nextSceneName = levelProgression[currentSceneName];
            //StartCoroutine(LoadSceneAsync(nextSceneName));
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("No next level defined for this level!");
        }
    }

    /*private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the scene is loaded
        }
    }*/
}
