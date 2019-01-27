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
    }

    // Update is called once per frame
    void Update()
    {
        // saloon.onClick.AddListener(Saloon);
    }

    public void Saloon()
    {
        Debug.Log("Clicked the saloon");
        
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

        exiting = false;
    }




}
