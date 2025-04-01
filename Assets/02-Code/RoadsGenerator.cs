using UnityEngine;
using System.Collections.Generic;

using static PlayerControls;

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

    void Awake()
    {
        PlayerControls.playerMovedForwardEvent += OnPlayerMovedForward;
    }

    void OnDestroy()
    {
        PlayerControls.playerMovedForwardEvent -= OnPlayerMovedForward;
    }

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

    void GenerateRoad(Transform playerTransform = null)
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

        lastRoadLength = GetZLength(instance);

        // Si le joueur est suffisamment loin devant la route (on laisse un petit buffer) + qu'il y a + de 30 routes
        // On détruit la route la plus ancienne
        GameObject oldestRoad = roadQueue.Peek();
        float roadZ = oldestRoad.transform.position.z;
        float roadLength = GetZLength(oldestRoad);
        if (playerTransform && playerTransform.position.z > roadZ + roadLength + 0.5f && roadQueue.Count > 30)
        {
            Destroy(roadQueue.Dequeue());
        }
    }

    float GetZLength(GameObject gameObject)
    {
        Renderer renderer = gameObject.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds.size.z;
        }

        return 1f;
    }

    void OnPlayerMovedForward(Transform playerTransform)
    {
        GenerateRoad(playerTransform);
    }
}
