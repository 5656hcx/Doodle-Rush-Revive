using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingBrick : Brick
{
	public float sinkingSpeed;
	public float sinkingAcceleration;
	private bool sinking;

	public override void OnHitAction()
	{
		if (!sinking)
		{
			sinking = true;
			speedY = sinkingSpeed;
		}
	}

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

}
