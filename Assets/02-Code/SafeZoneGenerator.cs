using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SafeZoneGenerator : MonoBehaviour
{
    public GameObject[] treePrefabs;
    public Transform safeZone;
    public List<Vector3> positions;
    public List<GameObject> instances;

    void Start()
    {
        int treesToGenerate = Random.Range(5, 15);

        for (int i = 0; i < treesToGenerate; i++)
        {
            GenerateTree();
        }
    }

    void OnDestroy()
    {
        DestroyAllTrees();
    }

    void DestroyAllTrees()
    {
        foreach (GameObject instance in instances)
        {
            Destroy(instance);
        }

        instances.Clear();
        positions.Clear();
    }

    void GenerateTree()
    {
        GameObject selectedTree = GetRandomTreePrefab();
        Vector3 randomPosition = GetRandomPosition();

        GameObject instance = Instantiate(
            original: selectedTree,
            position: randomPosition,
            rotation: Quaternion.identity
        );

        instances.Add(instance);
    }

    private GameObject GetRandomTreePrefab()
    {
        int randomIndex = Random.Range(0, treePrefabs.Length);
        return treePrefabs[randomIndex];
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition;

        do
        {
            randomPosition = new Vector3(
                Random.Range(-15f, 15f),
                -2f,
                safeZone.position.z + 0.5f
            );
        }
        while(positions.Contains(randomPosition));

        positions.Add(randomPosition);

        return randomPosition;
    }
}
