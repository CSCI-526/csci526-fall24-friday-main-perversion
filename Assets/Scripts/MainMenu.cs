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

}

