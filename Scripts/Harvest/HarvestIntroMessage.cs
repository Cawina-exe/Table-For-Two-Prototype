using System.Collections;
using UnityEngine;
using TMPro;

public class HarvestIntroMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private float scaleDuration = 0.6f;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float displayTime = 1f; 

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        
        canvasGroup = messageText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = messageText.gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
        messageText.transform.localScale = Vector3.zero;
    }

    private void Start()
    {
        StartCoroutine(PlayIntroAnimation());
    }

    private IEnumerator PlayIntroAnimation()
    {
       
        float t = 0f;
        while (t < scaleDuration)
        {
            t += Time.deltaTime;
            float progress = t / scaleDuration;
            messageText.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, progress);
            canvasGroup.alpha = progress;
            yield return null;
        }

        messageText.transform.localScale = Vector3.one;
        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(displayTime);

       
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float progress = t / fadeDuration;
            canvasGroup.alpha = 1f - progress;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        messageText.gameObject.SetActive(false);
    }
}
