using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public AudioClip[] soundEffects;
    public List<string> dialogue;
    private string currentDialogue;
    public int currentLine;
    public GameObject dialogueBox;
    public List<GameObject> backgrounds;
    private Font myFont;
    private Material fontMaterial;
    public Saving saver;


	// Use this for initialization
	void Start ()
    {
        dialogueBox.SetActive(false);
        dialogue.Add("First Line!");

        myFont = (Font)Resources.Load("Fonts/Saddlebag", typeof(Font));
        fontMaterial = (Material)Resources.Load("Materials/Text_Mat", typeof(Material));
        foreach(GameObject thisBG in backgrounds)
        {
            thisBG.SetActive(false);
        }
        currentLine = 0;

        if(currentLine == 0)
        {
            backgrounds[2].SetActive(true);
        }
        currentDialogue = dialogue[currentLine];
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            //if (currentLine != 0)
            //{
                NextLine();
            //}
        }

        if (Input.GetKeyDown(KeyCode.S)) 
        {
            saver.SaveLine(1, currentLine);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            currentLine = saver.ReturnLine(1);
            currentDialogue = dialogue[currentLine];

            switch (currentLine)
            {
                case 1:
                    backgrounds[0].SetActive(true);
                    break;
                case 2:
                    backgrounds[0].SetActive(false);
                    backgrounds[1].SetActive(true);
                    break;
                default:
                    break;
            }
        }

	}

    //switches to next line, or switches scenes
    public void NextLine()
    {
        currentLine++;
        UpdateFrame();

        
    }

    /// <summary>
    /// Changes the current display to match the current lines frame
    /// </summary>
    public void UpdateFrame()
    {
        currentDialogue = dialogue[currentLine];

        switch (currentLine)
        {
            case 1:
                backgrounds[0].SetActive(true);
                break;
            case 2:
                backgrounds[0].SetActive(false);
                backgrounds[1].SetActive(true);
                break;
            default:
                break;
        }
    }
    private void OnGUI()
    {
        GUIStyle textStyle = new GUIStyle();
        textStyle.font = myFont;
        textStyle.font.material = fontMaterial;
        textStyle.fontSize = 30;
        GUI.backgroundColor = Color.clear;
        GUI.Box(new Rect(105, 380, Screen.width, Screen.height), currentDialogue, textStyle);
    }
}
