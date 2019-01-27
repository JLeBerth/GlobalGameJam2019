﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownGate : MonoBehaviour {
    public Player player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E) && Mathf.Abs(player.transform.position.x - this.transform.position.x) < 2)
        {
            if (this.tag == "Miabode")
            {
                ChangeScene("Home");
            }
            else if (this.tag == "Level1")
            {
                ChangeScene("Level1");
            }
            else if (this.tag == "Town1")
            {
                ChangeScene("Town1");
            }
            else if (this.tag == "Level2")
            {
                ChangeScene("Level2");
            }
            else if (this.tag == "GhostTown")
            {
                ChangeScene("GhostTown");

            }
        }
	}

    public void ChangeScene(string sceneTag)
    {
        SceneManager.LoadScene(sceneTag);
    }
}
