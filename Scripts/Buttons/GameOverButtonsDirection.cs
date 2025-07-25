using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtonsDirection : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneFader.Instance.FadeAndRestart(); 
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
