using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public GameController gameController;
    public TextMeshProUGUI leaderboard;

    public uint scoreCount;
    public int scoreRate;
    public int maxNameLength;
    public string defaultText;
    
    private float score;
    private Record[] records;

    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = defaultText + score;
    }

    void Update()
    {
        if (gameController.GetState() == State.RUNNING)
        {
            int prev = (int)score;
            score += scoreRate * Time.deltaTime;
            if (prev < (int)score)
            {
                GetComponent<TextMeshProUGUI>().text = defaultText + ((int)score);
            }
        }
    }
    
    public void UpdateLeaderboard()
    {
        int i = 1;
        leaderboard.text = "";
        records = XMLHelper.Read(scoreCount);
        foreach (Record record in records)
        {
            leaderboard.text += i++ + ". " + record;
        }
    }

    public void SaveScore(string name)
    {
        if (records == null) records = XMLHelper.Read(scoreCount);
        for (int i=0; i<records.Length; i++)
        {
            if ((int)score > records[i].score)
            {
                for (int j=records.Length-1; j>i; j--)
                {
                    records[j].name = records[j-1].name;
                    records[j].score = records[j-1].score;
                }
                records[i].name = name;
                records[i].score = (int)score;
                break;
            }
        }
        XMLHelper.Write(records);
    }

    public int GetScore()
    {
        return (int)score;
    }
}
