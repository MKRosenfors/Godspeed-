using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class character_select : MonoBehaviour
{
    #region Variables
    #endregion

    #region External Components
    public characterInfo characterInfo;
    #endregion

    #region Core Functions
    void Start()
    {
    }
    void Update()
    {
    }
    #endregion

    #region Functions
    public void selectCharacter(character_stats character_stats)
    {
        characterInfo.i_name.text = character_stats.c_name + ", " + character_stats.c_class;
        characterInfo.description.text = character_stats.description;
        characterInfo.strength.text = character_stats.strength.ToString();
        characterInfo.dexterity.text = character_stats.dexterity.ToString();
        characterInfo.intelligence.text = character_stats.intelligence.ToString();
        characterInfo.i_class = character_stats.c_class;
    }
    #endregion
}
