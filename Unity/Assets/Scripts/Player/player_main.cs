using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_main : MonoBehaviour
{
    #region Variables
    float positionX;
    float positionY;

    float strength;
    float dexterity;
    float intelligence;
    string characterName;
    string className;
    int level;
    int experiencePoints;
    #endregion

    #region External Components
    Grid grid;
    gameManager gm;

    gridSense gridSense;
    #endregion

    #region Core Functions
    void Start()
    {
        initializeCharacter();
        gm = FindObjectOfType<gameManager>();
        gridSense = FindObjectOfType<gridSense>();
        grid = FindObjectOfType<Grid>();
        positionX = 0f;
        positionY = 0f;
    }
    void Update()
    {
        Vector2 startPos = transform.position;
        checkMove();
        gameObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(positionX, positionY));
    }
    #endregion

    #region Functions
    void initializeCharacter()
    {
        characterName = character_static.name;
        className = character_static.s_class;
        level = character_static.level;
        experiencePoints = character_static.experience_points;
        strength = character_static.strength;
        dexterity = character_static.dexterity;
        intelligence = character_static.intelligence;
    }
    void checkMove()
    {
        if (Input.GetKeyDown(KeyCode.W) && gridSense.topMid.isWall == false)
        {
            if (gridSense.topMid.isEnemy == true)
            {
                damageEnemy(gridSense.topMid, 1);
            }
            else
            {
                positionY += grid.cellSize.y;
            }
            gm.passTurn();
        }
        if (Input.GetKeyDown(KeyCode.S) && gridSense.botMid.isWall == false)
        {
            if (gridSense.botMid.isEnemy == true)
            {
                damageEnemy(gridSense.botMid, 1);
            }
            else
            {
                positionY -= grid.cellSize.y;
            }
            gm.passTurn();
        }
        if (Input.GetKeyDown(KeyCode.D) && gridSense.midRight.isWall == false)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
            if (gridSense.midRight.isEnemy == true)
            {
                damageEnemy(gridSense.midRight, 1);
            }
            else
            {
                positionX += grid.cellSize.x;
            }
            gm.passTurn();
        }
        if (Input.GetKeyDown(KeyCode.A) && gridSense.midLeft.isWall == false)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
            if (gridSense.midLeft.isEnemy == true)
            {
                damageEnemy(gridSense.midLeft, 1);
            }
            else
            {
                positionX -= grid.cellSize.x;
            }
            gm.passTurn();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gm.passTurn();
        }
    }
    void damageEnemy(gridSensor sensor, float attackValue)
    {
        sensor.enemy.damageEnemy(attackValue);
    }
    #endregion

}
