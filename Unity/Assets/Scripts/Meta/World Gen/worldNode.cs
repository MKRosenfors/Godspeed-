using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldNode : MonoBehaviour
{
    public string nodeName;
    public string nodeDescription;
    public Vector3 nodeLocation;
    public List<worldNode> childNodes;
    public worldNode parentNode;

    private void Update()
    {
        
    }
}
