using UnityEngine;
using System.Collections;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private int maxGemsInArena = 5;
    [SerializeField] private float spawnInterval = 3f;

    [SerializeField] private float spawnAreaX = 65f;
    [SerializeField] private float spawnAreaZ = 55f;
    [SerializeField] private float spawnHeightY = 0.5f;

    private int currentGemsInScene = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnGemsRoutine());
    }

    private IEnumerator SpawnGemsRoutine()
    {
        while (true)
        {

            currentGemsInScene = GameObject.FindGameObjectsWithTag("Gem").Length;

            if (currentGemsInScene < maxGemsInArena)
            {
                SpawnGem();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnGem()
    {

        float randomX = Random.Range(-spawnAreaX / 2f, spawnAreaX / 2f);
        float randomZ = Random.Range(-spawnAreaZ / 2f, spawnAreaZ / 2f);

        Vector3 spawnPosition = transform.position + new Vector3(randomX, spawnHeightY, randomZ);

        Instantiate(gemPrefab, spawnPosition, Quaternion.identity);
    }
}
