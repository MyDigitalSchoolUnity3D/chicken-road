using UnityEngine;

public class TriggerCarCollision : MonoBehaviour
{
    public float launchForce = 10f;
    public delegate void OnPlayerKilled();
    public static event OnPlayerKilled playerKilledEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Poulet") return;

        LaunchPouletInSpace(other);

        // Dispatch playerKilledEvent
        playerKilledEvent?.Invoke();
    }

    private void LaunchPouletInSpace(Collider poulet)
    {
        Rigidbody rb = poulet.GetComponent<Rigidbody>();
        if (rb == null)
        {
            return;
        }

        // Appliquer une force vers le haut et en arrière pour le faire voler en vrille
        Vector3 launchDirection = (Vector3.up * launchForce) + (Vector3.forward * Random.Range(-15f, 15f)) + (Vector3.right * Random.Range(-15f, 15f));
        rb.AddForce(launchDirection, ForceMode.Impulse);
        rb.AddForce(launchDirection, ForceMode.VelocityChange);
        rb.AddForce(launchDirection, ForceMode.Acceleration);
        rb.AddForce(launchDirection, ForceMode.Force);

        // Ajoute une rotation aléatoire
        rb.AddTorque(2 * launchForce * Random.insideUnitSphere, ForceMode.Impulse);
    }
}
