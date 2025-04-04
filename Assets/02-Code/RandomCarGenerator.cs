using UnityEngine;

public class RandomCarGenerator : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public bool spawnLeft;
    public float carSpeed;
    public float repeatRate;

    void Start()
    {
        spawnLeft = Random.value > 0.5f;

        ComputeCarSpeedWithPlayerScore();

        InvokeRepeating(
            methodName: nameof(SpawnRandomCar),
            time: 0.2f,
            repeatRate: Random.Range(1f, 4f)
        );
    }

    void ComputeCarSpeedWithPlayerScore()
    {
        int score = PlayerControls.score;
        float normalizedScore = Mathf.Clamp01(score / 500f);

        carSpeed = Mathf.Lerp(
            a: 8f,
            b: 18f,
            t: Mathf.Sqrt(normalizedScore)
        );
    }

    void SpawnRandomCar()
    {
        if (carPrefabs.Length == 0)
        {
            throw new System.Exception("Aucun prefab de voiture assigné !");
        }

        GameObject selectedCar = GetRandomCarPrefab();
        Transform spawnPoint = GetRandomSpawnPoint();
        GameObject carInstance = InstantiateCar(
            carPrefab: selectedCar,
            spawnPoint: spawnPoint
        );
        RotateCar(
            carInstance: carInstance,
            isLeft: spawnLeft
        );
        AddCarMoverComponent(
            carInstance: carInstance,
            isLeft: spawnLeft
        );
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
