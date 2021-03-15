using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Melee", menuName = "My Game/Enemy/Behaviours/Melee")]
public class Melee : TurnBehaviour
{
    public override TurnAction MakeDecision(enemyMain actor, GameObject target)
    {
        if (actor.transform.position == actor.homeDestination && actor.currentDestination == actor.homeDestination)
        {
            actor.hasDestination = false;
        }
        if (target != null)
        {
            actor.currentDestination = target.transform.position;
            actor.hasDestination = true;
            if ((target.transform.position - actor.transform.position).magnitude < 1.5f)
            {
                //Choose attack function and apply it to enemy main
                return TurnActions.Attack;
            }
            else
            {
                return TurnActions.Move;
            }
        }
        else if (actor.hasDestination)
        {
            if ((actor.currentDestination - actor.transform.position).magnitude > 1.5f)
            {
                return TurnActions.Move;
            }
            else
            {
                if (actor.currentDestination != actor.transform.position && actor.grid.NodeFromWorldPoint(actor.currentDestination).isOccupied == false)
                {
                    actor.pathToTarget = new Vector3[1];
                    actor.pathToTarget[0] = actor.currentDestination;
                    return TurnActions.Move;
                }
                else
                {
                    actor.currentDestination = actor.homeDestination;
                    actor.hasDestination = true;
                    return TurnActions.Move;
                }
            }
        }
        else if(actor.transform.position != actor.homeDestination)
        {
            actor.currentDestination = actor.homeDestination;
            actor.hasDestination = true;
            return TurnActions.Move;
        }
        else
        {
            return TurnActions.Idle;
        }
    }
}
