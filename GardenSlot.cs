using UnityEngine;

public class GardenSlot : MonoBehaviour
{
    [SerializeField] private GameObject cabbagePrefab;
    [SerializeField] private GameObject tomatoPrefab;
    [SerializeField] private Transform spawnPoint;

    private GameObject currentPlant;
    private bool isPlayerInRange = false;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TryPlant();
        }
    }

    public void ClearSlot()
    {
        currentPlant = null;
    }

    public void ResetSlot()
    {
        currentPlant = null;
        
    }


    private void TryPlant()
    {
        if (currentPlant != null) return;

        HarvestablePlant.PlantType selected = PlantingManager.Instance.selectedPlant;

        switch (selected)
        {
            case HarvestablePlant.PlantType.Cabbage:
                currentPlant = Instantiate(cabbagePrefab, spawnPoint.position, Quaternion.identity);
                currentPlant.GetComponent<HarvestablePlant>().plantedInSlot = this; 
                break;

            case HarvestablePlant.PlantType.Tomato:
                currentPlant = Instantiate(tomatoPrefab, spawnPoint.position, Quaternion.identity);
                currentPlant.GetComponent<HarvestablePlant>().plantedInSlot = this;
                break;

     

        }



       
        isPlayerInRange = false;
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

}

