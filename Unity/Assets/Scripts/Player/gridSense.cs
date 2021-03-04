using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridSense : MonoBehaviour
{
    public gridSensor topLeft;
    public gridSensor topMid;
    public gridSensor topRight;
    public gridSensor midLeft;
    public gridSensor midRight;
    public gridSensor botLeft;
    public gridSensor botMid;
    public gridSensor botRight;

    public gridSensor GetGridSensorFromVector(Vector3 relationToPlayer)
    {
        if (relationToPlayer.y == 1)
        {
            if (relationToPlayer.x == -1)
            {
                return topLeft;
            }
            if (relationToPlayer.x == 0)
            {
                return topMid;
            }
            if (relationToPlayer.x == 1)
            {
                return topRight;
            }
        }
        if (relationToPlayer.y == 0)
        {
            if (relationToPlayer.x == -1)
            {
                return midLeft;
            }
            if (relationToPlayer.x == 1)
            {
                return midRight;
            }
        }
        if (relationToPlayer.y == -1)
        {
            if (relationToPlayer.x == -1)
            {
                return botLeft;
            }
            if (relationToPlayer.x == 0)
            {
                return botMid;
            }
            if (relationToPlayer.x == 1)
            {
                return botRight;
            }
        }
        return null;
    }
}
