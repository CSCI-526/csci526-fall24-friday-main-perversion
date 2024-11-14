using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject Screen;
    public TMP_Text endText;
    public float timer;
    private RigidBodyController rc;
    //public int rotation_times;

    public Button nextButton;

    private bool levelCompleted = false; // Track if level is completed
    // Start is called before the first frame update

    private Dictionary<string, string> levelProgression = new Dictionary<string, string>
    {
        { "level1", "level2" },
        { "level2", "level3" },
        { "level3", "level4" },
        { "level4", "level5" },
        { "level5", "level6" },
        { "level6", "main"}
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
        rc = FindObjectOfType<RigidBodyController>();

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
        if (distance <= 0.5)
        {
            GetComponent<Renderer>().material.color = Color.red;
            Screen.SetActive(true);
            int times_rotations=rc.get_statistic_rotation_time();
            int times_respawn=rc.get_statistic_respawn_time();
            endText.SetText("Success!" + "\n" + "Time:" +  timer.ToString("0.00")+"\nRotated: "+times_rotations+"\nRespawned: "+times_respawn);
            string currentSceneName = SceneManager.GetActiveScene().name;
            
            Time.timeScale = 0; // Pause the game
            levelCompleted = true;
            LogDataToCSV(currentSceneName, timer, times_rotations, times_respawn, 1);
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



    private void LogDataToCSV(string levelName, float timeSpent, int rotationCount, int respawnCount, int completion)
    {
        string filePath = "C:/Users/vibha/game_data.csv";

        // If the file does not exist, create it and add headers
        if (!File.Exists(filePath))
        {
            string headers = "LevelName,TimeSpent,RotationCount,RespawnCount,Completion\n";
            File.WriteAllText(filePath, headers);
        }

        // Append the current data as a new line
        string newLine = $"{levelName},{timeSpent},{rotationCount},{respawnCount},{completion}\n";
        File.AppendAllText(filePath, newLine);
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
