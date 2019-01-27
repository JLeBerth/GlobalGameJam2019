using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startButton : MonoBehaviour
{
    public GameObject csm;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnMouseEnter()
    {
        Debug.Log("Hover");
    }
    private void OnMouseDown()
    {
        Debug.Log("clicked");
        csm.GetComponent<CutsceneManager>().StartGame();
    }
}
