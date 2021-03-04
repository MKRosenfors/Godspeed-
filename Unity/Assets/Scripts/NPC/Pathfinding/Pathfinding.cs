using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Pathfinding : MonoBehaviour
{
    static Pathfinding instance;
    GridField grid;
    private void Awake()
    {
        instance = this;
        grid = GetComponent<GridField>();
    }
    public static Vector3[] FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        PathNode startNode = instance.grid.NodeFromWorldPoint(startPos);
        PathNode targetNode = instance.grid.NodeFromWorldPoint(targetPos);

        if (startNode.isWalkable && targetNode.isWalkable && (targetNode.worldPosition - startNode.worldPosition).magnitude > 1.5f)
        {
            Heap<PathNode> openSet = new Heap<PathNode>(instance.grid.MaxSize);
            HashSet<PathNode> closedSet = new HashSet<PathNode>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                PathNode currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    sw.Stop();
                    pathSuccess = true;
                    break;
                }

                foreach (PathNode neighbour in instance.grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.isWalkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }
                    int newMovementCostToNeighbour = currentNode.gCost + instance.GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = instance.GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;
                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }
        if (pathSuccess)
        {
            waypoints = instance.RetracePath(startNode, targetNode);
            return waypoints;
        }
        else
        {
            return null;
        }
    }

    Vector3[] RetracePath(PathNode startNode, PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        PathNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] vectorPath = PathToVectorArray(path);
        Array.Reverse(vectorPath);
        return vectorPath;

    }

    Vector3[] PathToVectorArray(List<PathNode> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        for (int i = 1; i < path.Count; i++)
        {
                waypoints.Add(path[i].worldPosition);
        }
        return waypoints.ToArray();
    }

    int GetDistance(PathNode nodeA, PathNode nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        else
        {
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}
