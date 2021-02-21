using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public bool isWalkable;
    public Vector3 worldPosition;

    public PathNode(bool _isWalkable, Vector3 _worldPos)
    {
        isWalkable = _isWalkable;
        worldPosition = _worldPos;
    }
}
