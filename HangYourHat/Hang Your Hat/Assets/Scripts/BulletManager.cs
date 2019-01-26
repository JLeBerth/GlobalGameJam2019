﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletManager : MonoBehaviour
{
    private Camera cam;
    public float width;
    public float height;
    public static List<GameObject> bullets;
    public float speed;
    int numOfBullets;



    void Awake()
    {
    }

	// Use this for initialization
	void Start ()
    {
        bullets = new List<GameObject>();
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (bullets.Count > 0)
        {
            for  (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].transform.Translate(speed * bullets[i].transform.right * Time.deltaTime, Space.World);
                Debug.Log("Speed: " + speed * bullets[i].transform.right * Time.deltaTime);

                if (bullets[i].transform.position.x < -width || 
                    bullets[i].transform.position.x > width ||
                    bullets[i].transform.position.y < -height ||
                    bullets[i].transform.position.y > height)
                {
                    GameObject tempBullet = bullets[i];
                    bullets.Remove(bullets[i]);
                    Destroy(tempBullet);
                }
                /*ApplyForce(transform.right);

                velocity += acceleration;

                Vector3.ClampMagnitude(velocity, speed);

                position += velocity;

                acceleration = Vector3.zero;

                b.transform.position = position;*/
            }
        }
    }

}