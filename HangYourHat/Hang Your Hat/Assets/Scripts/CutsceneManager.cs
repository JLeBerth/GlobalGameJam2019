using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public AudioClip[] soundEffects;
    public List<string> dialogue;
    private string currentDialogue;
    public static int currentLine = -1;
    public GameObject dialogueBox;
    public List<GameObject> backgrounds;
    private Font myFont;
    private Material fontMaterial;
    public Saving saver;
    bool inScene;
    bool dialogueActive;
    public GameObject Stetson;
    public GameObject Sherry;
    public GameObject Orca;
    public GameObject Gunther;
    public GameObject Sonny;
    public TownGate tg;


	// Use this for initialization
	void Start ()
    {
        Stetson.SetActive(false);
        Sherry.SetActive(false);
        Orca.SetActive(false);
        Gunther.SetActive(false);
        Sonny.SetActive(false);
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
        backgrounds[2].SetActive(true);
        currentDialogue = dialogue[0];
        Debug.Log(currentLine);
        if (currentLine != -1)
        {
            backgrounds[2].SetActive(false);
            dialogueBox.SetActive(true);
            inScene = true;
            UpdateFrame();
        }
              
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
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
        }
        

	}

    //Method that loads place in game when clicked
    public void StartGame()
    {
        Debug.Log("clicked");
        if (!inScene)
        {
            currentLine = 0;
            inScene = true;
            NextLine();
        }
    }
    //switches to next line, or switches scenes
    public void NextLine()
    {
        currentLine++;
        Debug.Log(currentLine);
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
                AudioSource.PlayClipAtPoint(soundEffects[0], Vector3.zero);
                inScene = true;
                dialogueActive = false;
                dialogueBox.SetActive(false);
                backgrounds[2].SetActive(false);
                break;
            case 2:
                backgrounds[0].SetActive(true);
                Stetson.SetActive(true);
                break;
            case 3:
                dialogueBox.SetActive(true);
                dialogueActive = true;
                break;
            case 5:
                AudioSource.PlayClipAtPoint(soundEffects[2], Vector3.zero);
                backgrounds[0].SetActive(false);
                backgrounds[1].SetActive(true);
                break;
            case 6:
                AudioSource.PlayClipAtPoint(soundEffects[2], Vector3.zero);
                AudioSource.PlayClipAtPoint(soundEffects[2], Vector3.zero);
                AudioSource.PlayClipAtPoint(soundEffects[2], Vector3.zero);
                break;
            case 11:
                currentLine++;
                //transition to town code
                tg.ChangeScene("Home");
                break;
            case 12:
                Stetson.SetActive(true);
                Orca.SetActive(true);
                backgrounds[6].SetActive(true);
                dialogueBox.SetActive(true);
                break;
            case 14:
                AudioSource.PlayClipAtPoint(soundEffects[3], Vector3.zero);
                AudioSource.PlayClipAtPoint(soundEffects[7], Vector3.zero);
                break;
            case 15:
                currentLine++;
                //transition to level1 code
                tg.ChangeScene("Level1");
                break;
            case 16:
                Stetson.SetActive(true);
                backgrounds[5].SetActive(true);
                dialogueActive = false;
                dialogueBox.SetActive(false);
                break;
            case 17:
                dialogueActive = true;
                dialogueBox.SetActive(true);
                break;
            case 25:
                AudioSource.PlayClipAtPoint(soundEffects[3], Vector3.zero);
                AudioSource.PlayClipAtPoint(soundEffects[7], Vector3.zero);
                AudioSource.PlayClipAtPoint(soundEffects[3], Vector3.zero);
                backgrounds[5].SetActive(false);
                backgrounds[4].SetActive(true);
                Sherry.SetActive(true);
                Orca.SetActive(true);
                dialogueActive = false;
                dialogueBox.SetActive(false);
                break;
            case 26:
                dialogueActive = true;
                dialogueBox.SetActive(true);
                Orca.SetActive(false);
                break;
            case 30:
                currentLine++;
                //transition to town code
                tg.ChangeScene("Home");
                break;
            case 31:
                backgrounds[3].SetActive(true);
                Stetson.SetActive(true);
                Sherry.SetActive(true);
                break;
            case 46:
                currentLine++;
                //transition to town code
                tg.ChangeScene("Home");
                break;
            case 47:
                backgrounds[7].SetActive(true);
                Stetson.SetActive(true);
                break;
            case 48:
                Gunther.SetActive(true);
                Sonny.SetActive(true);
                break;
            case 49:
                dialogueActive = false;
                dialogueBox.SetActive(false);
                break;
            case 50:
                dialogueActive = true;
                dialogueBox.SetActive(true);
                break;
            case 68:
                backgrounds[7].SetActive(false);
                backgrounds[1].SetActive(true);
                dialogueActive = false;
                dialogueBox.SetActive(false);
                break;
            case 69:
                dialogueActive = true;
                dialogueBox.SetActive(true);
                break;
            case 96:
                currentLine++;
                tg.ChangeScene("Home");
                break;
            case 97:
                backgrounds[1].SetActive(true);
                Stetson.SetActive(true);
                Sonny.SetActive(true);
                break;
            case 106:
                dialogueActive = false;
                dialogueBox.SetActive(false);
                Sonny.SetActive(false);
                break;
            case 107:
                dialogueActive = true;
                dialogueBox.SetActive(true);
                break;
            case 108:
                tg.ChangeScene("Home");
                break;


            default:
                break;
        }

        if (!dialogueActive)
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
        GUI.Box(new Rect(Screen.width  / 10f, Screen.height - (Screen.height /4), Screen.width - 2* (Screen.width / 13f), Screen.height), currentDialogue, textStyle);
    }
}
