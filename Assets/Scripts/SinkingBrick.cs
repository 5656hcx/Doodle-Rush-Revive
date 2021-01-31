using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingBrick : Brick
{
    public float sinkingSpeed;
    public float sinkingAcceleration;
    private bool sinking;

    protected override void Start()
    {
        sinking = false;
        base.Start();
    }

    protected override void OnBecameInvisible()
    {
        speedY = 0;
        sinking = false;
        sinkingSpeed -= sinkingAcceleration;
        base.OnBecameInvisible();
    }

    public override float GetSpeedX()
    {
        return sinking ? 0 : base.GetSpeedX();
    }

    public override void SetSpeedX(float newSpeed)
    {
        if (sinking == false) base.SetSpeedX(newSpeed);
    }

    public override void OnHitAction()
    {
        if (sinking == false)
        {
            sinking = true;
            speedY = sinkingSpeed;
        }
    }

}
