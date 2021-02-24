using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridField : MonoBehaviour
{
    public bool displayGridGizmos;
    public Transform player;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    PathNode[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    public Tilemap unwalkableMap;

    private void Awake()
    {
        unwalkableMask = LayerMask.GetMask("unwalkable");
        nodeDiameter = nodeRadius * 2;
        gridSizeX =Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }
    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
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
                grid[x, y] = new PathNode(walkable, worldPoint, x, y);
            }
        }
    }
    public List<PathNode> GetNeighbours(PathNode node)
    {
        List<PathNode> neighbours = new List<PathNode>();
        for (int x = -1; x <= 1; x++){
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
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
        if (grid != null && displayGridGizmos == true)
        {
            foreach (PathNode n in grid)
            {
                Gizmos.color = (n.isWalkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
