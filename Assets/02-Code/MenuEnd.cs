using UnityEngine;

public class MenuEnd : MonoBehaviour
{
    public GameObject menu; // Assign in inspector
    public PlayerControls player;

    private void Start()
    {
        menu.SetActive(false);
    }

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
        menu.SetActive(true);
    }

    public void RestartGame()
    {
        menu.SetActive(false);
        player.DefaultPosition();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
