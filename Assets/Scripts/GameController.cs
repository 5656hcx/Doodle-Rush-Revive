using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum State { PAUSED, RUNNING, ZOMBIE, STOPPED };

public class GameController: MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer deadLine;
    public ScoreBoard scoreboard;
    public EndScreen endScreen;
    public Banner banner;

    public GameObject menuButtons;
    public GameObject menuPanels;

    public State state = State.STOPPED;
    private float right_edge, left_edge;

    public State GetState()
    {
        return state;
    }

    public void SetState(State new_state)
    {
        state = new_state;
    }

    public void StartPlaying()
    {
        SetState(State.RUNNING);
        menuButtons.SetActive(false);
        banner.gameObject.SetActive(false);
        scoreboard.gameObject.SetActive(true);
        deadLine.gameObject.SetActive(true);
    }

    public void EndPlaying()
    {
        SetState(State.STOPPED);
        scoreboard.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(true);
    }

    void Start()
    {
        left_edge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        right_edge = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == State.STOPPED)
            {
                if (!menuButtons.activeSelf && !endScreen.gameObject.activeSelf)
                {
                    menuButtons.SetActive(true);
                    for (int i=0; i<menuPanels.transform.childCount; i++)
                    {
                        menuPanels.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    banner.ChangeDisplay(false);
                }
                else
                {
                    Application.Quit();
                }
            }
            else if (state == State.PAUSED)
            {
                Time.timeScale = 1;
                state = State.RUNNING;
            }
            else if (state == State.RUNNING)
            {
                Time.timeScale = 0;
                state = State.PAUSED;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && !endScreen.gameObject.activeSelf)
        {
            StartPlaying();
        }

    }

    public float BoundCheck(float position, float offset)
    {
        if (position + offset > right_edge) return right_edge - position - offset;
        if (position - offset < left_edge) return left_edge - position + offset;
        return 0;
    }

}
