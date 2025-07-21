using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class CreditsMusicFader : MonoBehaviour
{
    [SerializeField] private float fadeInDuration = 3f;
    [SerializeField] private float fadeOutDuration = 2f;
    [SerializeField] private float targetVolume = 0.5f;

    private AudioSource audioSource;
    private float fadeTimer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f; 
    }

    private void Start()
    {
        audioSource.Play();
        StartCoroutine(FadeIn());
        SceneManager.sceneUnloaded += OnSceneUnloaded; 
    }

    private System.Collections.IEnumerator FadeIn()
    {
        fadeTimer = 0f;
        while (audioSource.volume < targetVolume)
        {
            fadeTimer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, targetVolume, fadeTimer / fadeInDuration);
            yield return null;
        }
        audioSource.volume = targetVolume;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        StartCoroutine(FadeOutAndStop());
    }

    private System.Collections.IEnumerator FadeOutAndStop()
    {
        float startVolume = audioSource.volume;
        float t = 0f;

        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeOutDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = 0f;
    }

    private void OnDestroy()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded; 
    }
}
