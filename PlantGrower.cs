using UnityEngine;

public class PlantGrower : MonoBehaviour
{
    [SerializeField] private float growDuration = 2f;
    [SerializeField] private AudioClip plantPopSound;
    [SerializeField] private GameObject popParticlesPrefab;

    private Vector3 targetScale;
    private float growTimer = 0f;

    private void Start()
    {
        targetScale = transform.localScale;
        transform.localScale = Vector3.zero;

        PlayPopFeedback();
    }

    private void Update()
    {
        if (growTimer < growDuration)
        {
            growTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(growTimer / growDuration);
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, progress);
        }
    }

    private void PlayPopFeedback()
    {
        if (plantPopSound != null)
        {
            AudioSource.PlayClipAtPoint(plantPopSound, transform.position);
        }

        if (popParticlesPrefab != null)
        {
            Vector3 spawnOffset = new Vector3(0f, 1, 0f);

            Instantiate(
                popParticlesPrefab,
                transform.position + spawnOffset,
                popParticlesPrefab.transform.rotation 
            );
        }
    }


}
