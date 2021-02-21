using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridField : MonoBehaviour
{
    public Transform player;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    PathNode[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    public Tilemap unwalkableMap;

    private void Start()
    {
        unwalkableMask = LayerMask.GetMask("unwalkable");
        nodeDiameter = nodeRadius * 2;
        gridSizeX =Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }
    void CreateGrid()
    {
        grid = new PathNode[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y*nodeDiameter+nodeRadius);
                bool walkable = (unwalkableMap.GetTile(unwalkableMap.WorldToCell(worldPoint)) == null);
                grid[x, y] = new PathNode(walkable, worldPoint);
            }
        }
    }

    public PathNode NodeFromWorldPoint(Vector3 worldPoint)
    {
        float percentX = (worldPoint.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPoint.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y));

        if (grid!=null)
        {
            //PathNode playerNode = NodeFromWorldPoint(player.position);
            foreach (PathNode n in grid)
            {
                Gizmos.color = (n.isWalkable) ? Color.white:Color.red;
                //if (playerNode == n)
                //{
                //    Gizmos.color = Color.cyan;
                //}
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
