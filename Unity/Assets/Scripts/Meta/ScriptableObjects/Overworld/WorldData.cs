using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WorldData", menuName = "My Game/World/World Data")]
public class WorldData : ScriptableObject
{
    public List<Sprite> nodeSprites;
    public Material lineMaterial;

    public WorldGen WorldGenType;


}
