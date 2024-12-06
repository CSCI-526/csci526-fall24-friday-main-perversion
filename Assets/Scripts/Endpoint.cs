using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject Screen;
    public TMP_Text endText;
    public float timer;
    private RigidBodyController rc;

    public Button nextButton;
    public Button livesButton;
    private TMP_Text livesText;

    private int remainingLives = 5;
    private bool levelCompleted = false; // Prevent multiple level ends
    private bool isFailureProcessed = false; // Specific flag for failure handling
    private int lastRespawnCount = 0;

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

    void Start()
    {
        timer = 0.0f;
        Screen.SetActive(false);

        levelCompleted = false; // Reset the completion flag
        isFailureProcessed = false; // Reset failure flag
        Time.timeScale = 1;     // Ensure time scale is normal at the start of each level

        nextButton.gameObject.SetActive(true);
        nextButton.onClick.AddListener(LoadNextLevel);

        rc = FindObjectOfType<RigidBodyController>();

        if (livesButton != null)
        {
            livesText = livesButton.GetComponentInChildren<TMP_Text>();
            if (livesText != null)
            {
                UpdateLivesButtonText();
            }
        }
    }

    void Update()
    {
        if (levelCompleted) return;

        // Increment the timer
        timer += Time.deltaTime;

        // Get the current respawn count from the player controller
        int times_respawn = rc.get_statistic_respawn_time();

        // Trigger respawn handling only when the count increases
        if (times_respawn > lastRespawnCount)
        {
            HandlePlayerRespawn(times_respawn);
            lastRespawnCount = times_respawn;
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

    private void HandlePlayerRespawn(int currentRespawnCount)
    {
        if (remainingLives > 0)
        {
            remainingLives--; // Decrement lives
            UpdateLivesButtonText(); // Update the text
        }

        // Trigger failure only when lives are depleted and failure hasn't been processed
        if (remainingLives <= 0 && !isFailureProcessed)
        {
            Debug.Log("No lives left. Triggering level failure.");
            HandleLevelEnd(false); // Trigger failure
        }
    }

    private void UpdateLivesButtonText()
    {
        if (livesText != null)
        {
            livesText.text = $"Lives: {remainingLives}";
        }
    }

    private void HandleLevelEnd(bool isSuccess)
    {
        if (levelCompleted) return; // Ensure this function is called only once

        levelCompleted = true; // Mark level as completed
        Time.timeScale = 0;    // Pause the game
        Screen.SetActive(true);

        int times_rotations = rc.get_statistic_rotation_time();
        int times_respawn = rc.get_statistic_respawn_time();
        float te = rc.get_timer();
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (isSuccess)
        {
            endText.SetText("Success!" + "\n" + "Time:" + timer.ToString("0.00"));
            StartCoroutine(SendGameData(currentSceneName, te, times_rotations, times_respawn, 1));
        }
        else
        {
            // Mark failure as processed to prevent duplicates
            isFailureProcessed = true;

            endText.SetText("Failure!" + "\n" + "Time:" + timer.ToString("0.00"));
            nextButton.gameObject.SetActive(false);
            StartCoroutine(SendGameData(currentSceneName, te, times_rotations, times_respawn, 0));
        }
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1; // Resume time before loading the next level
        string currentSceneName = SceneManager.GetActiveScene().name;

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
        }

        request.Dispose();
    }
}
