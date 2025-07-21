using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSucess;
    public event EventHandler OnRecipeFailed;


    public static DeliveryManager Instance { get; private set; }
    public RecipeSO LastDeliveredRecipe { get; private set; }



    [SerializeField] private RecipeListSO recipeListSO;


    private List <RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 15f;
    private int waitingRecipeMax = 3;
    private int sucessfulRecipesAmount = 0;
    private int minimumRecipesPassLevel = 4;

    private void Awake()
    {
        Instance = this;


        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        if (!DucktoryGameManager.Instance.IsGamePlaying()) return;

        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO newRecipe = recipeListSO.recipeSOList[
                    UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipeSOList.Add(newRecipe);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }


    public RecipeSO GetLastSpawnedRecipe()
    {
        if (waitingRecipeSOList.Count > 0)
        {
            return waitingRecipeSOList[waitingRecipeSOList.Count - 1];
        }
        return null;
    }


    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjectSO> plateIngredients = new List<KitchenObjectSO>(plateKitchenObject.GetKitchenObjectSOList());

        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO recipe = waitingRecipeSOList[i];

            if (recipe.kitchenObjectSOList.Count == plateIngredients.Count)
            {
                List<KitchenObjectSO> unmatched = new List<KitchenObjectSO>(plateIngredients);
                bool allMatch = true;

                foreach (KitchenObjectSO required in recipe.kitchenObjectSOList)
                {
                    if (unmatched.Contains(required))
                    {
                        unmatched.Remove(required);
                    }
                    else
                    {
                        allMatch = false;
                        break;
                    }
                }

                if (allMatch)
                {
                    LastDeliveredRecipe = recipe; 
                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSucess?.Invoke(this, EventArgs.Empty);
                    sucessfulRecipesAmount++;
                    return;
                }

            }
        }

       
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        CameraShake.Instance.Shake();

    }

    public int GetminimumRecipesPassLevel()
    {
        return minimumRecipesPassLevel;
    }



    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSucessfulRecipesAmount()
    {
        return sucessfulRecipesAmount;
    }

}
