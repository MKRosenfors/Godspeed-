using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData", menuName = "My Game/Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public Sprite enemyIdleSprite;
    public float sightRange;
    public float strength;
    public float dexterity;
    public float constitution;
    public float intelligence;
    public float wisdom;
    public float charisma;

    public TurnBehaviour enemyBehaviour;
}
