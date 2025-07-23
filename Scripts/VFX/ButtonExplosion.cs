using UnityEngine;

public class ButtonExplosion : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffectPrefab;

    public void PlayEffect()
    {
        Vector3 spawnPos = Input.mousePosition;
        spawnPos.z = 10f; 

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(spawnPos);
        GameObject effect = Instantiate(explosionEffectPrefab, worldPos, Quaternion.identity);
        Destroy(effect, 2f);
    }
}
