using UnityEngine;
using UnityEngine.UI;

public class TutorialOverlay : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;

    private bool tutorialShown = true;

    private void Start()
    {
        Time.timeScale = 0f; 
        tutorialPanel.SetActive(true);
    }

    private void Update()
    {
        if (tutorialShown && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)))
        {
            HideTutorial();
        }
    }

    private void HideTutorial()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f; 
        tutorialShown = false;


        DucktoryGameManager.Instance.ResetGameState();
    }
}
