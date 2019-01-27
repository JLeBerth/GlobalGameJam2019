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


	// Use this for initialization
	void Start ()
    {
        myFont = (Font)Resources.Load("Fonts/Saddlebag", typeof(Font));
        fontMaterial = (Material)Resources.Load("Materials/Text_Mat", typeof(Material));
        foreach(GameObject thisBG in backgrounds)
        {
            thisBG.SetActive(false);
        }
        currentLine = 0;
        currentDialogue = dialogue[currentLine];
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            NextLine();
        }

	}

    //switches to next line, or switches scenes
    public void NextLine()
    {
        currentLine++;
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
