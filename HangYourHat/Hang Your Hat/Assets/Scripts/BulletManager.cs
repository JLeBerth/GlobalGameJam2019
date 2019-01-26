using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletManager : MonoBehaviour
{
    public static List<GameObject> bullets;
    public float speed;



    void Awake()
    {
    }

	// Use this for initialization
	void Start ()
    {
        bullets = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (bullets.Count > 0)
        {
            foreach (GameObject b in bullets)
            {
                b.transform.Translate(speed * b.transform.right * Time.deltaTime, Space.World);
                Debug.Log("Speed: " + speed * b.transform.right * Time.deltaTime);

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
