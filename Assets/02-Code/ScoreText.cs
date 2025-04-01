using System;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour, IDataPersistence
{
    private int score = 0;
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void LoadData(GameData data)
    {
        score = data.score;
    }

    public void SaveData(ref GameData data)
    {
        data.score = score;
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
