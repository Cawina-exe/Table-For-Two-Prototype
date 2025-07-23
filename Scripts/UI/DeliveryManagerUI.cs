using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    private List<DeliveryManagerSingleUI> activeRecipeUIs = new List<DeliveryManagerSingleUI>();


    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;

        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        RecipeSO deliveredRecipe = DeliveryManager.Instance.LastDeliveredRecipe;

        for (int i = 0; i < activeRecipeUIs.Count; i++)
        {
            DeliveryManagerSingleUI ui = activeRecipeUIs[i];

            if (ui.GetRecipeSO() == deliveredRecipe)
            {
                ui.PlayFadeOutAnimation(() => {
                    Destroy(ui.gameObject);
                });
                activeRecipeUIs.RemoveAt(i);
                break;
            }
        }
    }





    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        RecipeSO latestRecipe = DeliveryManager.Instance.GetLastSpawnedRecipe();

        Transform recipeTransform = Instantiate(recipeTemplate, container);
        recipeTransform.gameObject.SetActive(true);

        DeliveryManagerSingleUI recipeUI = recipeTransform.GetComponent<DeliveryManagerSingleUI>();
        recipeUI.SetRecipeSO(latestRecipe);

        activeRecipeUIs.Add(recipeUI);
    }


    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);   
        }
    }
}
