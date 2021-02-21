using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    #region Variables
    private GameObject[] npcArray;
    private enemyMain[] enemies;

    #endregion
    #region External Components

    #endregion
    #region Core Functions
    void Start()
    {
        initializeArrays();
    }
    void Update()
    {

    }
    #endregion
    #region Functions
    void initializeArrays()
    {
        npcArray = GameObject.FindGameObjectsWithTag("enemy");

        enemies = new enemyMain[npcArray.Length];

        for (int i = 0; i < npcArray.Length; i++)
        {
            enemies[i] = npcArray[i].GetComponent<enemyMain>();
        }
    }
    public void passTurn()
    {
        executeNPCAI();
    }
    void executeNPCAI()
    {
        foreach (enemyMain npc in enemies)
        {
            npc.ExecuteAI();
        }
    }
    #endregion
}
