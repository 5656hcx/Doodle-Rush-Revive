using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float curr_speed_x;
    private float curr_speed_y;
    private Vector3 new_pos;

    public GameController gameController;
    public SpriteRenderer spriteRenderer;
    public BrickManager brickManager;
    public DeadLine deadLine;

    public float speedX, speedY;
    public float force, gravity;
    public float flipOffset;

    public float dyingSpeedY;
    public float dyingGravityScale;

    void Start()
    {
        curr_speed_x = 0;
        curr_speed_y = force * speedY;
    }

    void Update()
    {   
        CheckInput();
        
        // Calculate next frame 
        // 
        new_pos = transform.position + new Vector3(curr_speed_x, curr_speed_y, 0) * Time.deltaTime;
        curr_speed_y -= gravity * Time.deltaTime;

        // Validate and revise next frame
        // 
        switch (gameController.GetState())
        {
            case State.RUNNING:
                CheckSceneBound();
                CheckDeadLine();
                CheckBricks();
                break;
            case State.STOPPED:
                CheckBricks();
                break;
            case State.ZOMBIE:
                DyingAnimation();
                break;
        }

        transform.position = new_pos;
    }

    private Vector3 GetBottomLine(Vector3 pos)
    {
        float offset = spriteRenderer.flipX ? flipOffset : 0;
        return new Vector3 (pos.x + offset - 0.415f, pos.y - 0.56f, pos.x + offset + 0.255f);
    }

    /* Active after player died */
    private void DyingAnimation()
    {
        if (curr_speed_y < 0)
        {
            spriteRenderer.flipY = true;
            float offset = deadLine.BoundCheck(GetBottomLine(new_pos).y);
            if (offset != 0)
            {
                gravity = 0;
                curr_speed_x = 0;
                curr_speed_y = 0;
                new_pos.y += offset;
                gameController.EndPlaying();
            }
        }
    }

    /* Active when player hits bound of scene */
    private void CheckSceneBound()
    {
        float offset = gameController.BoundCheck(new_pos.x, spriteRenderer.size.x);
        if (offset != 0)
        {
            curr_speed_x = -curr_speed_x;
        }
        new_pos.x += offset;
    }

    /* Active when player hits dead line */
    private void CheckDeadLine()
    {
        if (curr_speed_y < 0)
        {
            float offset = deadLine.BoundCheck(GetBottomLine(new_pos).y);
            if (offset != 0)
            {
                gravity *= dyingGravityScale;
                curr_speed_x = 0;
                curr_speed_y = dyingSpeedY;
                spriteRenderer.color = Color.grey;
                gameController.SetState(State.ZOMBIE);
            }
            new_pos.y += offset;
        }
    }

    /* Active when player hits any brick */
    private void CheckBricks()
    {
        if (curr_speed_y < 0)
        {
            float offset = brickManager.BoundCheck(GetBottomLine(transform.position), GetBottomLine(new_pos));
            if (offset != 0)
            {
                curr_speed_x = 0;
                curr_speed_y = speedY;
            }
            new_pos.y += offset;
        }
    }

    /* Active when receive player input */
    private void CheckInput()
    {
        if (gameController.GetState() == State.RUNNING)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                curr_speed_x = speedX;
                spriteRenderer.flipX = false;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                curr_speed_x = -speedX;
                spriteRenderer.flipX = true;
            }
        }
    }
}
