using UnityEngine;
using UnityEngine.UI;

public class SeedSelectorUI : MonoBehaviour
{

    [SerializeField] private Image seedIcon;
    [SerializeField] private Sprite cabbageSprite;
    [SerializeField] private Sprite tomatoSprite;

    private void Update()
    {
        switch (PlantingManager.Instance.selectedPlant)
        {
            case HarvestablePlant.PlantType.Cabbage:
                seedIcon.sprite = cabbageSprite;
                break;

            case HarvestablePlant.PlantType.Tomato:
                seedIcon.sprite = tomatoSprite;
                break;
        }

    }
}
