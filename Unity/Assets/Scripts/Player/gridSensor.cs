using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridSensor : MonoBehaviour
{
    #region Variables
    public bool isWall;
    #endregion
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            isWall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            isWall = false;
        }
    }
}
