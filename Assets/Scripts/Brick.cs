using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public float speedX;
    public float speedY;
    protected bool reusable;

    protected virtual void Start()
    {
        reusable = false;
    }

    protected virtual void OnBecameInvisible()
    {
        reusable = true;
    }

    public virtual void SetSpeedX(float newSpeed)
    {
        speedX = newSpeed;
    }

    public virtual float GetSpeedX()
    {
        return speedX;
    }

    public virtual void SetSpeedY(float newSpeed)
    {
        speedY = newSpeed;
    }

    public virtual float GetSpeedY()
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

    public virtual void OnHitAction()
    {
        // Do nothing for Static Brick
    }

}
