using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    protected virtual void Start()
    {
        reusable = false;
    }

    public void SetSpeedX(float newSpeed)
    {
        speedX = newSpeed;
    }

    public float GetSpeedX()
    {
        return speedX;
    }

    public void SetSpeedY(float newSpeed)
    {
        speedY = newSpeed;
    }

    public float GetSpeedY()
    {
        return speedY;
    }

    public bool Reusable()
    {
        return reusable;
    }

    public void BeingUsed()
    {
        reusable = false;
    }

    protected virtual void OnBecameInvisible()
    {
        reusable = true;
    }

    public virtual void OnHitAction()
    {
        // Do nothing for Static Brick
    }

    protected float speedX;
    protected float speedY;
    protected bool reusable;
}
