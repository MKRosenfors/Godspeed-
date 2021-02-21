using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMain : MonoBehaviour
{
    #region Variables
    public float hitPoints;
    public int blockChance;
    
    public bool chasing;
    Vector2 position;
    Color damageState;
    float time;
    #endregion

    #region External Components
    Grid grid;
    player_main player;

    #endregion

    #region Core Funtions
    void Start()
    {
        player = FindObjectOfType<player_main>();
        grid = FindObjectOfType<Grid>();
        position = transform.position;
    }
    void Update()
    {
        DamageColor();
        CheckHealth();
    }
    #endregion

    #region Functions
    public void ExecuteAI()
    {
        Move();
    }
    void Move()
    {
        if (chasing == true)
        {
            if (Mathf.Abs(position.x - player.transform.position.x) >= Mathf.Abs(position.y - player.transform.position.y))
            {
                if (position.x - player.transform.position.x < 0)
                {
                    position.x += 1;
                }
                else
                {
                    position.x -= 1;
                }
            }
            else
            {
                if (position.y - player.transform.position.y < 0)
                {
                    position.y += 1;
                }
                else
                {
                    position.y -= 1;
                }
            }
            transform.position = position;
        }
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
    void CheckHealth()
    {
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
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
