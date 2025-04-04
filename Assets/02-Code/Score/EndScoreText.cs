using TMPro;
using UnityEngine;

public class EndScoreText : MonoBehaviour
{
    private int score = 0;
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    private void Update()
    {
        scoreText.text = "Score : " + score;
    }
}
