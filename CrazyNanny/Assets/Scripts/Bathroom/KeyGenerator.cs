using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject blackKeyPrefab;  // Assign your key prefab in the inspector
    public GameObject blueKeyPrefab;  // Assign your key prefab in the inspector
    public Vector3 center;        // Center of the spawning area
    public Vector3 size;          // Size of the spawning area

    void Start()
    {
        SpawnKey();
    }

    public void SpawnKey()
    {
        Vector3 blackPos;
        bool spaceOccupiedBlack;
        do {
            blackPos = center + new Vector3(Random.Range(-size.x/2, size.x/2),
                                            Random.Range(.3f, .4f),
                                            Random.Range(-size.z/2, size.z/2));
            spaceOccupiedBlack = Physics.CheckBox(blackPos, blackKeyPrefab.transform.localScale / 2);
            Debug.Log("Generated collided with a tree....");
        } while (spaceOccupiedBlack);

        Vector3 bluePos;
        bool spaceOccupiedBlue;
        do {
            bluePos = center + new Vector3(Random.Range(-size.x/2, size.x/2),
                                            Random.Range(.3f, .4f),
                                            Random.Range(-size.z/2, size.z/2));
            spaceOccupiedBlue = Physics.CheckBox(bluePos, blueKeyPrefab.transform.localScale / 2);
        } while (spaceOccupiedBlue);

        // Vector3 blackPos = center + new Vector3(Random.Range(-size.x/2, size.x/2),
        //                                     Random.Range(.3f, .4f),
        //                                     Random.Range(-size.z/2, size.z/2));
        Quaternion blackRot = Quaternion.Euler(90, 0, 90);
        Instantiate(blackKeyPrefab, blackPos, blackRot);

        // Vector3 bluePos = center + new Vector3(Random.Range(-size.x/2, size.x/2),
        //                                     Random.Range(.3f, .4f),
        //                                     Random.Range(-size.z/2, size.z/2));
        Quaternion blueRot = Quaternion.Euler(90, 0, 90);
        Instantiate(blueKeyPrefab, bluePos, blueRot);

    }

    // Show the spawn area in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
