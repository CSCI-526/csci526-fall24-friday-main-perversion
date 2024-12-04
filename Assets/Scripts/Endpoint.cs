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
    public TMP_Text livesText; // Reference to the UI text for displaying lives
    public float timer;
    private RigidBodyController rc;

    public Button nextButton;
    private bool levelCompleted = false;
    private bool IsUpdatedOnce = false;

    private int lives = 5; // Default lives count

    private Dictionary<string, string> levelProgression = new Dictionary<string, string>
    {
        { "level1", "level2" },
        { "level2", "level3" },
        { "level3", "level4" },
        { "level4", "level5" },
        { "level5", "level6" },
        { "level6", "level7" },
        { "level7", "level8" },
        { "level8", "level9" },
        { "level9", "main" }
    };

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        Screen.SetActive(false);
        levelCompleted = false; // Reset the completion flag
        Time.timeScale = 1; // Ensure time scale is normal at the start of each level
        nextButton.gameObject.SetActive(true);

        nextButton.onClick.AddListener(LoadNextLevel);
        rc = FindObjectOfType<RigidBodyController>();

        UpdateLivesText(); // Initialize the lives text on the screen
    }

    // Update is called once per frame
    void Update()
    {
        if (levelCompleted) return;

        timer += Time.deltaTime;
        int times_respawn = rc.get_statistic_respawn_time();

        if (times_respawn >= 5 && !levelCompleted)
        {
            HandleLevelEnd(false); // Trigger failure
            return;
        }

        // Check distance to the target object
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = targetObject.transform.position;
        float distance = Vector3.Distance(currentPosition, targetPosition);

        if (distance <= 0.5 && !levelCompleted)
        {
            HandleLevelEnd(true); // Trigger success
        }
    }

    // Method to handle level end logic
    private void HandleLevelEnd(bool isSuccess)
    {
        levelCompleted = true;
        Time.timeScale = 0; // Pause the game
        Screen.SetActive(true);

        int times_rotations = rc.get_statistic_rotation_time();
        int times_respawn = rc.get_statistic_respawn_time();
        float te = rc.get_timer();
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (isSuccess)
        {
            endText.SetText("Success!" + "\n" + "Time:" + timer.ToString("0.00"));
            if (!IsUpdatedOnce)
            {
                StartCoroutine(SendGameData(currentSceneName, te, times_rotations, times_respawn, 1));
                IsUpdatedOnce = true;
            }
        }
        else
        {
            endText.SetText("Failure!" + "\n" + "Time:" + timer.ToString("0.00"));
            if (!IsUpdatedOnce)
            {
                StartCoroutine(SendGameData(currentSceneName, te, times_rotations, times_respawn, 0));
                IsUpdatedOnce = true;
            }
        }
    }

    // Method to update the lives display on the screen
    private void UpdateLivesText()
    {
        livesText.SetText("Lives: " + lives.ToString());
    }

    // Method to decrement lives and check if game over
    public void DecrementLives()
    {
        lives--; // Decrease life count
        UpdateLivesText(); // Update the lives display

        if (lives <= 0)
        {
            HandleLevelEnd(false); // Trigger failure when lives reach 0
        }
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1; // Resume time before loading the next level
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Check if the current scene has a defined next level
        if (levelProgression.ContainsKey(currentSceneName))
        {
            string nextSceneName = levelProgression[currentSceneName];
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("No next level defined for this level!");
        }
    }

    // Static method to send game data to a server or other external destination
    public static IEnumerator SendGameData(string levelName, float timeSpent, int rotationCount, int respawnCount, int completion)
    {
        string appsScriptUrl = "https://script.google.com/macros/s/AKfycbxShQj97O_eUYA_p31ghexzewHlSeKxuT9iODVP1tW2sGtpl7u3xfj4t444zmSMdyiJ/exec";

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
