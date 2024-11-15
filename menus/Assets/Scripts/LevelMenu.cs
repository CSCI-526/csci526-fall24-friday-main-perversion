using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
  public void OpenLevel(int levelId)
  {
    Time.timeScale = 1; 
    string levelName = "Level" + levelId;
    SceneManager.LoadScene(levelName);
  }
}
