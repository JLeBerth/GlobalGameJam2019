using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saving : MonoBehaviour {
    public bool hasSaved;

	// Use this for initialization
	void Start () {
        hasSaved = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SaveCharacter(Player toSave, CutsceneManager CM, SceneManagementScript SM, int characterSlot)
    {
        hasSaved = true;

        PlayerPrefs.SetInt("health_CharacterSlot" + characterSlot, toSave.currentHealth);
        PlayerPrefs.SetFloat("bulletsLeft_CharacterSlot" + characterSlot, toSave.bulletsTillReload);
        PlayerPrefs.SetFloat("xPos_CharacterSlot" + characterSlot, toSave.position.x);
        PlayerPrefs.SetFloat("yPos_CharacterSlot" + characterSlot, toSave.position.y);

        PlayerPrefs.SetInt("dialogue_CharacterSlot" + characterSlot, CM.currentLine);
        

        // Upgrade System/ New Weapons/ Level player is on  --->  FOR FUTURE

        PlayerPrefs.Save();
    }

    public void LoadCharacter(Player toLoad, CutsceneManager CM, SceneManagementScript SM, int characterSlot)
    {
        if (hasSaved)
        {
            toLoad.currentHealth = PlayerPrefs.GetInt("health_CharacterSlot" + characterSlot);
            toLoad.bulletsTillReload = PlayerPrefs.GetFloat("bulletsLeft_CharacterSlot" + characterSlot);
            toLoad.position.x = PlayerPrefs.GetFloat("xPos_CharacterSlot" + characterSlot);
            toLoad.position.y = PlayerPrefs.GetFloat("yPos_CharacterSlot" + characterSlot);

            CM.currentLine = PlayerPrefs.GetInt("dialogue_CharacterSlot" + characterSlot);

        }
    }


    public void SaveLine(int characterSlot, int line)
    {
        PlayerPrefs.SetInt("dialogue_CharacterSlot" + characterSlot, line);
    }

    public int ReturnLine(int characterSlot)
    {
        Debug.Log(PlayerPrefs.GetInt("dialogue_CharacterSlot" + characterSlot));
        return PlayerPrefs.GetInt("dialogue_CharacterSlot" + characterSlot);
    }
}
