using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScore;
    private TextMeshProUGUI scoreText;
    private int score;
    public int Score
    {
        get 
        {
            return this.score;
        }
        set 
        {
            this.score = value;
            UpdateScoreText();
        }
    }
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void UpdateScoreText()
    {
        string scoreStr = string.Format("{0:0000000}", score);
        scoreText.text = scoreStr;
        finalScore.text = scoreStr;
    }
}
