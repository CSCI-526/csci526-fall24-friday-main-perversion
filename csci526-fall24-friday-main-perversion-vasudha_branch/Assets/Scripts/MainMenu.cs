using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{  
    public GameObject mainMenuPanel; // Assign in Inspector
    public GameObject levelSelectPanel; // Assign in Inspector

    public void PlayGame()
    {
        mainMenuPanel.SetActive(false); // Hide the main menu
        levelSelectPanel.SetActive(true); // Show the level select panel
    }

    public void SelectLevel(string levelName)
    {
        SceneManager.LoadScene(levelName); // Load the selected level scene
    }

    public void ReturnToMainMenu()
    {
        levelSelectPanel.SetActive(false); // Hide the level select panel
        mainMenuPanel.SetActive(true); // Show the main menu
    }


    public void PauseGame()
    {
        Time.timeScale = 0; // Pause the game
        // Show pause menu logic
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Resume the game
        // Hide pause menu logic
    }

    public void ExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
