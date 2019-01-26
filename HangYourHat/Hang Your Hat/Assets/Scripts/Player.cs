﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myBody;              //players rigidBody
    public BoxCollider2D box;               // Player's box collider
    public SpriteRenderer sprite;           // The player sprite
    public RaycastHit2D ground;
    public Collision2D botCollision;                  // Stores a vertical collision
    public Collision2D test = new Collision2D();
    public Vector2 position;                //the players current location
    public Vector2 tempPosition;            // Temporary position for hit detection
    public Vector3 velocity;                //the players velocity added to position
    public Vector3 acceleration;            //the players acceleration added to velocity
    public Vector2 mousePos;                //locates where the mouse is
    public Vector3 playerToMouse;           //draws a line between the player and the mouse position
    public GameObject bullet;               // The bullet

    public float mass;                      //the mass of a player
    public float maxAcceleration;             //the maximum acceleration of a player
    public float maxVelocity;                 //the maximum speed of a player
    public float rollSpeed;                 // This is the speed of a player while rolling.  Higher than regular.
    public float normalSpeed;               // This is the normal speed
    public float fallSpeed;                 //rate of downards acceleration
    public float jumpSpeed;                 //jump height
    public float timer;
    public float distToGround;              //Distance from the center of the sprite to the ground
    public float offset;                    // This is so the raycast never hits its own collider
    public float angle;
    public float bulletsTillReload;        //the number of times the player may shoot before reloading


    public int baseHealth;                  //the total amount of health the player has
    public int currentHealth;               //the current remaining health the player has

    public double reloadTime;               //the amount of time it takes to reload the current gun
    public double timeReloading;            //the amount of time passed since reloading started
    

    public bool falling;                    //boolean of whether the player is currently falling
    public bool rolling;                    // Boolean of whether the player is currently rolling

	// Use this for initialization
	void Start ()
    {
        position = transform.position;
        myBody = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        box = gameObject.GetComponent<BoxCollider2D>();
        // distToGround = sprite.bounds.extents.y;
        // offset = distToGround + .01f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        acceleration = Vector3.zero;


        GetAngle();

        angle = Mathf.Atan2(playerToMouse.y, playerToMouse.x);



        if (currentHealth <= 0)
        {
            PlayerDeath(); 
        }

        // Shooting
        if (Input.GetMouseButtonDown(0))
        {
            
            Debug.Log("FIRE!");
            //Make GameObject from Bullet prefab
            GameObject b = Instantiate(bullet, 
                transform.position, 
                Quaternion.identity);
            // Get angle of fire
            // Change bullet's transform.forward to the angle of fire
            // b.transform.up = playerToMouse;

            angle = Mathf.Atan2(playerToMouse.y, playerToMouse.x);
            
            b.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle);

            Debug.Log(Mathf.Rad2Deg * angle);


            // Add bullet to manager list
            BulletManager.bullets.Add(b);
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
            // falling = true;
        }

        // Checks if grounded
        ground = IsGrounded();
        
        // If the player is above a certain point, start fallign
        if (ground.distance >= .1f || ground.collider == null)
        {
            falling = true;
        }
        // Otherwise, the player is grounded, so stop falling and set
        // vertical velocity to zero
        else
        {
           falling = false;
            velocity.y = 0;
        }

        // Falling
        if(falling)
        {
            ApplyForce(new Vector2(0, fallSpeed));
        }
        
        // Roll
        if (rolling)
        {
            // Change max velocity so you move faster
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
    public RaycastHit2D IsGrounded()
    {

        LayerMask mask = LayerMask.GetMask("Terrain");
        tempPosition = transform.position;

        // tempPosition.y -= offset;
        // Debug.Log("ORIGIN: " + tempPosition);


        tempPosition.x -= sprite.bounds.extents.x - .5f;
        tempPosition.y -= sprite.bounds.extents.y;

        RaycastHit2D hit1 = Physics2D.Raycast(tempPosition,
            //transform.position - sprite.bounds.extents,
            //transform.position + new Vector3(0,-1,0),
            -Vector2.up,
            distToGround + 5f,
            mask);

        tempPosition = transform.position;

        tempPosition.x += sprite.bounds.extents.x - .85f;
        tempPosition.y -= sprite.bounds.extents.y;
        RaycastHit2D hit2 = Physics2D.Raycast(tempPosition,
            //transform.position + new Vector3(0,-1,0),
            -Vector2.up,
            distToGround + 5f,
            mask);
        
        Debug.DrawLine(tempPosition, transform.position + new Vector3 (0,-1,0) * 3);
        Debug.DrawLine(transform.position - sprite.bounds.extents, transform.position + new Vector3(0, -1, 0) * 3);

        // Sends a ray out on either side of the object.  Checks which one is closer to the ground
        // If they are both of equal(ish) distance, then you can fall off a platform
        if ((hit2.distance < hit1.distance && hit2.collider != null) || (hit1.collider == null && hit2.collider != null))
        {
            Debug.Log("return hit2 during check");
            return hit2;
        }
        else if ((hit1.distance < hit2.distance && hit1.collider != null)|| (hit2.collider == null && hit1.collider != null))
        {
            Debug.Log("return hit1 during check");
            return hit1;
        }
        

        // Debug.Log("return hit1 after check");
        return hit1;



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

        if (falling)
        {

            if (transform.position.y + velocity.y < ground.point.y + .1f)
            {
                myBody.MovePosition(new Vector2(transform.position.x, ground.point.y + .1f));
            }
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

    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (falling)
    //    {
    //        falling = false;
    //        velocity.y = 0;
    //        botCollision = collision;
    //    }
    //}
    

}
