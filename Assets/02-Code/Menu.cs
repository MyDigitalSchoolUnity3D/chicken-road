using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public int score = 100;
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
        scoreText.text = "Meilleur score : " + score;
    }
}
