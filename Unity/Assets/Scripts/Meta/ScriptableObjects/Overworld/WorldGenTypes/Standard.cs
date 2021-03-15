using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WorldGen", menuName = "My Game/World/World Generation/Standard")]
public class Standard : WorldGen
{
    public override List<worldNode> CreateWorld(int numberOfNodes, float nodeSpread)
    {
        List<worldNode> worldNodes = new List<worldNode>();
        string nodeName;
        string nodeDescription;
        Vector3 nodeLocation;
        List<worldNode> nodeChildren;
        worldNode nodeParent;

        GameObject newObject = new GameObject("King's Castle");
        newObject.AddComponent<worldNode>();
        newObject.GetComponent<worldNode>().nodeName = "King's Castle";
        newObject.GetComponent<worldNode>().nodeDescription = "This is the center of the kingdom";
        newObject.GetComponent<worldNode>().nodeLocation = Vector3.zero;
        newObject.GetComponent<worldNode>().childNodes = null;
        newObject.GetComponent<worldNode>().parentNode = null;
        worldNodes.Add(newObject.GetComponent<worldNode>());
        
        for (int i = 0; i < numberOfNodes; i++)
        {

            nodeName = "-Placeholder Name-";
            nodeDescription = "-Placeholder Description-";

            int randomX = Random.Range(-5,5);
            int randomY = Random.Range(-5,5);
            for (int x = 0; x < worldNodes.Count; x++)
            {
                if ((new Vector3(randomX, randomY, 0) - worldNodes[x].nodeLocation).magnitude < nodeSpread)
                {
                    x = 0;
                    randomX = Random.Range(-5, 5);
                    randomY = Random.Range(-5, 5);
                }
            }
            nodeLocation = new Vector3(randomX, randomY, 0);
            nodeChildren = null;
            nodeParent = worldNodes[0];

            GameObject newNode = new GameObject(nodeName + "Node");
            newNode.AddComponent<worldNode>();
            newNode.GetComponent<worldNode>().nodeName = nodeName;
            newNode.GetComponent<worldNode>().nodeDescription = nodeDescription;
            newNode.GetComponent<worldNode>().nodeLocation = nodeLocation;
            newNode.GetComponent<worldNode>().childNodes = nodeChildren;
            newNode.GetComponent<worldNode>().parentNode = worldNodes[0];
            worldNodes.Add(newNode.GetComponent<worldNode>());

        }
        return worldNodes;
    }
}
