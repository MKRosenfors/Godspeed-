using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AbstractTest", menuName = "My Game/AbstractTest")]
public abstract class AbstractTest : ScriptableObject
{
    public abstract void PerformAction();
}
