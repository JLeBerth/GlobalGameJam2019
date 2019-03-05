using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownGate : MonoBehaviour {
    public Player player;

	// Use this for initialization
	void Start ()
    {
        CutsceneManager.currentLine = 60;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E) && Mathf.Abs(player.transform.position.x - this.transform.position.x) < 2)
        {
            if (this.tag == "Miabode")
            {
                if (CutsceneManager.currentLine == 31)
                {
                    ChangeScene("StartScreen");
                }
                else
                {
                    ChangeScene("Home");

                }
            }
            else if (this.tag == "Level1")
            {
                ChangeScene("Level1");
            }
            else if (this.tag == "Town1")
            {
                if (CutsceneManager.currentLine == 16)
                {

                    ChangeScene("StartScreen");
                }
                else
                {
                    ChangeScene("Home");
                }
            }
            else if (this.tag == "Level2")
            {
                ChangeScene("Level2");
            }
            else if (this.tag == "GhostTown")
            {
                if (CutsceneManager.currentLine == 46)
                {
                    ChangeScene("StartScreen");
                }
                else
                {
                    ChangeScene("Home");
                }
            }
            else if (this.tag == "Cutscene") 
            {
                ChangeScene("StartScreen");
            }
        }
	}

    public void ChangeScene(string sceneTag)
    {
        SceneManager.LoadScene(sceneTag);
    }
}
