using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public Animator animator;
    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            if (player.GetComponent<Player>().gunUsage == 0)
            {

                animator.Play("fire");

            }
            else if (player.GetComponent<Player>().gunUsage == 1)
            {
                animator.Play("pridAccomp");
            }
            else if (player.GetComponent<Player>().gunUsage == 2)
            {
                animator.Play("hemingway");
            }
            else if (player.GetComponent<Player>().gunUsage == 3)
            {
                animator.Play("goldRat");
            }
        }
	}
}
