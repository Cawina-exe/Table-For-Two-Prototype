using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
  

    public void LoadLevel(string sceneName)
    {
        Debug.Log("Trying to load: " + sceneName);

        Time.timeScale = 1f;

        if (SceneFader.Instance != null)
        {
            SceneFader.Instance.FadeToScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
