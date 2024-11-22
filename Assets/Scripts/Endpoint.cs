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
    private string appsScriptUrl = "https://script.google.com/macros/s/AKfycbxShQj97O_eUYA_p31ghexzewHlSeKxuT9iODVP1tW2sGtpl7u3xfj4t444zmSMdyiJ/exec";

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
            //endText.SetText("Success!");// + "\n" + "Time:" +  timer.ToString("0.00")+"\nRotated: "+times_rotations+"\nRespawned: "+times_respawn);
            string currentSceneName = SceneManager.GetActiveScene().name;
            
            Time.timeScale = 0; // Pause the game
            levelCompleted = true;

            //LogDataToCSV(currentSceneName, timer, times_rotations, times_respawn, 1);
            if (IsUpdatedOnce == false)
            {
                StartCoroutine(SendGameData(currentSceneName, timer, times_rotations, times_respawn, 1));
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



    // private void LogDataToCSV(string levelName, float timeSpent, int rotationCount, int respawnCount, int completion)
    // {
    //     string filePath = "C:/Users/vibha/game_data.csv";

    //     // If the file does not exist, create it and add headers
    //     if (!File.Exists(filePath))
    //     {
    //         string headers = "LevelName,TimeSpent,RotationCount,RespawnCount,Completion\n";
    //         File.WriteAllText(filePath, headers);
    //     }

    //     // Append the current data as a new line
    //     string newLine = $"{levelName},{timeSpent},{rotationCount},{respawnCount},{completion}\n";
    //     File.AppendAllText(filePath, newLine);
    // }


    public IEnumerator SendGameData(string levelName, float timeSpent, int rotationCount, int respawnCount, int completion) 
    {
        // 构建URL参数
        string url = $"{appsScriptUrl}?" +
            $"levelName={UnityWebRequest.EscapeURL(levelName)}&" +
            $"timeSpent={timeSpent}&" +
            $"rotationCount={rotationCount}&" +
            $"respawnCount={respawnCount}&" +
            $"completion={completion}";
            
        var request = UnityWebRequest.Get(url);
        
        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.Success) 
        {
            Debug.Log($"Data sent successfully: {request.downloadHandler.text}");
        }
        else 
        {
            Debug.LogError($"Error sending data: {request.error}");
            Debug.LogError($"Response: {request.downloadHandler.text}");
        }
        
        request.Dispose();
    }
}
