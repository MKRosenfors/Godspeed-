using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public bool isWalkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public PathNode parent;

    public PathNode(bool _isWalkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        isWalkable = _isWalkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }
    public int fCost{
        get{
            return gCost + hCost;
        }
    }
}
