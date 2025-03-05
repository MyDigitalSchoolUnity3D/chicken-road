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
        transform.position += speed * Time.deltaTime * Vector3.right;
    }
}
