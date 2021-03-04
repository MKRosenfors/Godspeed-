using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_main : MonoBehaviour, IsDamagable
{
    #region Variables
    public float spriteFollowSpeed;
    public float attackSpriteSpeed;
    public bool isMyTurn;
    float positionX;
    float positionY;
    float strength;
    float dexterity;
    float intelligence;
    string characterName;
    string className;
    int level;
    int experiencePoints;
    
    Color damageState;
    public int blockChance;
    public float hitPoints;
    float time;

    Vector2 positionChange;
    string turnActionInput;
    #endregion

    #region External Components
    Grid grid;
    GridField gridField;
    gameManager gm;
    GameObject sprite;
    GameObject cameraObject;

    gridSense gridSense;
    #endregion

    #region Core Functions
    void Start()
    {
        turnActionInput = null;
        initializeCharacter();
        gridField = FindObjectOfType<GridField>();
        cameraObject = GetComponentInChildren<Camera>().gameObject;
        sprite = GetComponentInChildren<SpriteRenderer>().gameObject;
        gm = FindObjectOfType<gameManager>();
        gridSense = FindObjectOfType<gridSense>();
        grid = FindObjectOfType<Grid>();
        positionX = transform.position.x;
        positionY = transform.position.y;
    }
    void Update()
    {
        DamageColor();
        checkInput();
        if (isMyTurn == true && turnActionInput != null)
        {
            ExecuteTurn();
            turnActionInput = null;
        }
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

    void ExecuteTurn()
    {
        if (turnActionInput == "skip")
        {

        }
        if (turnActionInput == "move")
        {
            Move(positionChange);
        }
        if (turnActionInput == "attack")
        {

        }
        gm.PassTurnTo("environment");
    }
    void Move(Vector3 changeVector)
    {
        Vector3 newPos = transform.position + changeVector;
        AlignTo(newPos);
        transform.position = newPos;
        gameObject.GetComponent<Rigidbody2D>().MovePosition(newPos);

        sprite.transform.position = newPos - changeVector;
        cameraObject.transform.position = new Vector3(newPos.x - changeVector.x, newPos.y - changeVector.y, -10);
        StartCoroutine(tools.MoveTo(sprite.transform, newPos, spriteFollowSpeed));
        StartCoroutine(tools.MoveTo(cameraObject.transform, new Vector3(newPos.x, newPos.y, -10), spriteFollowSpeed));
    }
    void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePos = cameraObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            Vector3[] path = Pathfinding.FindPath(transform.position, mousePos);
            if (path != null)
            {
                positionChange = path[0] - transform.position;
                turnActionInput = "move";
                if (gridSense.GetGridSensorFromVector(positionChange).isEnemy)
                {
                    attack(gridSense.GetGridSensorFromVector(positionChange));
                    turnActionInput = "attack";
                }
            }
            else if (gridField.NodeFromWorldPoint(mousePos).isWalkable)
            {
                positionChange = gridField.NodeFromWorldPoint(mousePos).worldPosition - transform.position;
                turnActionInput = "move";
                if (gridSense.GetGridSensorFromVector(positionChange).isEnemy)
                {
                    attack(gridSense.GetGridSensorFromVector(positionChange));
                    turnActionInput = "attack";
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.W) && gridSense.topMid.isWall == false)
        {
            if (gridSense.topMid.isEnemy == true)
            {
                turnActionInput = "attack";
                attack(gridSense.topMid);
            }
            else
            {
                positionChange.x = 0;
                positionChange.y = 1;
                turnActionInput = "move";
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && gridSense.botMid.isWall == false)
        {
            if (gridSense.botMid.isEnemy == true)
            {
                turnActionInput = "attack";
                attack(gridSense.botMid);
            }
            else
            {
                positionChange.x = 0;
                positionChange.y = -1;
                turnActionInput = "move";
            }
        }
        if (Input.GetKeyDown(KeyCode.D) && gridSense.midRight.isWall == false)
        {
            if (gridSense.midRight.isEnemy == true)
            {
                turnActionInput = "attack";
                attack(gridSense.midRight);
            }
            else
            {
                positionChange.x = 1;
                positionChange.y = 0;
                turnActionInput = "move";
            }
        }
        if (Input.GetKeyDown(KeyCode.A) && gridSense.midLeft.isWall == false)
        {
            if (gridSense.midLeft.isEnemy == true)
            {
                turnActionInput = "attack";
                attack(gridSense.midLeft);
            }
            else
            {
                positionChange.x = -1;
                positionChange.y = 0;
                turnActionInput = "move";
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            turnActionInput = "skip";
        }

    }
    void attack(gridSensor sensor)
    {
        AlignTo(sensor.transform.position);
        Attack basicAttack = new Attack(1, "melee", "physical");
        sensor.attackableObject.GetComponent<IsDamagable>().Damage(basicAttack);
        Vector3 spriteTargetPos = (transform.position + (sensor.transform.position - transform.position) * 0.2f);
        StartCoroutine(tools.MoveToAndBack(sprite.transform, spriteTargetPos, attackSpriteSpeed));
    }
    public void Damage(Attack incomingAttack)
    {
        int rnd = Random.Range(0, 101);
        if (rnd > blockChance && incomingAttack.damageSource == "melee")
        {
            hitPoints -= incomingAttack.damageValue;
            damageState = Color.red;
            time = Time.time + 0.2f;
        }
        else
        {
            damageState = new Color(0, 0, 1f, 0.5f);
            time = Time.time + 0.2f;
        }
    }
    void DamageColor() // make this into a coroutine && abstract it
    {
        if (time > Time.time)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = damageState;
        }
        else
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }
    void AlignTo(Vector3 target)
    {
        if (target.x > transform.position.x)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false; ;
        }
        else
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true; ;
        }
    }
    #endregion

}
