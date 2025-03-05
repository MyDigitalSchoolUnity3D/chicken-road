using UnityEngine;

public class CarMover : MonoBehaviour
{
    public float speed;
    public float lifetime = 10f; // Temps avant destruction

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
