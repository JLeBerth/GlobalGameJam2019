using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clickable : MonoBehaviour {

    public Button saloon;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        saloon.onClick.AddListener(Saloon);
    }

    public void Saloon()
    {
        Debug.Log("Clicked the saloon");
        
    }

    private void OnMouseOver()
    {
        Debug.Log("Testing");
    }

    public void ChangeTint()
    {

    }
    


}
