using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private GameObject optionsUIObject;

    private void Awake()
    {
        if (newGameButton != null)
        {
            newGameButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Cutscene"); 
            });
        }

        if (playButton != null)
        {
            playButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("LevelSelection"); 
            });
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }

        if (optionsButton != null)
        {
            optionsButton.onClick.AddListener(() =>
            {
                if (OptionsUI.Instance != null)
                {
                    OptionsUI.Instance.Show(() =>
                    {
                        gameObject.SetActive(true); 
                    });

                    gameObject.SetActive(false); 
                }
            });
        }
    }

    private void Start()
    {
        if (optionsUIObject != null)
        {
            optionsUIObject.SetActive(false);
        }
    }
}

