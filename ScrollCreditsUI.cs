using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollCredits : MonoBehaviour
{
    [SerializeField] private RectTransform scrollingPanel;
    [SerializeField] private float scrollSpeed = 50f;
    [SerializeField] private float endYPosition = 2000f; 
    [SerializeField] private float delayBeforeSceneChange = 2f;
    [SerializeField] private string nextSceneName = ""; 
    private bool isScrolling = true;


    void Update()
    {
        if (!isScrolling) return;

        scrollingPanel.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (scrollingPanel.anchoredPosition.y >= endYPosition)
        {
            isScrolling = false;

            if (!string.IsNullOrEmpty(nextSceneName))
            {
                Invoke(nameof(FadeOutToNextScene), delayBeforeSceneChange);
            }
        }
    }

    private void FadeOutToNextScene()
    {
        if (SceneFader.Instance != null)
        {
            SceneFader.Instance.FadeToScene(nextSceneName);
        }
        else
        {
            SceneManager.LoadScene(nextSceneName); // fallback if SceneFader not in scene
        }
    }
}
