





//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RandomObjectSpawner : MonoBehaviour
//{
//    public GameObject trashPrefab;
//    public Vector3 centerPosition;
//    public float spawnRadius = 20f;
//    public float spawnInterval = 10f;
//    public int maxTrashCount = 4;

//    private int trashCount = 0;
//    private float spawnTimer = 0f;

//    private void Update()
//    {
//        spawnTimer += Time.deltaTime;

//        if (spawnTimer >= spawnInterval && trashCount < maxTrashCount)
//        {
//            SpawnTrash(centerPosition, spawnRadius);
//            spawnTimer = 0f;
//            trashCount++;
//        }
//    }

//    private void SpawnTrash(Vector3 center, float radius)
//    {
//        Vector3 randomPosition = center + Random.insideUnitSphere * radius;
//        randomPosition.y = 0.11f; // Ensure the trash is spawned on the floor (y = 0)

//        GameObject pile = Instantiate(trashPrefab, randomPosition, Quaternion.identity);
//        pile.tag = "Piles";
//    }
//}





//////////////////////////////////////////////////





//using UnityEngine;

//public class RandomObjectGenerator : MonoBehaviour
//{
//    public GameObject pilePrefab;
//    public int numPiles = 4;

//    private GameObject[] piles;

//    void Start()
//    {
//        SpawnPiles();
//    }

//    void SpawnPiles()
//    {
//        piles = new GameObject[numPiles];

//        for (int i = 0; i < numPiles; i++)
//        {
//            Vector3 spawnPosition = GetRandomSpawnPosition();
//            piles[i] = Instantiate(pilePrefab, spawnPosition, Quaternion.identity);
//            piles[i].tag = "Piles"; // Assign the "Piles" tag to the newly generated pile

//            TrashInteraction trashInteraction = piles[i].GetComponent<TrashInteraction>();

//            if (trashInteraction != null)
//            {
//                trashInteraction.enabled = true;
//            }
//            else
//            {
//                Debug.LogError("Trahs Interaction script not found on the trahs!!@!!");
//            }
//        }
//    }

//    Vector3 GetRandomSpawnPosition()
//    {
//        // Calculate random spawn position within a certain range
//        // Modify this based on your scene and desired spawn area
//        float spawnX = Random.Range(-10f, 10f);
//        float spawnZ = Random.Range(-10f, 10f);
//        Vector3 spawnPosition = new Vector3(spawnX, 0.1f, spawnZ);
//        return spawnPosition;
//    }

//}
















