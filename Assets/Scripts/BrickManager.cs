using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public GameController gameController;
    public Brick[] bricks;

    public float speedX, speedY;
    public float acceleration;

    public float brickUpperBound;
    public float brickLowerBound;
    public float brickGap;
    
    void Start()
    {        
        prev_brick = bricks[bricks.Length - 1];
        spawn_axis_x = prev_brick.transform.position.x;
        spawn_axis_y = prev_brick.transform.position.y;

        foreach (Brick brick in bricks)
        {
            brick.SetSpeedX(speedX);
            brick.SetSpeedY(speedY);
        }
    }

    void Update()
    {
        if (gameController.GetState() == State.RUNNING)
        {
            foreach (Brick brick in bricks)
            {
                float offset_x = brick.GetSpeedX() * Time.deltaTime;
                float offset_y = brick.GetSpeedY() * Time.deltaTime;
                brick.transform.position += new Vector3(offset_x, offset_y, 0);
                if (brick.Reusable())
                {
                    UpdateSpawnAxis();
                    brick.transform.position = new Vector3(spawn_axis_x, spawn_axis_y, 0);
                    brick.SetSpeedX(brick.GetSpeedX() - acceleration);
                    brick.BeingUsed();
                    prev_brick = brick;
                }
            }
        }
    }

    /* Calculate Safe Area for Bricks */
    private Brick prev_brick;
    private float spawn_axis_x;
    private float spawn_axis_y;

    private void UpdateSpawnAxis()
    {   
        float multiplier, candidate;
        do {
            while ((multiplier = Random.Range(-5, 5)) == 0);
            if (multiplier > 0) multiplier = 1;
            candidate = spawn_axis_y + multiplier * brickGap;
        } while (candidate < brickLowerBound || candidate > brickUpperBound);
        spawn_axis_y = candidate;
    }

    public void ActiveAll()
    {
        foreach (Brick brick in bricks)
        {
            brick.gameObject.SetActive(true);
        }
    }

    /* Collision Check Utilities */
    public float BoundCheck(Vector3 old_vec, Vector3 new_vec)
    {
        foreach (Brick brick in bricks)
        {
            float x = brick.transform.position.x - 0.57f;
            float y = brick.transform.position.y + 0.19f;
            float z = brick.transform.position.x + 0.62f;
            if (old_vec.y >= y && new_vec.y < y)
            {
                if ((new_vec.x >= x && new_vec.x <= z) || (new_vec.z >= x && new_vec.z <= z))
                {
                    brick.OnHitAction();
                    return y - new_vec.y;
                }
            }
        }
        return 0;
    }
}
