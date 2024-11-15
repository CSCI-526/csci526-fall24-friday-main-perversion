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

    public void OpenLevel(int levelId)
    {
        Time.timeScale = 1;
        string levelName = "Level" + levelId;
        SceneManager.LoadScene(levelName);
    }

    public void ReturnToMainMenu()
    {
        levelSelectPanel.SetActive(false); // Hide the level select panel
        mainMenuPanel.SetActive(true); // Show the main menu
    }
}
