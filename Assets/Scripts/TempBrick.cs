using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBrick : Brick
{
    public override void OnHitAction()
    {
        reusable = true;
    }
}
