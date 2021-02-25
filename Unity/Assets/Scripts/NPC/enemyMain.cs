using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMain : MonoBehaviour
{
    #region Variables
    public float sightRange;
    public float hitPoints;
    public int blockChance;
    public float spriteFollowSpeed;

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
        FindPathToClosestVisibleTarget();
        if (currentTarget != null)
        {
            AlignToTarget();
        }
        Move();
    }
    public void AddTarget(GameObject newTarget)
    {
        targets.Add(newTarget);
    }
    public void damageEnemy(float damage)
    {
        int rnd = Random.Range(0, 101);
        if (rnd > blockChance)
        {
            hitPoints -= damage;
            damageState = Color.red;
            time = Time.time + 0.2f;
        }
        else
        {
            damageState = new Color(0, 0, 1f, 0.5f);
            time = Time.time + 0.2f;
        }

    }
    void FindPathToClosestVisibleTarget()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (Physics.Raycast(transform.position, targets[i].transform.position, Mathf.Infinity, mask) == false)
            {
                if (currentTarget != null)
                {
                    if ((targets[i].transform.position - transform.position).magnitude < (currentTarget.transform.position - transform.position).magnitude)
                    {
                        currentTarget = targets[i];
                    }
                }
                else
                {
                    currentTarget = targets[i];
                }
            }
        }
        if (currentTarget!=null)
        {
            pathToTarget = Pathfinding.FindPath(transform.position, currentTarget.transform.position);
        }
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

    void DamageColor()
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
