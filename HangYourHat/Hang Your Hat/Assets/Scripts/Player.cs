using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Vector3 position;              //the players current location
    public Vector3 velocity;                //the players velocity added to position
    public Vector3 acceleration;            //the players acceleration added to velocity
    public Vector3 mousePos;                //locates where the mouse is
    public Vector3 playerToMouse;           //draws a line between the player and the mouse position

    public float mass;                      //the mass of a player
    public float maxAcceleration;             //the maximum acceleration of a player
    public float maxVelocity;                 //the maximum speed of a player


    public int baseHealth;                  //the total amount of health the player has
    public int currentHealth;               //the current remaining health the player has

    public double reloadTime;               //the amount of time it takes to reload the current gun
    public double timeReloading;            //the amount of time passed since reloading started
    public double bulletsTillReload;        //the number of times the player may shoot before reloading

	// Use this for initialization
	void Start ()
    {
        position = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        acceleration = Vector3.zero;

        GetAngle();

		if (currentHealth <= 0)
        {
            PlayerDeath(); 
        }

        // moving right
        if (Input.GetKey(KeyCode.D))
        {
            ApplyForce(new Vector3(maxVelocity, 0, 0));
        }

        // moving left
        if (Input.GetKey(KeyCode.A))
        {
            ApplyForce(new Vector3(-1 * maxVelocity, 0, 0));
        }

        Movement();
        velocity = velocity / 2;

        

        //move player after calculations
        

    }

    /// <summary>
    /// Method applies a force upon the player to allow acceleration
    /// </summary>
    public void ApplyForce(Vector3 appliedForce)
    {
        acceleration += appliedForce / mass;
    }

    /// <summary>
    /// Movement method calculates change in position based on factors
    /// </summary>
    public void Movement()
    {
        Vector3.ClampMagnitude(acceleration, maxAcceleration);
        velocity += acceleration;
        Vector3.ClampMagnitude(velocity, maxVelocity);
        position += velocity;
        transform.position = position;
    }


    /// <summary>
    /// Player Death Method called when the player reaches zero or less health, inflicts the death penalty.
    /// </summary>
    public void PlayerDeath()
    {

    }

    /// <summary>
    /// Calculates the vector between the player's position and the mouse position
    /// Normalizes it and returns it as a direction vector
    /// </summary>
    public void GetAngle()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerToMouse = mousePos - position;
        playerToMouse.z = 0;
        playerToMouse.Normalize();
    }
}
