using UnityEngine;

public class RandomCarGenerator : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public bool spawnLeft;
    public float carSpeed;

    void Start()
    {
        spawnLeft = Random.value > 0.5f;
        carSpeed = Random.Range(5f, 15f);

        InvokeRepeating(nameof(SpawnRandomCar), 0.2f, Random.Range(1f, 3f));
    }

    void SpawnRandomCar()
    {
        if (carPrefabs.Length == 0)
        {
            Debug.LogWarning("Aucun prefab de voiture assigné !");
            return;
        }

        int randomIndex = Random.Range(0, carPrefabs.Length);
        GameObject selectedCar = carPrefabs[randomIndex];

        Transform spawnPoint = spawnLeft ? leftSpawnPoint : rightSpawnPoint;
        GameObject carInstance = Instantiate(selectedCar, spawnPoint.position, spawnPoint.rotation);
        carInstance.transform.Rotate(0f, spawnLeft ? 0f : 180f, 0f);

        CarMover mover = carInstance.AddComponent<CarMover>();
        mover.speed = spawnLeft ? carSpeed : -carSpeed;
    }
}
