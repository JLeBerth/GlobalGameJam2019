﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Animator animation;
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
    public Vector3 mousePos;                //locates where the mouse is
    public Vector3 playerToMouse;           //draws a line between the player and the mouse position
    public Vector3 scaleBy;
    public Vector3 normalScale;
    public GameObject bullet;               // The bullet
    public GameObject pivotPoint;           // The point at which the gun pivots in the hand
    public GameObject bulletSpawn;          // Point from the gun where the bullet spawns
    public TownGate tg;

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
    public float bulletsTillReload;         //the number of times the player may shoot before reloading
    public float maxBullets;                // the number of bullets the player can hold, max


    public int baseHealth;                  //the total amount of health the player has
    public int currentHealth;               //the current remaining health the player has
    public int gunUsage;
    public Dictionary<string, bool> guns;    //all the possible guns to have, and which you have
    public List<GameObject> gunsList;           // all possible guns

    public double reloadTime;               //the amount of time it takes to reload the current gun
    public double timeReloading;            //the amount of time passed since reloading started
    

    public bool falling;                    //boolean of whether the player is currently falling
    public bool rolling;                    // Boolean of whether the player is currently rolling
    public Vector3 rollDir;             // Direction in which you are rolling
    public int airRolls = 0;                    // Tracks how many times you've rolled in midair
    public int maxRolls = 1;                // Tracks how many rolls you can make
    public bool facingLeft;
    public bool reloading;

    public static int coal;                 //currency


    void Awake()
    {
        QualitySettings.vSyncCount = 1;
    }
	// Use this for initialization
	void Start ()
    {
        guns = new Dictionary<string, bool> { { "De Confluenza", true }, { "Pride and Accomp", true }, { "Hemway", true }, { "Golden Ratio", true } };

        position = transform.position;
        myBody = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        // box = gameObject.GetComponent<BoxCollider2D>();
        scaleBy = new Vector3(1, 1, 1);
        normalScale = transform.localScale;
        // distToGround = sprite.bounds.extents.y;
        // offset = distToGround + .01f;

        //rollVelocity = velocity;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Once you reach a certain point, you get the new guns
        if (CutsceneManager.currentLine > 50)
        {
            // Q changes guns
            if (Input.GetKeyDown(KeyCode.Q))
            {
                gunUsage++;
                if (gunUsage < guns.Count)
                {
                    Debug.Log("Change To: " + gunUsage);
                    ChangeGun(gunUsage);
                }
                else
                {
                    ChangeGun(0);
                }

            }
        }

        acceleration = Vector3.zero;


        GetAngle();

        angle = Mathf.Atan2(playerToMouse.y, playerToMouse.x);
        pivotPoint.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);



        if (currentHealth <= 0)
        {
            PlayerDeath(); 
        }

        // Shooting
        if (Input.GetMouseButtonDown(0))
        {
            if (!rolling)
            {
                if (gunUsage == 0 || gunUsage == 1 || gunUsage == 3)
                {
                    if (bulletsTillReload > 0 && !reloading)
                    {
                        bulletsTillReload--;
                        //Make GameObject from Bullet prefab
                        GameObject b = Instantiate(bullet,
                            bulletSpawn.transform.position,
                            Quaternion.identity);
                        // Get angle of fire
                        // Change bullet's transform.forward to the angle of fire
                        // b.transform.up = playerToMouse;

                        angle = Mathf.Atan2(playerToMouse.y, playerToMouse.x);

                        b.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle);


                        // Add bullet to manager list
                        BulletManager.bullets.Add(b);
                        BulletManager.bulletDic.Add(b, "Player");
                    }
                    else
                    {
                        if (!reloading)
                        {
                            Reload();
                        }
                    }

                }
                else if (gunUsage == 2)
                {
                    if (bulletsTillReload > 0 && !reloading)
                    {
                        bulletsTillReload -= 3;
                        //Make GameObject from Bullet prefab
                        GameObject b = Instantiate(bullet,
                            bulletSpawn.transform.position,
                            Quaternion.identity);
                        // Get angle of fire
                        // Change bullet's transform.forward to the angle of fire
                        // b.transform.up = playerToMouse;

                        angle = Mathf.Atan2(playerToMouse.y, playerToMouse.x);

                        angle = angle * Mathf.Rad2Deg;

                        b.transform.rotation = Quaternion.Euler(0, 0, angle);

                        GameObject b2 = Instantiate(bullet,
                            bulletSpawn.transform.position,
                            Quaternion.identity);

                        int change = Random.Range(5, 20);
                        angle += change;

                        b2.transform.rotation = Quaternion.Euler(0, 0, angle);

                        GameObject b3 = Instantiate(bullet,
                            bulletSpawn.transform.position,
                            Quaternion.identity);

                        angle -= change * 2;

                        b3.transform.rotation = Quaternion.Euler(0, 0, angle);


                        // Add bullet to manager list
                        BulletManager.bullets.Add(b);
                        BulletManager.bullets.Add(b2);
                        BulletManager.bullets.Add(b3);

                        BulletManager.bulletDic.Add(b, "Player");
                        BulletManager.bulletDic.Add(b2, "Player");
                        BulletManager.bulletDic.Add(b3, "Player");
                    }
                    else
                    {
                        if (!reloading)
                        {
                            Reload();
                        }
                    }
                }
            }
        }

        // Reloading
        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.R))
        {
            if (!reloading)
            {
                Reload();
            }
        }
        if (reloading)
        {
            timeReloading -= Time.deltaTime;

            if (timeReloading <= 0)
            {
                bulletsTillReload = maxBullets;
                reloading = false;
            }
            
        }

        // moving right
        if (Input.GetKey(KeyCode.D))
        {
            ApplyForce(new Vector2(maxVelocity, 0));
            facingLeft = false;
        }

        // moving left
        if (Input.GetKey(KeyCode.A))
        {
            ApplyForce(new Vector2(-maxVelocity, 0));
            facingLeft = true;
        }
        
        // Rolling
        if (Input.GetKey(KeyCode.LeftShift) 
            //&& timer > 1f
            && !rolling
            && airRolls < maxRolls)
        {
            rolling = true;
            // This changes the scale so the animation works
            Transform temp = GetComponentInChildren<Transform>();
            Vector3 tempScale = temp.localScale;
            transform.localScale = scaleBy;
            temp.localScale = tempScale;

            // Sets the timer to 0
            timer = 0;

            // Normal roll is in the direction you're currently moving
            // rollDir = velocity.normalized;

            // CHECKS FOR WHICH DIRECTION TO ROLL!!!

            rollDir = Vector3.zero;

            // Figures out which keys the user is pressing, and adds to the rollDir Vector as necessary
            if(Input.GetKey(KeyCode.W))
            {
                rollDir += Vector3.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                rollDir += Vector3.down;
            }
            if (Input.GetKey(KeyCode.A))
            {
                rollDir += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rollDir += Vector3.right;
            }

            // If the user pressed two keys, the magnitude will be greater than 1
            if (rollDir.sqrMagnitude > 1)
            {
                // Just normalize it to get the direction the user wants to go
                rollDir.Normalize();
            }



        }

        // ROll animation
        if (rolling)
        {
            //transform.localScale = new Vector3(1, 1, 1);
            animation.Play("Roll");
        }
        
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animation.Play("walk");
        }
        else
        {
            animation.Play("Idle");
        }


        if (facingLeft)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //jump
        if (Input.GetKey(KeyCode.Space) && 
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

            // Reset amount of rolls you can perform in midair
            airRolls = 0;
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
            if (falling)
            {
                airRolls++;
            }
        }
        else
        {
            
            maxVelocity = normalSpeed;
        }

        // Timer for roll animation/ Length
        timer += .01f;
        if (rolling && timer >= .2f)
        {
            rolling = false;
            transform.localScale = normalScale;
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

    public void Reload()
    {
        timeReloading = reloadTime;
        reloading = true;
    }

    public void ChangeGun(int gunKeyToChangeTo)
    {
        gunUsage = gunKeyToChangeTo;

        switch (gunUsage)
        {
            case 0: bulletsTillReload = 6;
                maxBullets = 6;

                for (int i = 0; i < gunsList.Count; i++)
                {
                    if (i == 0)
                    {
                        gunsList[i].SetActive(true);
                    }
                    else
                    {
                        gunsList[i].SetActive(false);
                    }
                }
                break;

            case 1:
                bulletsTillReload = 12;
                maxBullets = 12;

                for (int i = 0; i < gunsList.Count; i++)
                {
                    if (i == 1)
                    {
                        gunsList[i].SetActive(true);
                    }
                    else
                    {
                        gunsList[i].SetActive(false);
                    }
                }
                break;

            case 2:
                bulletsTillReload = 6;
                maxBullets = 6;

                for (int i = 0; i < gunsList.Count; i++)
                {
                    if (i == 2)
                    {
                        gunsList[i].SetActive(true);
                    }
                    else
                    {
                        gunsList[i].SetActive(false);
                    }
                }
                break;

            case 3:
                bulletsTillReload = 6;
                maxBullets = 6;

                for (int i = 0; i < gunsList.Count; i++)
                {
                    if (i == 3)
                    {
                        gunsList[i].SetActive(true);
                    }
                    else
                    {
                        gunsList[i].SetActive(false);
                    }
                }
                break;

        }

        Debug.Log("Gun Key: " + gunKeyToChangeTo);
    }

    /// <summary>
    /// Method to check if player is grounded??? Doesn't work, but it might
    /// </summary>
    /// <returns></returns>
    public RaycastHit2D IsGrounded()
    {

        LayerMask mask = LayerMask.GetMask("Collision");
        tempPosition = transform.position;

        

        // tempPosition.y -= offset;
        // Debug.Log("ORIGIN: " + tempPosition);


        tempPosition.x -= sprite.bounds.extents.x - .525f;
        tempPosition.y -= sprite.bounds.extents.y;

        if (facingLeft)
        {
            tempPosition.x += .28f;
        }

        RaycastHit2D hit1 = Physics2D.Raycast(tempPosition,
            //transform.position - sprite.bounds.extents,
            //transform.position + new Vector3(0,-1,0),
            -Vector2.up,
            distToGround + 5f,
            mask);



        Debug.DrawLine(tempPosition, transform.position + new Vector3(0, -1, 0) * 3);

        tempPosition = transform.position;

        tempPosition.x += sprite.bounds.extents.x - .825f;
        tempPosition.y -= sprite.bounds.extents.y;


        if (facingLeft)
        {
            tempPosition.x += .28f;
        }


        RaycastHit2D hit2 = Physics2D.Raycast(tempPosition,
            //transform.position + new Vector3(0,-1,0),
            -Vector2.up,
            distToGround + 5f,
            mask);
        
        Debug.DrawLine(tempPosition, transform.position + new Vector3 (0,-1,0) * 3);
        // Debug.DrawLine(transform.position - sprite.bounds.extents, transform.position + new Vector3(0, -1, 0) * 3);

        // Sends a ray out on either side of the object.  Checks which one is closer to the ground
        // If they are both of equal(ish) distance, then you can fall off a platform
        if ((hit2.distance < hit1.distance && hit2.collider != null) || (hit1.collider == null && hit2.collider != null))
        {
            //Debug.Log("return hit2 during check");
            return hit2;
        }
        else if ((hit1.distance < hit2.distance && hit1.collider != null)|| (hit2.collider == null && hit1.collider != null))
        {
            //Debug.Log("return hit1 during check");
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
                myBody.MovePosition(new Vector2(transform.position.x, ground.point.y + .1f) 
                    // * Time.deltaTime
                    );
            }
        }
        myBody.MovePosition(transform.position + velocity 
            * Time.deltaTime
            );
        
    }

    /// <summary>
    /// This executes a roll instead of normal movement
    /// </summary>
    public void Roll()
    {
        // if (!facingLeft)
        // {
        //     velocity.x = rollSpeed;
        // }
        // 
        // else if (facingLeft)
        // {
        //     velocity.x = -rollSpeed;
        // }
        // if (falling)
        // {
        //     velocity.y = 0.8f * jumpSpeed;
        // }

        velocity = rollSpeed * rollDir; 

        myBody.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    public void Roll(Vector3 rollVelocity)
    {
        if (!facingLeft)
        {
            rollVelocity.x = rollSpeed;
        }

        else if (facingLeft)
        {
            rollVelocity.x = -rollSpeed;
        }
        if (falling)
        {
            rollVelocity.y = 0.8f * jumpSpeed;
        }

        myBody.MovePosition(transform.position + rollVelocity * Time.deltaTime);
    }


    /// <summary>
    /// Player Death Method called when the player reaches zero or less health, inflicts the death penalty.
    /// </summary>
    public void PlayerDeath()
    {
        Scene curScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curScene.name);
    }

    /// <summary>
    /// Causes the player to take damage
    /// </summary>
    public void TakeDamage()
    {
        currentHealth--;
       
        if (currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    /// <summary>
    /// Calculates the vector between the player's position and the mouse position
    /// Normalizes it and returns it as a direction vector
    /// </summary>
    public void GetAngle()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerToMouse = mousePos - pivotPoint.transform.position;
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
