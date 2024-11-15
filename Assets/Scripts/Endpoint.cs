using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using System;

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
    private bool IsUpdatedOnce = false;
    // Start is called before the first frame update
    private string url = "https://script.google.com/macros/s/AKfycbzUjlXfv8KkvwrKqnXOVOPc-D5qIvpJ_rC_U9eEMf2mJZJl0MFh1_c23xHCzdmHXG5fNg/exec";

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



    private class GameData
    {
        public string levelName;
        public float timeSpent;
        public int rotationCount;
        public int respawnCount;
        public int completion;

        public GameData(string levelName, float timeSpent, int rotationCount, int respawnCount, int completion)
        {
            this.levelName = levelName;
            this.timeSpent = timeSpent;
            this.rotationCount = rotationCount;
            this.respawnCount = respawnCount;
            this.completion = completion;
        }
    }


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

            //LogDataToCSV(currentSceneName, timer, times_rotations, times_respawn, 1);
            if (IsUpdatedOnce == false)
            {
                StartCoroutine(LogDataToGoogleSheets(currentSceneName, timer, times_rotations, times_respawn, 1));
            }
            if (levelCompleted == true)
            {
                IsUpdatedOnce = true;
            }
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


    public IEnumerator LogDataToGoogleSheets(string levelName, float timeSpent, int rotationCount, int respawnCount, int completion)
    {
        GameData gameData = new GameData(levelName, timeSpent, rotationCount, respawnCount, completion);
        string jsonData = JsonUtility.ToJson(gameData);
        Debug.Log("JSON data being sent: " + jsonData);
        private string script_url="https://script.google.com/macros/s/AKfycbwuvVPaBtEXaCi8vSrNOlhVOVlDPs9Nkjon-hfrG8OYyf1OnsNomz3ArhFh9lH9DPU/exec";
        using (UnityWebRequest request = new UnityWebRequest(script_url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            request.SetRequestHeader("Access-Control-Allow-Origin", "*");

            request.SetRequestHeader("Origin", "https://distr1ct9.github.io");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to log data: " + request.error);
            }
            else
            {
                Debug.Log("Data logged successfully to Google Sheets");
            }
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
