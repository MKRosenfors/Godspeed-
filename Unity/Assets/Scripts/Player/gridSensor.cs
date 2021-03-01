using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridSensor : MonoBehaviour
{
    #region Variables
    public bool isWall;
    public bool isEnemy;
    #endregion

    #region External Components
    public GameObject attackableObject;

    #endregion
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            isWall = true;
        }
        if (other.gameObject.CompareTag("enemy"))
        {
            attackableObject = other.gameObject;
            isEnemy = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            isWall = false;
        }
        if (other.gameObject.CompareTag("enemy"))
        {
            attackableObject = null;
            isEnemy = false;
        }
    }
}
