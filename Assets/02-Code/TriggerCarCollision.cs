using UnityEngine;

public class TriggerCarCollision : MonoBehaviour
{
    public float launchForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Poulet")
        {
            return;
        }

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Appliquer une force vers le haut et en arrière pour le faire voler en vrille
            Vector3 launchDirection = (Vector3.up * launchForce) + (Vector3.forward * Random.Range(-5f, 5f)) + (Vector3.right * Random.Range(-5f, 5f));

            rb.AddForce(launchDirection, ForceMode.Impulse);
            // Ajoute une rotation aléatoire
            rb.AddTorque(Random.insideUnitSphere * launchForce * 2, ForceMode.Impulse);
        }
    }
}
