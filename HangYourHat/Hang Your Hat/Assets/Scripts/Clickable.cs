﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clickable : MonoBehaviour {

    public GameObject saloon;
    public GameObject shop;
    public GameObject exit;
    public GameObject lvl1;
    public GameObject lvl2;
    public GameObject saloon_bldg;
    public GameObject shop_bldg;

    // Bool to see if story is active so the exit button knows what to do
    public bool story;

    public GameObject bg;

    public Saving saver;
    public Player player;

    CutsceneManager csm;

    // Stores what line the dialogue is currently on
    public int line;

    public bool exiting;
    // Use this for initialization
    void Start()
    {
        shop.SetActive(false);
        lvl1.SetActive(false);
        lvl2.SetActive(false);
        saloon_bldg.SetActive(false);
        shop_bldg.SetActive(false);
        saloon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        line = saver.ReturnLine(1);
        // saloon.onClick.AddListener(Saloon);
        if (line >= 30)
        {
            saloon.SetActive(true);
        }

        if (line >= 50)
        {
            shop.SetActive(true);
        }
    }

    public void Saloon()
    {
        saloon_bldg.SetActive(true);
        story = true;

        
    }

    public void Shop()
    {
        shop_bldg.SetActive(true);
        story = true;
    }

    public void NextLevel()
    {
        exiting = true;
        //exit.SetActive(false);
        saloon.SetActive(false);

        lvl1.SetActive(true);

        if (line >= 46)
        {
            lvl2.SetActive(true);
        }

        if (line > 50)
        {
            shop.SetActive(true);

        }

        bg.SetActive(false);

        // if you can have the second level



    }

    public void Exit()
    {
        lvl1.SetActive(false);
        lvl2.SetActive(false);

        if (line >= 30)
        {

            saloon.SetActive(true);
        }

        if (line >= 50)
        {
            shop.SetActive(true);
        }
        bg.SetActive(true);
        shop_bldg.SetActive(false);
        saloon_bldg.SetActive(false);

        story = false;
        exiting = false;
    }




}
