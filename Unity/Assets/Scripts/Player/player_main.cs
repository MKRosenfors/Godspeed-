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
    GridField grid;
    gameManager gm;
    GameObject sprite;
    GameObject cameraObject;

    #endregion

    #region Core Functions
    void Start()
    {
        turnActionInput = null;
        initializeCharacter();
        grid = FindObjectOfType<GridField>();
        cameraObject = GetComponentInChildren<Camera>().gameObject;
        sprite = GetComponentInChildren<SpriteRenderer>().gameObject;
        gm = FindObjectOfType<gameManager>();
        positionX = transform.position.x;
        positionY = transform.position.y;
        tools.SetPositionOnGridOccupied(grid, transform.position, gameObject, true);
        gm.playerFriends.Add(gameObject);
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
        tools.SetPositionOnGridOccupied(grid, transform.position, null, false);

        if (turnActionInput == "idle")
        {

        }
        else if (turnActionInput == "move")
        {
            Move(positionChange);
        }
        else if (turnActionInput == "attack")
        {

        }
        else
        {
            print("turn action input not recognized");
        }

        tools.SetPositionOnGridOccupied(grid, transform.position, gameObject, true);
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
        if (Input.GetKeyDown(KeyCode.Mouse0)) // left mouse button
        {
            if (grid.NodeFromWorldPoint(cameraObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition)).isOccupied) //attack and interact
            {
                GameObject target = grid.NodeFromWorldPoint(cameraObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition)).objectOnTile;
                if (target.GetComponent<IsDamagable>() != null && (target.transform.position - transform.position).magnitude < 1.75 && target != gameObject) //attack
                {
                    Attack basicAttack = new Attack(1, "melee", "physical");
                    attack(target, basicAttack);
                    turnActionInput = "attack";
                }
                else if (target == gameObject)
                {
                    turnActionInput = "idle";
                }
                if (true)
                {
                    // interact
                }

            } // attack and interact
            else //move
            {
                int pathableNeighbours = 0;
                foreach (PathNode neighbour in grid.GetNeighbours(grid.NodeFromWorldPoint(transform.position))) //Check neighbours to see if a path is possible or not
                {
                    if (neighbour.isOccupied == false && neighbour.isWalkable == true)
                    {
                        pathableNeighbours++;
                    }
                }
                if (pathableNeighbours != 0)
                {
                    Vector3 mousePos = cameraObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                    Vector3[] path = Pathfinding.FindPath(transform.position, mousePos);
                    if (path != null && (mousePos - transform.position).magnitude > 1.75f)
                    {
                        positionChange = path[0] - transform.position;
                        turnActionInput = "move";
                    }
                    else if (grid.NodeFromWorldPoint(mousePos).isWalkable)
                    {
                        positionChange = grid.NodeFromWorldPoint(mousePos).worldPosition - transform.position;
                        turnActionInput = "move";
                    }
                }
            } // move
        } // left mouse button
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (grid.NodeFromWorldPoint(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z)).isOccupied) // attack and interact
            {
                GameObject target = grid.NodeFromWorldPoint(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z)).objectOnTile;
                if (target.GetComponent<IsDamagable>() != null && (target.transform.position - transform.position).magnitude < 1.75) //attack
                {
                    Attack basicAttack = new Attack(1, "melee", "physical");
                    attack(target, basicAttack);
                    turnActionInput = "attack";
                }
                if (true)
                {
                    // interact
                }
            } // attack and interact
            else
            {
                Vector3 movePos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                if (grid.NodeFromWorldPoint(movePos).isWalkable)
                {
                    positionChange = grid.NodeFromWorldPoint(movePos).worldPosition - transform.position;
                    turnActionInput = "move";
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (grid.NodeFromWorldPoint(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z)).isOccupied) // attack and interact
            {
                GameObject target = grid.NodeFromWorldPoint(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z)).objectOnTile;
                if (target.GetComponent<IsDamagable>() != null && (target.transform.position - transform.position).magnitude < 1.75) //attack
                {
                    Attack basicAttack = new Attack(1, "melee", "physical");
                    attack(target, basicAttack);
                    turnActionInput = "attack";
                }
                if (true)
                {
                    // interact
                }
            } // attack and interact
            else
            {
                Vector3 movePos = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                if (grid.NodeFromWorldPoint(movePos).isWalkable)
                {
                    positionChange = grid.NodeFromWorldPoint(movePos).worldPosition - transform.position;
                    turnActionInput = "move";
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (grid.NodeFromWorldPoint(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z)).isOccupied) // attack and interact
            {
                GameObject target = grid.NodeFromWorldPoint(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z)).objectOnTile;
                if (target.GetComponent<IsDamagable>() != null && (target.transform.position - transform.position).magnitude < 1.75) //attack
                {
                    Attack basicAttack = new Attack(1, "melee", "physical");
                    attack(target, basicAttack);
                    turnActionInput = "attack";
                }
                if (true)
                {
                    // interact
                }
            } // attack and interact
            else
            {
                Vector3 movePos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                if (grid.NodeFromWorldPoint(movePos).isWalkable)
                {
                    positionChange = grid.NodeFromWorldPoint(movePos).worldPosition - transform.position;
                    turnActionInput = "move";
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (grid.NodeFromWorldPoint(new Vector3(transform.position.x - 1, transform.position.y, transform.position.z)).isOccupied) // attack and interact
            {
                GameObject target = grid.NodeFromWorldPoint(new Vector3(transform.position.x - 1, transform.position.y, transform.position.z)).objectOnTile;
                if (target.GetComponent<IsDamagable>() != null && (target.transform.position - transform.position).magnitude < 1.75) //attack
                {
                    Attack basicAttack = new Attack(1, "melee", "physical");
                    attack(target, basicAttack);
                    turnActionInput = "attack";
                }
                if (true)
                {
                    // interact
                }
            } // attack and interact
            else
            {
                Vector3 movePos = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                if (grid.NodeFromWorldPoint(movePos).isWalkable)
                {
                    positionChange = grid.NodeFromWorldPoint(movePos).worldPosition - transform.position;
                    turnActionInput = "move";
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            turnActionInput = "idle";
        }

    }
    void attack(GameObject target, Attack attack)
    {
        AlignTo(target.transform.position);
        target.GetComponent<IsDamagable>().Damage(attack);
        Vector3 spriteTargetPos = (transform.position + (target.transform.position - transform.position) * 0.2f);
        StartCoroutine(tools.MoveToAndBack(sprite.transform, gameObject.transform.position, spriteTargetPos, attackSpriteSpeed));
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
        bool origFlip = gameObject.GetComponentInChildren<SpriteRenderer>().flipX;
        if (target.x > transform.position.x)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false; ;
        }
        else
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true; ;
        }
        if (target.x == transform.position.x)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = origFlip;
        }
    }
    #endregion

}
