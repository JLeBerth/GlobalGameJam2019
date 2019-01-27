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
    bool inScene;
    bool dialogueActive;


	// Use this for initialization
	void Start ()
    {
        dialogueActive = true;
        inScene = false;
        dialogueBox.SetActive(false);
        dialogue.Add("First Line!");

        myFont = (Font)Resources.Load("Fonts/Saddlebag", typeof(Font));
        fontMaterial = (Material)Resources.Load("Materials/Text_Mat", typeof(Material));
        foreach(GameObject thisBG in backgrounds)
        {
            thisBG.SetActive(false);
        }
        currentLine = 0;
        backgrounds[2].SetActive(true);
        currentDialogue = dialogue[currentLine];
        NextLine();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(inScene)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                NextLine();
            }
        }
        

	}

    //Method that loads place in game when clicked
    public void StartGame()
    {
        Debug.Log("clicked");
        if (!inScene)
        {
            currentLine = saver.ReturnLine(1);
            inScene = true;
            if (currentLine == 0)
            {
                NextLine();
            }
            else
            {
                UpdateFrame();
            }
        }
    }
    //switches to next line, or switches scenes
    public void NextLine()
    {
        currentLine++;
        saver.SaveLine(1, currentLine);
        UpdateFrame();

        
    }

    /// <summary>
    /// Changes the current display to match the current lines frame
    /// </summary>
    public void UpdateFrame()
    {
        switch (currentLine)
        {
            case 1:
                inScene = true;
                dialogueBox.SetActive(true);
                backgrounds[0].SetActive(true);
                backgrounds[2].SetActive(false);
                break;
            case 2:
                dialogueBox.SetActive(false);
                dialogueActive = false;
                backgrounds[0].SetActive(false);
                backgrounds[1].SetActive(true);
                break;
            default:
                break;
        }

        if (dialogueActive)
        {
            currentDialogue = " ";
        }
        else
        {
            currentDialogue = dialogue[currentLine];
        }
    }
    private void OnGUI()
    {
        GUIStyle textStyle = new GUIStyle();
        textStyle.font = myFont;
        textStyle.wordWrap = true;
        textStyle.font.material = fontMaterial;
        textStyle.fontSize = 30;
        GUI.backgroundColor = Color.clear;
        GUI.Box(new Rect(105, 380, Screen.width - 200, Screen.height), currentDialogue, textStyle);
    }
}
