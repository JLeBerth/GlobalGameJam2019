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
    public float levelSize;
    public float camWidth;
    public Vector3 startPos;

    float maxPosition;
    float minPosition;
	// Use this for initialization
	void Start ()
    {
        camSpeed = new Vector3(speed, 0, 0);
        camWidth = 2f * Camera.main.orthographicSize * Camera.main.aspect;
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        maxPosition = transform.position.x - position + width;
        minPosition = transform.position.x - position;

        if( player.transform.position.x > maxPosition 
            && transform.position.x < levelSize - camWidth)
        {
            transform.position += camSpeed;
        }
        if (player.transform.position.x < minPosition
            && transform.position.x > startPos.x)
        {
            transform.position -= camSpeed;
        }
	}
}
