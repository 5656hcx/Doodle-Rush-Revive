using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct Skin
{
    public Sprite sprite;
    public float x0;
    public float x1;
    public float bot;
    public float edge;
    public float flipOffset;

    public Skin(Sprite sp, float x0, float x1, float bot, float edge)
    {
        this.sprite = sp;
        this.x0 = x0;
        this.x1 = x1;
        this.bot = bot;
        this.edge = edge;
        this.flipOffset = -1 * (x0 + x1);
    }
}

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

    public float dyingSpeedY;
    public float dyingGravityScale;

    public Sprite[] sprites;
    private Skin[] skins;
    private static int skinIndex = 0;

    void Start()
    {
        curr_speed_x = 0;
        curr_speed_y = force * speedY;
        skins = SkinHelper.LoadSkins(sprites);
        spriteRenderer.sprite = skins[skinIndex].sprite;
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
                CheckSceneEdge();
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
        float offset = spriteRenderer.flipX ? skins[skinIndex].flipOffset : 0;
        Vector3 vec = new Vector3();
        vec.x = pos.x + offset + skins[skinIndex].x0;
        vec.y = pos.y + skins[skinIndex].bot;
        vec.z = pos.x + offset + skins[skinIndex].x1;
        return vec;
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
    private void CheckSceneEdge()
    {
        float offset = gameController.BoundCheck(new_pos.x, skins[skinIndex].edge);
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

    public void NewLook()
    {
    	if (++skinIndex == skins.Length) skinIndex = 0;
    	spriteRenderer.sprite = skins[skinIndex].sprite;
    }
}
