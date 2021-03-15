using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ranged", menuName = "My Game/Enemy/Behaviours/Ranged")]
public class Ranged : TurnBehaviour
{
    public override TurnAction MakeDecision(enemyMain actor, GameObject target)
    {
        return TurnActions.Idle;
    }
}
