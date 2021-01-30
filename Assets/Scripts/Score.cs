using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GetState() == State.RUNNING)
        {
            int prev = (int) scoreVal;
            scoreVal += scoreSpeed * Time.deltaTime;
            if (prev != (int)scoreVal)
            {
            	GetComponent<TextMeshProUGUI>().text = ((int)scoreVal).ToString();
            }
        }
    }

    public GameController gameController;
    public int scoreSpeed;
    private float scoreVal;
}
