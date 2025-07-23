using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainMenuMusicFader : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private float targetVolume = 0.5f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
    }

    private void Start()
    {
        audioSource.Play();
        StartCoroutine(FadeIn());
    }

    private System.Collections.IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, targetVolume, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = targetVolume;
    }
}
