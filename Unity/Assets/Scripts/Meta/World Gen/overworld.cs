using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overworld : MonoBehaviour
{
    #region Variables
    public int numberOfNodes;
    public float nodeSpread;
    List<worldNode> worldNodes;

    #endregion

    #region External Components
    public WorldData worldData;

    #endregion

    #region Core Functions
    private void Start()
    {
        createWorld();
    }
    #endregion

    #region Functions
    void createWorld()
    {
        //This will create the worldNodes, link them together and apply the relevant information through a scriptable object
        // Ex. A node get's created with the nodeData smith_village, 3 child nodes are created related to the smith_village object.
        // smith_village might have potential children: orc-camp, monastery, cultist hideout, crone's hut, Wizard's Tower, other village(maybe)
        worldNodes = worldData.WorldGenType.CreateWorld(numberOfNodes, nodeSpread);
        for (int i = 0; i < worldNodes.Count; i++)
        {
            worldNodes[i].gameObject.transform.position = worldNodes[i].nodeLocation;
            worldNodes[i].gameObject.AddComponent<SpriteRenderer>().sprite = worldData.nodeSprites[0];
            worldNodes[i].gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            if (worldNodes[i].parentNode != null)
            {
                LineRenderer lr = worldNodes[i].gameObject.AddComponent<LineRenderer>();

                lr.positionCount = 2;
                lr.SetPosition(0, worldNodes[i].nodeLocation);
                lr.SetPosition(1, worldNodes[i].parentNode.gameObject.transform.position);

                lr.material = worldData.lineMaterial;
                lr.startWidth = 0.05f;
                lr.endWidth = 0.05f;
                lr.startColor = Color.black;
                lr.endColor = Color.black;
            }
        }
    }
    #endregion

}
