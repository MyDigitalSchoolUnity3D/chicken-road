using TMPro;
using UnityEngine;

public class BestScoreText : MonoBehaviour
{
    private int bestScore = 0;
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void SetScore()
    {
        bestScore = DataPersistenceManager.instance.GetHighScore();
    }

    private void Update()
    {
        scoreText.text = "Meilleur Score : " + bestScore;
    }
}
