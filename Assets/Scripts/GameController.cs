using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum State { PAUSED, RUNNING, ZOMBIE, STOPPED };

public class GameController: MonoBehaviour
{
	public Camera mainCamera;
	public LineRenderer deadLine;

	private State state = State.STOPPED;
	private float right_edge, left_edge;
	private float bottom_edge;

	public State GetState()
	{
		return state;
	}

	public void SetState(State new_state)
	{
		state = new_state;
	}

	public void ChangeState()
	{
		state = State.RUNNING;
	}

	void Start()
	{
		left_edge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
		right_edge = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
		bottom_edge = deadLine.GetPosition(0).y + deadLine.widthMultiplier/2;
	}

	public float OutOfBound(float left, float right)
	{
		float offset = 0;
		if (right > right_edge) offset = right_edge - right;
		else if (left < left_edge) offset = left_edge - left;
		return offset;
	}
	
	public float GameOver(float altitude)
	{
		if (altitude < bottom_edge)
		{
			state = State.ZOMBIE;
			deadLine.startColor = Color.red;
			deadLine.endColor = Color.red;
			return bottom_edge - altitude;
		}
		else return 0;
	}
}
