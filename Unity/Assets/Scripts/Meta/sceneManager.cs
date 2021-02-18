using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager
{
    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
    public void godspeed(characterInfo characterInfo)
    {
        character_static.name = characterInfo.i_name.text;
        character_static.s_class = characterInfo.i_class;
        character_static.strength = float.Parse(characterInfo.strength.text);
        character_static.dexterity = float.Parse(characterInfo.dexterity.text);
        character_static.intelligence = float.Parse(characterInfo.intelligence.text);
        character_static.level = 1;
        character_static.experience_points = 0;
        SceneManager.LoadScene("test_level", LoadSceneMode.Single);
    }
}
