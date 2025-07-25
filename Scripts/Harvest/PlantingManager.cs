using UnityEngine;

public class PlantingManager : MonoBehaviour
{
    public static PlantingManager Instance { get; private set; }

    public HarvestablePlant.PlantType selectedPlant = HarvestablePlant.PlantType.Cabbage;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedPlant = HarvestablePlant.PlantType.Cabbage;
            Debug.Log("Selected: Cabbage");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedPlant = HarvestablePlant.PlantType.Tomato;
            Debug.Log("Selected: Tomato");
        }
    }
}
