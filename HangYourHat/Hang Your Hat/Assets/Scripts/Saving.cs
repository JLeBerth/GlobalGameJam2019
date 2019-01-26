using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saving : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    static void SaveCharacter(Player toSave, CutsceneManager CM, SceneManager SM, int characterSlot)
    {
        PlayerPrefs.SetInt("health_CharacterSlot" + characterSlot, toSave.currentHealth);
        PlayerPrefs.SetFloat("bulletsLeft_CharacterSlot" + characterSlot, toSave.bulletsTillReload);
        PlayerPrefs.SetFloat("xPos_CharacterSlot" + characterSlot, toSave.position.x);
        PlayerPrefs.SetFloat("yPos_CharacterSlot" + characterSlot, toSave.position.y);

        PlayerPrefs.SetInt("dialogue_CharacterSlot" + characterSlot, CM.currentLine);

        PlayerPrefs.SetInt("enemiesLeft_CharacterSlot" + characterSlot, SM.enemies.Count);

        // Upgrade System/ New Weapons/ Level player is on  --->  FOR FUTURE

        PlayerPrefs.Save();
    }

    static void LoadCharacter(Player toLoad, CutsceneManager CM, SceneManager SM, int characterSlot)
    {
        toLoad.currentHealth = PlayerPrefs.GetInt("health_CharacterSlot" + characterSlot);
        toLoad.bulletsTillReload = PlayerPrefs.GetFloat("bulletsLeft_CharacterSlot" + characterSlot);
        toLoad.position.x = PlayerPrefs.GetFloat("xPos_CharacterSlot" + characterSlot);
        toLoad.position.y = PlayerPrefs.GetFloat("yPos_CharacterSlot" + characterSlot);

        CM.currentLine = PlayerPrefs.GetInt("dialogue_CharacterSlot" + characterSlot);

        SM.enemiesLeft = PlayerPrefs.GetInt("enemiesLeft_CharacterSlot" + characterSlot);
    }
}
