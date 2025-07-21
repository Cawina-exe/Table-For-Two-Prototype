using TMPro;
using UnityEngine;

public class RecipesLeftUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesLeftText;
    private int totalRecipes = 4;

    private void Start()
    {
        UpdateText(totalRecipes);

        
        DeliveryManager.Instance.OnRecipeSucess += OnRecipeDelivered;
    }

    private void OnDestroy()
    {
        if (DeliveryManager.Instance != null)
        {
            DeliveryManager.Instance.OnRecipeSucess -= OnRecipeDelivered;
        }
    }

    private void OnRecipeDelivered(object sender, System.EventArgs e)
    {
        totalRecipes = Mathf.Max(0, totalRecipes - 1);
        UpdateText(totalRecipes);
    }

    private void UpdateText(int remaining)
    {
        recipesLeftText.text = "Recipes Left: " + remaining;
    }
}
