using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sight : MonoBehaviour
{
    enemyMain parent;
    private void Awake()
    {
        parent = GetComponentInParent<enemyMain>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            parent.AddTarget(other.gameObject);
        }
    }
}
