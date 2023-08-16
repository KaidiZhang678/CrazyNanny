using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class TrashesGenerator : MonoBehaviour
{
    public GameObject[] trashPrefabs;
    public float spawnInterval = 5.0f;
    public float spawnAreaSize = 5.0f;
    public int maxTrashCount = 6;
    public int scale = 2;
    public float spawnDelay = 5.0f;
    public TextMeshProUGUI countText;

    public int currentTrashCount = 0;
    public int existingTrashCount = 0;
    public int trashCleanedCount = 0;

    private void Start()
    {
        StartCoroutine(SpawnTrash());
    }

    private IEnumerator SpawnTrash()
    {
        // Start an infinite loop
        while (true)
        {
            // Only spawn trash if the current trash count is less than the maximum allowed
            if (currentTrashCount < maxTrashCount)
            {
                // Wait for the defined interval
                yield return new WaitForSeconds(spawnInterval);

                // Select a random trash prefab
                GameObject trashToSpawn = trashPrefabs[Random.Range(0, trashPrefabs.Length)];

                // Determine a random spawn position within the defined cube
                Vector3 spawnPosition = new Vector3(
                    transform.position.x + Random.Range(-spawnAreaSize / 2, spawnAreaSize / 2),
                    0.1f,
                    transform.position.z + Random.Range(-spawnAreaSize / 2, spawnAreaSize / 2)
                );

                // Spawn the trash
                GameObject trashSpawned = Instantiate(trashToSpawn, spawnPosition, Quaternion.identity);
                trashSpawned.transform.localScale = new Vector3(scale, scale, scale);
                trashSpawned.tag = "Trash";

                currentTrashCount++;
                existingTrashCount++;
            }
            // If the current trash count has reached the maximum, yield return null to wait until the next frame
            else
            {
                yield return null;
            }

            countText.text = "x " + currentTrashCount.ToString();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(spawnAreaSize, 0.3f, spawnAreaSize));
    }

}



