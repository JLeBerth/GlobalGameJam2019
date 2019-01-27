using System.Collections;
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

    // Bool to see if story is active so the exit button knows what to do
    public bool story;

    public GameObject bg;

    public Saving saver;
    public Player player;

    public bool exiting;
    // Use this for initialization
    void Start()
    {
        shop.SetActive(false);
        lvl1.SetActive(false);
        lvl2.SetActive(false);
        saloon_bldg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // saloon.onClick.AddListener(Saloon);
    }

    public void Saloon()
    {
        saloon_bldg.SetActive(true);
        story = true;

        
    }

    public void NextLevel()
    {
        exiting = true;
        //exit.SetActive(false);
        saloon.SetActive(false);

        lvl1.SetActive(true);

        bg.SetActive(false);

        // if you can have the second level



    }

    public void Exit()
    {
        lvl1.SetActive(false);
        lvl2.SetActive(false);

        saloon.SetActive(true);
        bg.SetActive(true);
        saloon_bldg.SetActive(false);

        story = false;
        exiting = false;
    }




}
