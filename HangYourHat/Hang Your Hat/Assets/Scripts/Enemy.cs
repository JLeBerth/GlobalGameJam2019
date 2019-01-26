﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // Vectors
    public Vector3 enemyPosition;        // Holds the position of the enemy
    public Vector3 velocity;        
    public Vector3 acceleration;
    public Vector3 direction;

    // Objects
    private Player playerScript;    // Holds the player script for reference
    public GameObject playerObject; // Holds the player object
    public Rigidbody2D enemyRigid;  // Holds the enemy rigidBody
    public GameObject pivotPoint;   // Holds the pivotPoint for the gun

    // Floats
    private float playerDistance;   // Distance of the player from the enemy (DISTANCES MEASURED IN DISTANCE SQUARED)
    public float wanderLimit;       // The distance at which the enemy starts wandering
    public float shootLimit;        // The distance at which the enemy starts shooting at the player

    public float enemySpeed;        // Max speed that the enemy moves
    public float mass;

    /*public float furthestLeft;      // If on platform, the enemy will only walk so far left or right to stay on the platform. If not, will default to 8
    public float furthestRight;*/

    // Bools
    public bool bWander;             // Should the enemy wander? (Based on player distance?)
    public bool bShoot;              // Should the enemy shoot at the player?
    public bool moveRight;
    public bool moveLeft;

    //public bool onPlatform;          // Tells if the enemy is on a platform or not

    // Ints
    public int health;


	// Use this for initialization
	void Start () {
        enemyPosition = this.transform.position;

        //playerScript = playerObject.GetComponent<Player>();

        enemyRigid = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        // Move the enemy
        velocity += acceleration * Time.deltaTime;

        velocity = Vector3.ClampMagnitude(velocity, enemySpeed);

        enemyPosition += velocity * Time.deltaTime;

        direction = velocity.normalized;

        acceleration = Vector3.zero; // zero out acceleration so each frame the forces are taken into effect

        enemyRigid.position = enemyPosition;

        // Check player distance and associated fxns
        playerDistance = GetDistanceSqrd(playerObject);
        StartCoroutine(Wander());
        Shoot();
	}

    /// <summary>
    /// Applies a force to the enemy
    /// </summary>
    /// <param name="force">The force applied</param>
    public void ApplyForce(Vector3 _force)
    {
        acceleration += _force / mass;
    }

    /// <summary>
    /// Checks if the player is close enough, and shoots at them if they are
    /// </summary>
    public void Shoot ()
    {
        // Check if the player is close enough to begin shooting
        if (playerDistance < shootLimit)
        {
            bShoot = true;
        }
        else
        {
            bShoot = false;
        }

        if (bShoot) // If the player is close enough, do below code
        {
            Vector2 toPlayer = playerObject.transform.position - pivotPoint.transform.position;

            float angleRad;

            angleRad = Mathf.Atan2(toPlayer.y, toPlayer.x);

            angleRad *= Mathf.Rad2Deg;
            angleRad += 180f;

            pivotPoint.transform.rotation = Quaternion.Euler (0, 0, angleRad);

            //Debug.log("SHOOOOT");
        }
    }

    /// <summary>
    /// Checks if the player is close enough, and wanders if they are
    /// </summary>
    IEnumerator Wander()
    {
        // Check if the player is close enough to begin wandering
        if (playerDistance < wanderLimit)
        {
            bWander = true;
        }
        else
        {
            bWander = false;
        }

        if (bWander) // If the player is close enough, do below code
        {
            for (int i = 0; i < 5; i++)
            {
                enemyPosition.x++;
                Debug.Log("right");
            }

            yield return 0;

            for (int i = 0; i < 5; i++)
            {
                enemyPosition.x--;
                Debug.Log("left");
            }
        }
    }

    /// <summary>
    /// Gets the distance between this object and the given object
    /// </summary>
    /// <param name="_object">The object to check the distance from</param>
    /// <returns>The distance-squared of the given object</returns>
    public float GetDistanceSqrd(GameObject _object)
    {
        Vector2 distVector = transform.position - _object.transform.position;

        float distSqrd = Mathf.Pow(distVector.x, 2) + Mathf.Pow(distVector.y, 2);

        return distSqrd;
    }
}
