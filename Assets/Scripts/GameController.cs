﻿using System.Collections;
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
	}

	public void EndPlaying()
	{
		SetState(State.STOPPED);
		SceneManager.LoadScene(0);
	}

	void Start()
	{
		left_edge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
		right_edge = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
	}

	public float BoundCheck(float position, float size)
	{
		float left = position - size / 2;
		float right = position + size / 2;
		
		if (right > right_edge) return right_edge - right;
		else if (left < left_edge) return left_edge - left;
		else return 0;
	}
	
}
