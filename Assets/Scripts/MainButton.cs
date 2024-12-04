using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement; // To load scenes
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class MainButton : MonoBehaviour
{
    private RigidBodyController rc;
    // This function will be called when the button is clicked
    public void OnMainMenuButtonClick()
    {
        GameObject obj = GameObject.Find("Player");
        rc= obj.GetComponent<RigidBodyController>();
        string currentSceneName = SceneManager.GetActiveScene().name;
        int times_rotations = rc.get_statistic_rotation_time();
        int times_respawn = rc.get_statistic_respawn_time();
        float timer=rc.get_timer();
        StartCoroutine(SendGameData(currentSceneName, timer, times_rotations, times_respawn, 0));
        // Assuming "MainMenu" is the name of the scene for your main menu
        SceneManager.LoadScene("Main");
    }

    public static IEnumerator SendGameData(string levelName, float timeSpent, int rotationCount, int respawnCount, int completion)
    {
        // ����URL����
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
