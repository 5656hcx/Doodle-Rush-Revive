using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float curr_speed_x;
    private float curr_speed_y;

    public GameController gameController;
    public SpriteRenderer spriteRenderer;
    public BrickManager brickManager;
    public LineRenderer lineRenderer;
    public float speedX, speedY;
    public float force, gravity;
    public float flipOffset;

    private float zombie_speed;
    private float zombie_gravity;

    void Start()
    {
        curr_speed_x = 0;
        curr_speed_y = force * speedY;

        zombie_speed = 0.2f;
        zombie_gravity = 0.02f;
    }

    void FixedUpdate()
    {
        if (gameController.GetState() == State.ZOMBIE)
        {

            transform.position += new Vector3(0, zombie_speed, 0);
            zombie_speed -= zombie_gravity;
            if (zombie_speed < 0)
            {
                spriteRenderer.flipY = true;
                float tmp = gameController.GameOver(GetBottomLine().y);
                if (tmp > 0)
                {
                    zombie_speed = 0;
                    zombie_gravity = 0;
                    transform.position += new Vector3(0, tmp, 0);
                    SceneManager.LoadScene(0);
                }
            }

            return;
        }


        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
                UpdateLine(-flipOffset);
            }
            curr_speed_x = speedX;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
                UpdateLine(flipOffset);
            }
            curr_speed_x = -speedX;
        }

        Vector3 old_bottom_line = GetBottomLine();
        
        transform.position += new Vector3(curr_speed_x, curr_speed_y, 0);
        float left_edge = transform.position.x - spriteRenderer.size.x / 2;
        float right_edge = transform.position.x + spriteRenderer.size.x / 2;
        float offset_x = gameController.OutOfBound(left_edge, right_edge);
        if (offset_x != 0)
        {
            curr_speed_x = -curr_speed_x;
            transform.position += new Vector3(offset_x, 0, 0);
        }        

        
        Vector3 new_bottom_line = GetBottomLine();

        curr_speed_y -= gravity;
        if (curr_speed_y < 0)
        {

            float offset_y = gameController.GameOver(new_bottom_line.y);
            if (offset_y > 0)
            {
                DeadAction(offset_y);
            }
            else if ((offset_y = brickManager.CollideCheck(old_bottom_line, new_bottom_line)) != 0)
            {
                Vector3 revised_position = transform.position;
                revised_position.y += offset_y;
                transform.position = revised_position;
                curr_speed_x = 0;
                curr_speed_y = speedY;
            }
        }
    }

    private Vector3 GetBottomLine()
    {
        Vector3 bottomLine = new Vector3();
        bottomLine.x = transform.position.x + lineRenderer.GetPosition(0).x;
        bottomLine.y = transform.position.y + lineRenderer.GetPosition(0).y;
        bottomLine.z = transform.position.x + lineRenderer.GetPosition(1).x;
        return bottomLine;
    }

    private void UpdateLine(float offset)
    {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        for (int i=0; i<lineRenderer.positionCount; i++)
        {
            positions[i].x += offset;
        }
        lineRenderer.SetPositions(positions);
    }

    private void DeadAction(float offset)
    {
        curr_speed_x = 0;
        curr_speed_y = 0;
        gravity = 0;
        transform.position += new Vector3(0, offset, 0);
        spriteRenderer.color = Color.grey;
    }

}
