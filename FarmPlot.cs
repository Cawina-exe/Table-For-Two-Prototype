using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmPlot : MonoBehaviour
{
    [SerializeField] private GameObject cabbagePrefab;
    [SerializeField] private Transform spawnPoint;
    private bool isPlayerInRange = false;
    private GameObject currentCabbage;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TrySpawnCabbage();
        }
    }

    private void TrySpawnCabbage()
    {
        if (currentCabbage == null)
        {
            currentCabbage = Instantiate(cabbagePrefab, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Cabbage already planted here!");
        }
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
