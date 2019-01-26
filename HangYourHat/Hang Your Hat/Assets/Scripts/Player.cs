using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myBody;              //players rigidBody
    public Collision2D botCollision;                  // Stores a vertical collision
    public Collision2D test = new Collision2D();
    public Vector2 position;                //the players current location
    public Vector3 velocity;                //the players velocity added to position
    public Vector3 acceleration;            //the players acceleration added to velocity
    public Vector2 mousePos;                //locates where the mouse is
    public Vector2 playerToMouse;           //draws a line between the player and the mouse position

    public float mass;                      //the mass of a player
    public float maxAcceleration;             //the maximum acceleration of a player
    public float maxVelocity;                 //the maximum speed of a player
    public float rollSpeed;                 // This is the speed of a player while rolling.  Higher than regular.
    public float normalSpeed;               // This is the normal speed
    public float fallSpeed;                 //rate of downards acceleration
    public float jumpSpeed;                 //jump height
    public float timer;
    public float distToGround;              //Distance from the center of the sprite to the ground


    public int baseHealth;                  //the total amount of health the player has
    public int currentHealth;               //the current remaining health the player has

    public double reloadTime;               //the amount of time it takes to reload the current gun
    public double timeReloading;            //the amount of time passed since reloading started
    public double bulletsTillReload;        //the number of times the player may shoot before reloading

    public bool falling;                    //boolean of whether the player is currently falling
    public bool rolling;                    // Boolean of whether the player is currently rolling

	// Use this for initialization
	void Start ()
    {
        position = transform.position;
        myBody = gameObject.GetComponent<Rigidbody2D>();
        distToGround = gameObject.GetComponent<SpriteRenderer>().bounds.extents.y;
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
            ApplyForce(new Vector2(maxVelocity, 0));

            if (Input.GetKey(KeyCode.Space) && timer > .2f)
            {
                rolling = true;
                timer = 0;
                
            }
        }

        // moving left
        if (Input.GetKey(KeyCode.A))
        {
            ApplyForce(new Vector2(-maxVelocity, 0));

            if (Input.GetKey(KeyCode.Space) && timer > .2f)
            {
                rolling = true;
                timer = 0;

            }
        }

        //jump
        if (Input.GetKey(KeyCode.W) && 
            !falling
            //IsGrounded()
            )
        {
            ApplyForce(new Vector2(0, jumpSpeed));
            falling = true;
        }
        
        if (IsGrounded())
        {
            falling = false;
        }

        // Falling
        if(falling)
        {
            ApplyForce(new Vector2(0, fallSpeed));
        }
        
        // Roll
        if (rolling)
        {
            maxVelocity = rollSpeed;
        }
        else
        {
            maxVelocity = normalSpeed;
        }

        // Timer for roll
        timer += .01f;
        if (rolling && timer >= .1f)
        {
            rolling = false;
        }

        // if rolling, then roll
        if (rolling)
        {
            Roll();
        }
        // Otherwise, move as normal
        else
        {
            Movement();
        }

        velocity.x = velocity.x / 2;



        // falling = true;
        //move player after calculations
        

    }

    /// <summary>
    /// Method to check if player is grounded??? Doesn't work, but it might
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector2.up, distToGround + .1f);
    }
    /// <summary>
    /// AppkyForce Method accelerates based on force
    /// </summary>
    /// <param name="force"></param>
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    /// <summary>
    /// Movement method calculates change in position based on factors
    /// </summary>
    public void Movement()
    {
        Vector3.ClampMagnitude(acceleration, maxAcceleration);
        velocity += acceleration;
        if(velocity.x > maxVelocity)
        {
            velocity.x = maxVelocity;
        }
        else if (velocity.x < - maxVelocity)
        {
            velocity.x = -maxVelocity;
        }
        myBody.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    /// <summary>
    /// This executes a roll instead of normal movement
    /// </summary>
    public void Roll()
    {
        if (velocity.x > 0)
        {
            velocity.x = rollSpeed;
        }

        else if (velocity.x < 0)
        {
            velocity.x = -rollSpeed;
        }

        myBody.MovePosition(transform.position + velocity * Time.deltaTime);
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
        playerToMouse.Normalize();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (falling)
        {
            falling = false;
            velocity.y = 0;
            botCollision = collision;
        }
    }
    

}
