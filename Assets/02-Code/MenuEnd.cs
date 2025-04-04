using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(
            sceneName: SceneManager.GetActiveScene().name
        );
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
