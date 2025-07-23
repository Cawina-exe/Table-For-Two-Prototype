using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private AudioClip failSound;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private Image timerBar;
    [SerializeField] private float recipeTimeLimit = 40f;
    [SerializeField] private Color normalColor = Color.green;
    [SerializeField] private Color warningColor = Color.red;
    [SerializeField] private float warningThreshold = 5f;
    [SerializeField] private float flashSpeed = 4f;

    private bool isFlashing = false;
    private Animator animator;


    private float timer;
    private bool isActive = false;
    private bool hasBeenInitialized = false;

    private RecipeSO recipeSO;

   

    public RecipeSO GetRecipeSO()
    {
        return recipeSO;
    }


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (!isActive) return;

        timer -= Time.deltaTime;
        float progress = Mathf.Clamp01(timer / recipeTimeLimit);
        timerBar.fillAmount = progress;

      
        if (timer <= warningThreshold)
        {
            isFlashing = true;
            float t = Mathf.PingPong(Time.time * flashSpeed, 1f);
            timerBar.color = Color.Lerp(normalColor, warningColor, t);
        }
        else
        {
            isFlashing = false;
            timerBar.color = normalColor;
        }


        if (timer <= 0f)
        {
            isActive = false;
            HandleRecipeTimeout();
        }
    }

    private void HandleRecipeTimeout()
    {
        Debug.Log("Recipe expired! " + gameObject.name);
        if (failSound != null)
        {
            AudioSource.PlayClipAtPoint(failSound, Camera.main.transform.position);
        }

        StartCoroutine(PlayFailAnimationThenDestroy());
    }

    private IEnumerator PlayFailAnimationThenDestroy()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 punchScale = originalScale * 1.2f;

        float t = 0f;
        float duration = 0.3f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float scale = Mathf.Lerp(1f, 1.2f, Mathf.PingPong(t * 10f, 1f));
            transform.localScale = originalScale * scale;
            yield return null;
        }

        Destroy(gameObject);
    }


    public void SetRecipeSO(RecipeSO recipeSO)
    {
        this.recipeSO = recipeSO;

        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }

        timer = recipeTimeLimit;
        isActive = true;
        timerBar.fillAmount = 1f;
    }

    public void PlayFadeOutAnimation(System.Action onComplete)
    {
        if (animator != null)
        {
            animator.SetTrigger("FadeOut");
            StartCoroutine(WaitToDestroy(onComplete, 0.5f)); 
        }
        else
        {
            
            onComplete?.Invoke();
        }
    }

    private IEnumerator WaitToDestroy(System.Action callback, float delay)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }



}

