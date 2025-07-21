using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            DucktoryGameManager.Instance.TogglePauseGame();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenuScene"); 
        });

        optionsButton.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.Instance.Show(() =>
            {
                Show();


            });
        });
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);

        DucktoryGameManager.Instance.OnGamePaused += DucktoryGameManager_OnGamePaused;
        DucktoryGameManager.Instance.OnGameUnpaused += DucktoryGameManager_OnGameUnpaused;

        Hide();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    private void DucktoryGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void DucktoryGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
