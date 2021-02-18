using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_main : MonoBehaviour
{
    #region Variables
    float positionX;
    float positionY;
    #endregion

    #region External Components
    Grid grid;
    #endregion

    #region Core Functions
    void Start()
    {
        grid = FindObjectOfType<Grid>();
        positionX = 0.5f;
        positionY = 0.5f;
    }
    void Update()
    {
        gameObject.transform.position = new Vector2(positionX, positionY);
        if (Input.GetKeyDown(KeyCode.W))
        {
            positionY += grid.cellSize.y;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            positionY -= grid.cellSize.y;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            positionX += grid.cellSize.x;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            positionX -= grid.cellSize.x;
        }
    }
    #endregion

    #region Functions

    #endregion

}
