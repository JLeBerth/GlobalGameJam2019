using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saving : MonoBehaviour {
    public GameObject characterData;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveCharacter(characterData.GetComponent<Player>(), 1);//characterData, 0);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {

        }
	}

    static void SaveCharacter(Player toSave, int characterSlot)
    {
        PlayerPrefs.SetInt("health_CharacterSlot" + characterSlot, toSave.currentHealth)
    }

    void SetSaveChar(GameObject _character)
    {
        characterData = _character;
    }
}
