using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    public LineRenderer deadLine;
    public Color alarmColor;
    private float bottom_edge;

    void Awake()
    {
        bottom_edge = deadLine.GetPosition(0).y + deadLine.widthMultiplier / 2;
    }

    public float BoundCheck(float altitude)
    {
        if (altitude < bottom_edge)
        {
            deadLine.startColor = alarmColor;
            deadLine.endColor = alarmColor;
            return bottom_edge - altitude;
        }
        return 0;
    }

}
