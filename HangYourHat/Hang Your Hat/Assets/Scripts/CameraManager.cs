using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float width;
    public float position;
    public GameObject player;
    public float speed;
    public Vector3 camSpeed;

    float maxPosition;
    float minPosition;
	// Use this for initialization
	void Start ()
    {
        camSpeed = new Vector3(speed, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        maxPosition = transform.position.x - position + width;
        minPosition = transform.position.x - position;

        if( player.transform.position.x > maxPosition )
        {
            transform.position += camSpeed;
        }
        if (player.transform.position.x < minPosition)
        {
            transform.position -= camSpeed;
        }
	}
}
