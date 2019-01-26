using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public AudioClip[] soundEffects;
    public List<string> dialogue;
    public int currentLine;
    public GameObject dialogueBox;


	// Use this for initialization
	void Start ()
    {
        currentLine = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.A))
        {
            dialogueBox.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            dialogueBox.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
	}

    //switches to next line, or switches scenes
    public void NextLine()
    {
        currentLine++;
    }
}
