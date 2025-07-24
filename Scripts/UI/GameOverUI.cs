using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deliveredText;

    private void Start()
    {
        gameObject.SetActive(false);
        DucktoryGameManager.Instance.OnStateChanged += DucktoryGameManager_OnStateChanged;
    }

    private void DucktoryGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (DucktoryGameManager.Instance.IsGameOver())
        {
            int delivered = DeliveryManager.Instance.GetSucessfulRecipesAmount();
            int minimumRecipesPassLevel = DeliveryManager.Instance.GetminimumRecipesPassLevel();
            deliveredText.text = "You delivered " + delivered + " recipes out of " + minimumRecipesPassLevel;
            gameObject.SetActive(true);
        }
    }
}
