using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneEndHandler : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string nextSceneName = "GameScene";

    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
        
    }
}
