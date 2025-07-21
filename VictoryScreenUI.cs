using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject victoryScreenCanvas;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private string nextSceneName = "Garden";
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        victoryScreenCanvas.SetActive(false); 
        nextLevelButton.onClick.AddListener(GoToNextLevel);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    public void ShowVictoryScreen()
    {
        victoryScreenCanvas.SetActive(true);
        Time.timeScale = 0f; 
    }

    private void GoToNextLevel()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(nextSceneName);
    }

    private void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene"); 
    }


}
