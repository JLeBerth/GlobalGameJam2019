using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour {

    public SpriteRenderer render;

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
        Debug.Log("Clicked");
        manager.Shop();
    }
}
