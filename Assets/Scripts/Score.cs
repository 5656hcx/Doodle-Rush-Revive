using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public GameController gameController;
    public TextMeshProUGUI scoreText;
    public int scoreRate;
    private float score;

    void Update()
    {
        if (gameController.GetState() == State.RUNNING)
        {
            int prev = (int)score;
            score += scoreRate * Time.deltaTime;
            if (prev < (int)score)
            {
                scoreText.text = ((int)score).ToString();
            }
        }
    }

    public int GetScore()
    {
        return (int)score;
    }
    
}
