using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnBehaviour : ScriptableObject
{
    public abstract TurnAction MakeDecision(enemyMain actor, GameObject target);
}
