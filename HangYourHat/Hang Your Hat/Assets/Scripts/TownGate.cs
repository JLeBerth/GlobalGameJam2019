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
                SceneManager.LoadScene("Home");
            }
            else if (this.tag == "Level1")
            {
                SceneManager.LoadScene("Level1");
            }
            else if (this.tag == "Town1")
            {
                SceneManager.LoadScene("Town1");
            }
            else if (this.tag == "Level2")
            {
                SceneManager.LoadScene("Level2");
            }
            else if (this.tag == "GhostTown")
            {
                SceneManager.LoadScene("GhostTown");

            }
        }
	}
}
