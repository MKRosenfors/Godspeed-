using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldGen : ScriptableObject
{
    public abstract List<worldNode> CreateWorld(int numberOfNodes, float nodeSpread);
}
