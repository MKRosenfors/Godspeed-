using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMain : MonoBehaviour, IsDamagable
{
    #region Variables
    public float hitPoints;
    public int blockChance;
    public float spriteFollowSpeed;
    public float attackSpriteSpeed;

    Color damageState;
    float time;

    public float sightRange;
    int lostTrackCounter;
    public bool hasDestination;
    public Vector3 homeDestination;
    public Vector3 currentDestination;

    TurnAction turnAction;

    public Vector3[] pathToTarget;
    LayerMask mask;

    #endregion

    #region Components
    List<GameObject> targets = new List<GameObject>();
    GameObject currentTarget;
    player_main player;
    public GridField grid;
    gameManager gm;
    public EnemyData EnemyType;
    TurnBehaviour ai;

    GameObject sprite;
    Rigidbody2D rb;
    #endregion

    #region Core Funtions
    void Start()
    {
        ai = EnemyType.enemyBehaviour;
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
    public void ExecuteAI()
    {
        tools.SetPositionOnGridOccupied(grid, transform.position, null, false);

        pathToTarget = null;
        GameObject oldTarget = currentTarget;
        currentTarget = FindClosestVisibleTarget();
        if (currentTarget != null)
        {
            currentDestination = currentTarget.transform.position;
            hasDestination = true;
            lostTrackCounter = 0;
            //The enemy can see and is tracking the target
        }
        else if (oldTarget != null && lostTrackCounter < 6)
        {
            lostTrackCounter++;
            currentTarget = oldTarget;
            //The enemy has lost sight but is still moving towards the target
        }
        else
        {
            oldTarget = null;
            currentDestination = homeDestination;
            lostTrackCounter = 0;
            //The enemy has lost sight and given up the search
        }

        turnAction = ai.MakeDecision(this, currentTarget);

        if (turnAction == TurnActions.Attack)
        {
            AlignToTarget(currentDestination);
            Attack(currentTarget.GetComponent<IsDamagable>(), Attacks.basic_melee);
        }
        if (turnAction == TurnActions.Move)
        {
            AlignToTarget(currentDestination);
            if (pathToTarget == null)
            {
                pathToTarget = Pathfinding.FindPath(transform.position, currentDestination);
            }
            Move(pathToTarget[0]);
        }
        if (turnAction == TurnActions.UseAbility)
        {
            // Not implemented
        }
        if (turnAction == TurnActions.Idle)
        {
            // Does nothing
        }

        tools.SetPositionOnGridOccupied(grid, transform.position, gameObject, true);
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
    void Attack(IsDamagable target, Attack attackType)
    {
        if (target != null && attackType != null)
        {
            target.Damage(attackType);

            Vector3 spriteTargetPos = (transform.position + (currentTarget.transform.position - transform.position) * 0.2f);
            StartCoroutine(tools.MoveToAndBack(sprite.transform, gameObject.transform.position, spriteTargetPos, attackSpriteSpeed));
        }
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
        if (rnd > blockChance && incomingAttack.damageSource.melee == true)
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
