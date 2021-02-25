using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    #region Variables
    private GameObject[] npcArray;
    private List<enemyMain> enemies;

    string passTurn;
    #endregion
    #region External Components
    player_main player;

    #endregion
    #region Core Functions
    void Start()
    {
        InitializeArrays();
        player = FindObjectOfType<player_main>();
        passTurn = "player";
    }
    void LateUpdate()
    {
        CheckDeath();
        if (passTurn == "environment")
        {
            ExecuteNPCAI();
            PassTurnTo("player");
        }
        if (passTurn == "player")
        {
            player.isMyTurn = true;
        }
    }
    #endregion
    #region Functions
    void InitializeArrays()
    {
        npcArray = GameObject.FindGameObjectsWithTag("enemy");

        enemies = new List<enemyMain>();

        for (int i = 0; i < npcArray.Length; i++)
        {
            enemies.Add(npcArray[i].GetComponent<enemyMain>());
        }
    }
    public void PassTurnTo(string turnName)
    {
        passTurn = turnName;
    }
    void ExecuteNPCAI()
    {
        foreach (enemyMain npc in enemies)
        {
            npc.ExecuteAI(1);
        }
    }
    void CheckDeath()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].hitPoints <= 0)
            {
                GameObject deadObject = enemies[i].gameObject;
                enemies.Remove(enemies[i]);
                Destroy(deadObject);
            }
        }
    }
    #endregion
}
