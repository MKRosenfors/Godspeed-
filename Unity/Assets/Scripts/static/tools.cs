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
    public static IEnumerator MoveToAndBack(Transform objectToMove, Vector3 endPos, float speed)
    {
        Vector3 startPos = objectToMove.transform.position;
        float step = (speed / (objectToMove.position - endPos).magnitude) * Time.fixedDeltaTime;
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
}
