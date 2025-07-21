using UnityEngine;
using UnityEngine.UI;

public class UIButtonAttract : MonoBehaviour
{
    [Header("Pulse (Scale) Settings")]

    [SerializeField] private float pulseSpeed = 1.5f;
    [SerializeField] private float pulseAmount = 0.05f;

    [Header("Color Glow Settings")]
    [SerializeField] private Color startColor = Color.white;
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private float colorLerpSpeed = 2f;

    private Vector3 originalScale;
    private Image buttonImage;

    private void Awake()
    {
        originalScale = transform.localScale;
        buttonImage = GetComponent<Image>();

        if (buttonImage == null)
        {
            Debug.LogWarning("UIButtonAttract requires an Image component on the same GameObject.");
        }
    }

    private void Update()
    {
        AnimatePulse();
        AnimateColor();
    }

    private void AnimatePulse()
    {
        float pulse = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = originalScale + new Vector3(pulse, pulse, 0);
    }

    private void AnimateColor()
    {
        if (buttonImage == null) return;

        float t = (Mathf.Sin(Time.time * colorLerpSpeed) + 1f) / 2f;
        buttonImage.color = Color.Lerp(startColor, highlightColor, t);
    }
}
