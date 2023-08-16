using UnityEngine;

public class TrashGenerator : MonoBehaviour
{
    public GameObject trashPrefab;
    public float spawnRadius = 10f;
    public int trashCount = 5;

    private void Start()
    {
        GenerateTrash();
    }

    private void GenerateTrash()
    {
        for (int i = 0; i < trashCount; i++)
        {
            Vector3 randomPos = Random.insideUnitSphere * spawnRadius;
            randomPos.y = 0.13f; // Ensure the trash is on the same level as the player (assuming Y-axis is up).

            GameObject trash = Instantiate(trashPrefab, randomPos, Quaternion.identity);
            trash.tag = "Trash";
            trash.transform.SetParent(transform); // Make the trash a child of the generator for organization.
        }
    }
}
