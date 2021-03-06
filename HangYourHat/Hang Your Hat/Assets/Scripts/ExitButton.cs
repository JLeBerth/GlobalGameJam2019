﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour {

    public SpriteRenderer render;
    public TownGate tg;
    // This is the button manager.  Ignore the stupid script name
    public Clickable manager;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

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
        // tg.ChangeScene("StartScreen");
        
        Debug.Log("Clicked");
        if (manager.exiting || manager.story)
        {
            manager.Exit();
        }
        else
        {
            manager.NextLevel();
        }
        
    }
}
