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
            throw new System.Exception("Aucun prefab de voiture assigné !");
        }

        GameObject selectedCar = GetRandomCarPrefab();
        Transform spawnPoint = GetRandomSpawnPoint();
        GameObject carInstance = InstantiateCar(selectedCar, spawnPoint);
        RotateCar(carInstance, spawnLeft);
        AddCarMoverComponent(carInstance, spawnLeft);
    }

    GameObject GetRandomCarPrefab()
    {
        int randomIndex = Random.Range(
            minInclusive: 0,
            maxExclusive: carPrefabs.Length
        );

        return carPrefabs[randomIndex];
    }
    
    Transform GetRandomSpawnPoint()
    {
        return spawnLeft ? leftSpawnPoint : rightSpawnPoint;
    }

    GameObject InstantiateCar(GameObject carPrefab, Transform spawnPoint)
    {
        return Instantiate(
            original: carPrefab,
            position: spawnPoint.position,
            rotation: spawnPoint.rotation
        );
    }

    void RotateCar(GameObject carInstance, bool isLeft)
    {
        carInstance.transform.Rotate(
            xAngle: 0f,
            yAngle: isLeft ? 0f : 180f,
            zAngle: 0f
        );
    }

    void AddCarMoverComponent(GameObject carInstance, bool isLeft)
    {
        CarMover mover = carInstance.AddComponent<CarMover>();
        mover.speed = isLeft ? carSpeed : -carSpeed;
    }
}
