using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clickable : MonoBehaviour {

    public GameObject saloon;
    public GameObject shop;
    public GameObject exit;

    // Use this for initialization
    void Start()
    {
        shop.SetActive(false);
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
        //exit.SetActive(false);
        saloon.SetActive(false);


    }
    
    


}
