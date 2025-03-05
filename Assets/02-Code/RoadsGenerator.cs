using UnityEngine;
using System.Collections.Generic;

public class RoadsGenerator : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public GameObject roadPrefab;
    public GameObject safeZonePrefab;
    public Transform safeZone;
    public int roadsToGenerateOnStart = 10;

    private Queue<GameObject> roadQueue = new Queue<GameObject>(); // Stocke les routes visibles

    private Vector3 lastRoadPosition;
    public float lastRoadLength;

    void Start()
    {
        GenerateStartRoads();
    }

    void GenerateStartRoads()
    {
        lastRoadPosition = safeZone.position;
        lastRoadLength = 1f;

        for (int i = 0; i < roadsToGenerateOnStart; i++)
        {
            GenerateRoad();
        }
    }

    void GenerateRoad()
    {
        // Calculer la nouvelle position (collée à la dernière route)
        lastRoadPosition += Vector3.forward * lastRoadLength;

        // 1 chance sur 6 de générer une safe zone
        GameObject prefab = null;

        if (Random.value < 1f / 6f)
        {
            prefab = safeZonePrefab;
        }
        else
        {
            prefab = roadPrefab;
        }

        GameObject instance = Instantiate(prefab, lastRoadPosition, safeZone.rotation);
        roadQueue.Enqueue(instance);

        lastRoadLength = getZLength(instance);
    }

    float getZLength(GameObject gameObject)
    {
        Renderer renderer = gameObject.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            Debug.Log(" BLOUUUUUH" + renderer.bounds.size.z);
            return renderer.bounds.size.z;
        }

        return 1f;
    }
}
