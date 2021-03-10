using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class tools
{
    public static IEnumerator MoveTo(Transform objectToMove, Vector3 endPos, float speed)
    {
        float step = (speed / (objectToMove.position - endPos).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step;
            objectToMove.position = Vector3.Lerp(objectToMove.position, endPos, t);
            yield return new WaitForFixedUpdate();
        }
        objectToMove.position = endPos;
    }
    public static IEnumerator MoveToAndBack(Transform objectToMove, Vector3 startPos, Vector3 endPos, float speed)
    {
        float step = (speed / (startPos - endPos).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step;
            objectToMove.position = Vector3.Lerp(objectToMove.position, endPos, t);
            yield return new WaitForFixedUpdate();
        }
        objectToMove.position = endPos;

        step = (speed / (objectToMove.position - startPos).magnitude) * Time.fixedDeltaTime;
        t = 0;
        while (t <= 1.0f)
        {
            t += step;
            objectToMove.position = Vector3.Lerp(objectToMove.position, startPos, t);
            yield return new WaitForFixedUpdate();
        }
        objectToMove.position = startPos;
    }
    public static void SetPositionOnGridWalkable(GridField grid, Vector3 position, bool walkableState)
    {
        grid.NodeFromWorldPoint(position).isWalkable = walkableState;
    }
    public static void SetPositionOnGridOccupied(GridField grid, Vector3 position , bool occupiedState)
    {
        grid.NodeFromWorldPoint(position).isOccupied = occupiedState;
    }
    public static void SetPositionOnGridOccupied(GridField grid, Vector3 position, GameObject objectOnTile, bool occupiedState)
    {
        grid.NodeFromWorldPoint(position).isOccupied = occupiedState;
        grid.NodeFromWorldPoint(position).objectOnTile = objectOnTile;
    }
}
