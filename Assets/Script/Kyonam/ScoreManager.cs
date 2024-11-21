using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField]
    private int score = 0;
    [SerializeField]
    private Text[] scoreText = null; 
    
    public void InitScore()
    {
        score = 0;
        for(int i = 0; i < scoreText.Length; i++)
            scoreText[i].text = score.ToString();
    }
    public void AddScore(int _score)
    {
        score += _score;
        for (int i = 0; i < scoreText.Length; i++)
            scoreText[i].text = score.ToString();
    }
}
