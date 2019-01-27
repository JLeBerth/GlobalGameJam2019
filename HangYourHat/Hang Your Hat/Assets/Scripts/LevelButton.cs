using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public SpriteRenderer render;
    public int level;
    public TownGate tg;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        Debug.Log("Testing");
        render.color = Color.grey;
    }

    private void OnMouseExit()
    {
        render.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (CutsceneManager.currentLine == 12 && gameObject.tag == "Level1")
        {
            tg.ChangeScene("StartScreen");

        }
        else if (CutsceneManager.currentLine == 47 && gameObject.tag == "Level2")
        {
            Debug.Log("In Here");
            tg.ChangeScene("Level2");
        }
        else
        {
            tg.ChangeScene("Level1");
        }
    }
}
