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

    public bool chasing;
    Vector2 position;
    Color damageState;
    float time;

    List<GameObject> targets = new List<GameObject>();
    GameObject currentTarget;
    Vector3[] pathToTarget;
    LayerMask mask;
    #endregion

    #region External Components
    Grid grid;
    player_main player;

    GameObject sprite;
    #endregion

    #region Core Funtions
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>().gameObject;
        mask = LayerMask.GetMask("unwalkable");
        player = FindObjectOfType<player_main>();
        grid = FindObjectOfType<Grid>();
        position = transform.position;
    }
    void Update()
    {
        DamageColor();
    }
    #endregion

    #region Functions
    public void ExecuteAI(int i)
    {
        currentTarget = FindClosestVisibleTarget();
        if (targets != null && (targets[0].transform.position - transform.position).magnitude > 1.5)
        {
            if (currentTarget != null)
            {
                pathToTarget = Pathfinding.FindPath(transform.position, currentTarget.transform.position);
                AlignToTarget();
                Move();
            }
        }
        else if (currentTarget != null)
        {
            Attack basicAttack = new Attack(1, "melee", "physical");
            currentTarget.GetComponent<IsDamagable>().Damage(basicAttack);
            Vector3 spriteTargetPos = (transform.position + (currentTarget.transform.position - transform.position) * 0.2f);
            StartCoroutine(tools.MoveToAndBack(sprite.transform, spriteTargetPos, attackSpriteSpeed));
        }

        
    }
    public void AddTarget(GameObject newTarget)
    {
        targets.Add(newTarget);
    }
    GameObject FindClosestVisibleTarget()
    {
        GameObject target = null;
        for (int i = 0; i < targets.Count; i++)
        {
            if (Physics.Raycast(transform.position, targets[i].transform.position, Mathf.Infinity, mask) == false)
            {
                if (target != null)
                {
                    if ((targets[i].transform.position - transform.position).magnitude < (target.transform.position - transform.position).magnitude)
                    {
                        target = targets[i];
                    }
                }
                else
                {
                    target = targets[i];
                }
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
    void Move()
    {
        if (pathToTarget!=null && pathToTarget.Length != 0)
        {
            Vector2 origPos = transform.position;
            transform.position = pathToTarget[0];
            sprite.transform.position = origPos;
            StartCoroutine(tools.MoveTo(sprite.transform, pathToTarget[0], spriteFollowSpeed));
        }
    }
    void AlignToTarget()
    {
        if (currentTarget.transform.position.x < transform.position.x)
        {
            sprite.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            sprite.GetComponent<SpriteRenderer>().flipX = false;
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
