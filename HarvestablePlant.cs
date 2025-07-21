using UnityEngine;

public class HarvestablePlant : MonoBehaviour
{

    public enum PlantType { Cabbage, Tomato }
    public PlantType plantType;
    public GardenSlot plantedInSlot;


    private bool isPlayerNear = false;
    private bool hasBeenCollected = false;


    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Collect();
        }
    }

    private void Collect()
    {
        if (hasBeenCollected) return; 
        hasBeenCollected = true;

        if (plantedInSlot != null)
        {
            plantedInSlot.ResetSlot(); 
        }



        Debug.Log("Collect() called for: " + plantType);
        HarvestManager.Instance.CollectPlant(plantType);
        Destroy(gameObject);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
