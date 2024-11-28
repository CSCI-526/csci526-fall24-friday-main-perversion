using UnityEngine;
using UnityEngine.SceneManagement; // To load scenes

public class MainButton : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void OnMainMenuButtonClick()
    {
        // Assuming "MainMenu" is the name of the scene for your main menu
        SceneManager.LoadScene("Main");
    }
}
