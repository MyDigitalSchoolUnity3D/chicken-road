using UnityEngine;

public class TriggerCarCollision : MonoBehaviour
{
    public float launchForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Poulet") return;
        Debug.Log("Collision avec Poulet");

        LaunchPouletInSpace(other);
        // Event mort du poulet
    }

    private void LaunchPouletInSpace(Collider poulet)
    {
        Rigidbody rb = poulet.GetComponent<Rigidbody>();
        if (rb == null) {
            return;
        }

        // Appliquer une force vers le haut et en arrière pour le faire voler en vrille
        Vector3 launchDirection = (Vector3.up * launchForce) + (Vector3.forward * UnityEngine.Random.Range(-15f, 15f)) + (Vector3.right * UnityEngine.Random.Range(-15f, 15f));
        rb.AddForce(launchDirection, ForceMode.Impulse);
        rb.AddForce(launchDirection, ForceMode.VelocityChange);
        rb.AddForce(launchDirection, ForceMode.Acceleration);
        rb.AddForce(launchDirection, ForceMode.Force);

        // Ajoute une rotation aléatoire
        rb.AddTorque(UnityEngine.Random.insideUnitSphere * launchForce * 2, ForceMode.Impulse);
    }
}
