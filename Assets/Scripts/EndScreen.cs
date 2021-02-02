using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;
    public ScoreBoard scoreboard;
    
    private static int minNameLength;
    private static string nameString;

    void Awake()
    {
        minNameLength = nameText.text.Length;
        scoreboard.maxNameLength += minNameLength;
        nameText.text += nameString;
        scoreText.text += scoreboard.GetScore().ToString();
    }

    void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b')
            {
                if (nameText.text.Length > minNameLength)
                {
                    nameText.text = nameText.text.Substring(0, nameText.text.Length - 1);
                }
            }
            else if ((c == '\n') || (c == '\r'))
            {   
                nameString = nameText.text.Substring(minNameLength, nameText.text.Length - minNameLength);
                if (nameString.Length > 0)
                {
                    scoreboard.SaveScore(nameString);
                }
                SceneManager.LoadScene(0);
            }
            else if (nameText.text.Length < scoreboard.maxNameLength)
            {
                if ( ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z') || ('0' <= c && c <= '9'))
                {
                    nameText.text += c;
                }
            }
        }
    }

}
