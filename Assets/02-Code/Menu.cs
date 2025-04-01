using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Start()
    {
        if (DataPersistenceManager.instance != null)
        {
            scoreText.text = "Meilleur score : " + DataPersistenceManager.instance.GetHighScore();
        }
        else
        {
            Debug.LogWarning("DataPersistenceManager instance is missing.");
            scoreText.text = "Meilleur score : 0";
        }
    }
}
