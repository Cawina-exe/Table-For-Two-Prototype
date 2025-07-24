using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartContdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        DucktoryGameManager.Instance.OnStateChanged += DucktoryGameManager_OnStateChanged;

        Hide();
    }

    private void DucktoryGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (DucktoryGameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        countdownText.text =Mathf.Ceil( DucktoryGameManager.Instance.GetCountdownToStartTimer()).ToString();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
