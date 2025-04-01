using UnityEngine;

public class MenuEnd : MonoBehaviour
{
    void Awake()
    {
        TriggerCarCollision.playerKilledEvent += OnPlayerKilled;
    }

    void OnDestroy()
    {
        TriggerCarCollision.playerKilledEvent -= OnPlayerKilled;
    }

    void OnPlayerKilled()
    {
        Debug.Log("Game Over !");
    }
}
