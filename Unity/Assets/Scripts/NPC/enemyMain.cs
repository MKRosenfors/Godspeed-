using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMain : MonoBehaviour, IsDamagable
{
    #region Variables
    public float sightRange;
    public float hitPoints;
    public int blockChance;
    public float spriteFollowSpeed;
    public float attackSpriteSpeed;

    int lostTrackCounter;
    public bool chasing;
    Color damageState;
    float time;
    bool hasDestination;
    Vector3 homeDestination;
    Vector3 currentDestination;

    List<GameObject> targets = new List<GameObject>();
    GameObject currentTarget;
    Vector3[] pathToTarget;
    LayerMask mask;
    Rigidbody2D rb;
    #endregion

    #region External Components
    player_main player;
    GridField grid;
    gameManager gm;

    GameObject sprite;
    #endregion

    #region Core Funtions
    void Start()
    {
        gm = FindObjectOfType<gameManager>();
        rb = GetComponent<Rigidbody2D>();
        grid = FindObjectOfType<GridField>();
        sprite = GetComponentInChildren<SpriteRenderer>().gameObject;
        mask = LayerMask.GetMask("unwalkable");
        player = FindObjectOfType<player_main>();
        tools.SetPositionOnGridOccupied(grid, transform.position, gameObject, true);

        hasDestination = false;
        currentTarget = null;
        homeDestination = transform.position;
        lostTrackCounter = 0;
    }
    void Update()
    {
        DamageColor();
    }
    #endregion

    #region Functions
    public void ExecuteAI() //rewrite similar to player
    {
        tools.SetPositionOnGridOccupied(grid, transform.position, null, false);

        GameObject oldTarget = currentTarget;
        currentTarget = FindClosestVisibleTarget();
        if (currentTarget != null)
        {
            lostTrackCounter = 0;
        }
        else if (oldTarget != null && lostTrackCounter <= 2)
        {
            lostTrackCounter++;
            currentTarget = oldTarget;
        }
        else
        {
            oldTarget = null;
            lostTrackCounter = 0;
        }

        if (currentTarget != null)
        {
            currentDestination = currentTarget.transform.position;
            hasDestination = true;
            if ((currentTarget.transform.position - transform.position).magnitude > 1.5)
            {
                pathToTarget = Pathfinding.FindPath(transform.position, currentDestination);
                AlignToTarget(currentDestination);
                if (pathToTarget != null)
                {
                    Move(pathToTarget[0]);
                }
            }
            else
            {
                AlignToTarget(currentTarget.transform.position);
                Attack basicAttack = new Attack(1, "melee", "physical");
                currentTarget.GetComponent<IsDamagable>().Damage(basicAttack);
                Vector3 spriteTargetPos = (transform.position + (currentTarget.transform.position - transform.position) * 0.2f);
                StartCoroutine(tools.MoveToAndBack(sprite.transform, spriteTargetPos, attackSpriteSpeed));
            }
        }
        else if (hasDestination == true)
        {
            if ((currentDestination - transform.position).magnitude > 1.5f)
            {
                    pathToTarget = Pathfinding.FindPath(transform.position, currentDestination);
                    AlignToTarget(currentDestination);
                    Move(pathToTarget[0]);
            }
            else
            {
                if (currentDestination != transform.position && grid.NodeFromWorldPoint(currentDestination).isOccupied == false)
                {
                    print("tp");
                    AlignToTarget(currentDestination);
                    Move(currentDestination);
                }
                else
                {
                    currentDestination = homeDestination;
                    hasDestination = true;
                }
            }

        }
        else
        {
            currentDestination = homeDestination;
            hasDestination = true;
        }
        tools.SetPositionOnGridOccupied(grid, transform.position, gameObject, true);
    }
    public void AddTarget(GameObject newTarget)
    {
        targets.Add(newTarget);
    }
    GameObject FindClosestVisibleTarget()
    {
        GameObject target = null;
        for (int i = 0; i < gm.playerFriends.Count; i++)
        {
            if (Physics2D.Raycast(transform.position, (gm.playerFriends[i].transform.position - transform.position), Vector3.Distance(gm.playerFriends[i].transform.position, transform.position), mask) == false 
                && Vector3.Distance(transform.position, gm.playerFriends[i].transform.position) <= sightRange
                || Vector3.Distance(transform.position, gm.playerFriends[i].transform.position) < 1.5f)
            {
                if (target != null)
                {
                    if ((gm.playerFriends[i].transform.position - transform.position).magnitude < (target.transform.position - transform.position).magnitude)
                    {
                        target = gm.playerFriends[i];
                    }
                }
                else
                {
                    target = gm.playerFriends[i];
                }
            }
            else
            {
                print("I don't see target " + gameObject.name);
            }
        }
        return target;
    }
    public void OnPathFound(Vector3[] newPath, bool pathSuccess)
    {
        if (pathSuccess)
        {
            print(newPath.Length);
            pathToTarget = newPath;
        }
    }
    void Move(Vector3 targetPos)
    {
        if (targetPos != null)
        {
            Vector2 origPos = transform.position;
            transform.position = targetPos;
            rb.MovePosition(targetPos);

            sprite.transform.position = origPos;
            StartCoroutine(tools.MoveTo(sprite.transform, targetPos, spriteFollowSpeed));
        }
    }
    void AlignToTarget(Vector3 targetLocation)
    {
        bool origFlip = gameObject.GetComponentInChildren<SpriteRenderer>().flipX;
        if (targetLocation.x < transform.position.x)
        {
            sprite.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            sprite.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (targetLocation.x == transform.position.x)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = origFlip;
        }
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
    void DamageColor() // make this into a coroutine
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
    #endregion

}
