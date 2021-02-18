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

    gridSense gridSense;
    #endregion

    #region Core Functions
    void Start()
    {
        gridSense = FindObjectOfType<gridSense>();
        grid = FindObjectOfType<Grid>();
        positionX = 0f;
        positionY = 0f;
    }
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(positionX, positionY));
        if (Input.GetKeyDown(KeyCode.W) && gridSense.topMid.isWall == false)
        {
            positionY += grid.cellSize.y;
        }
        if (Input.GetKeyDown(KeyCode.S) && gridSense.botMid.isWall == false)
        {
            positionY -= grid.cellSize.y;
        }
        if (Input.GetKeyDown(KeyCode.D) && gridSense.midRight.isWall == false)
        {
            positionX += grid.cellSize.x;
        }
        if (Input.GetKeyDown(KeyCode.A) && gridSense.midLeft.isWall == false)
        {
            positionX -= grid.cellSize.x;
        }
    }
    #endregion

    #region Functions

    #endregion

}
