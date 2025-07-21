using UnityEngine;
using TMPro;

public class HarvestManager : MonoBehaviour
{
    [SerializeField] private int cabbageGoal = 20;
    [SerializeField] private int tomatoGoal = 20;
    [SerializeField] private string nextSceneName = "GameScene2"; 

    private bool levelCompleted = false;

    public static HarvestManager Instance { get; private set; }

    private int cabbageCount = 0;
    private int tomatoCount = 0;

    [SerializeField] private TextMeshProUGUI cabbageText;
    [SerializeField] private TextMeshProUGUI tomatoText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void CollectPlant(HarvestablePlant.PlantType plantType)
    {
        if (levelCompleted) return;

        switch (plantType)
        {
            case HarvestablePlant.PlantType.Cabbage:
                cabbageCount+=1;
                break;

            case HarvestablePlant.PlantType.Tomato:
                tomatoCount+=1;
                break;
        }

        UpdateUI();

        if (cabbageCount >= cabbageGoal && tomatoCount >= tomatoGoal)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        levelCompleted = true;
        Debug.Log("All crops collected! Advancing to next level...");
        SceneFader.Instance.FadeToScene(nextSceneName); 
    }


    private void UpdateUI()
    {
        cabbageText.text = "Cabbages: " + cabbageCount + "/" + cabbageGoal;
        tomatoText.text = "Tomatoes: " + tomatoCount + "/" + tomatoGoal;
    }
}
