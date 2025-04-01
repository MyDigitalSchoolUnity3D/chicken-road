using UnityEngine;

[System.Serializable]
public class GameData
{
    public int score;
    public int bestScore;

    public GameData()
    {
        score = 0;
        bestScore = 0;
    }
}
