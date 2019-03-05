using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // Vectors
    public Vector3 enemyPosition;        // Holds the position of the enemy
    public Vector3 velocity;        
    public Vector3 acceleration;
    public Vector3 direction;
    public Vector3 startPos;
    public Vector2 tempPosition;         // temporary position for raycasting the ground

    // Objects
    private Player playerScript;    // Holds the player script for reference
    public GameObject playerObject; // Holds the player object
    public Rigidbody2D enemyRigid;  // Holds the enemy rigidBody
    public SpriteRenderer sprite;   // Holds the enemy sprite
    public RaycastHit2D ground;
    public GameObject pivotPoint;   // Holds the pivotPoint for the gun
    public GameObject bulletSpawn;  // Where the bullet spawns
    public GameObject gun;            // the gun

    // Floats
    public float playerDistance;   // Distance of the player from the enemy (DISTANCES MEASURED IN DISTANCE SQUARED)
    public float wanderLimit;       // The distance at which the enemy starts wandering
    public float shootLimit;        // The distance at which the enemy starts shooting at the player

    public float enemySpeed;        // Max speed that the enemy moves
    public float mass;
    public float fallSpeed = -1.2f;

    public float maxBullets;
    public float currentBullets;
    public float shootTimer;
    public float currentShootTimer;
    public float reloadTimer;
    public float currentReloadTimer;

    /*public float furthestLeft;      // If on platform, the enemy will only walk so far left or right to stay on the platform. If not, will default to 8
    public float furthestRight;*/

    // Bools
    public bool bWander;             // Should the enemy wander? (Based on player distance?)
    public bool bShoot;              // Should the enemy shoot at the player?
    private bool moveRight;
    private bool moveLeft;
    public bool falling;

    public bool backToStart;
    public bool alive;

    public bool reload;

    //public bool onPlatform;          // Tells if the enemy is on a platform or not

    // Ints
    public int health;


	// Use this for initialization
	void Start () {
        alive = true;

        enemyPosition = this.transform.position;

        //playerScript = playerObject.GetComponent<Player>();

        enemyRigid = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        startPos = this.transform.position;

        currentShootTimer = 0;
        currentReloadTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        // Move the enemy
        /*velocity += acceleration * Time.deltaTime;

        velocity = Vector3.ClampMagnitude(velocity, enemySpeed);

        enemyPosition += velocity * Time.deltaTime;

        direction = velocity.normalized;

        acceleration = Vector3.zero; // zero out acceleration so each frame the forces are taken into effect

        enemyRigid.position = enemyPosition;*/

        tempPosition = transform.position;
        tempPosition.y -= sprite.bounds.extents.y;

        ground = Physics2D.Raycast(tempPosition, -Vector2.up);

        velocity += acceleration;
        if (velocity.x > enemySpeed)
        {
            velocity.x = enemySpeed;
        }
        else if (velocity.x < -enemySpeed)
        {
            velocity.x = -enemySpeed;
        }

        if (falling)
        {
            if (transform.position.y + velocity.y < ground.point.y + .1f)
            {
                enemyRigid.MovePosition(new Vector2(transform.position.x, ground.point.y + .1f));
            }
        }

        enemyRigid.MovePosition(transform.position + velocity * Time.deltaTime);

        // Check player distance and associated fxns
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
    public void Shoot (GameObject _shootAt, GameObject _bullet)
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
            // Gun look at player
            Vector2 toPlayer = _shootAt.transform.position - pivotPoint.transform.position;

            float angleRad;

            angleRad = Mathf.Atan2(toPlayer.y, toPlayer.x);

            angleRad *= Mathf.Rad2Deg;
            angleRad += 180f;

            pivotPoint.transform.rotation = Quaternion.Euler (0, 0, angleRad);

            // Shoot at player:
            if (currentShootTimer <= 0 && currentReloadTimer <= 0)
            {
                gun.GetComponent<EnemyGun>().Animate(); 
                GameObject b = Instantiate(_bullet,
                bulletSpawn.transform.position,
                Quaternion.identity);

                b.transform.rotation = Quaternion.Euler(0, 0, angleRad + 180);

                BulletManager.bullets.Add(b);
                BulletManager.bulletDic.Add(b, "Enemy");

                currentBullets -= 1;

                currentShootTimer = shootTimer;

                if (currentBullets <= 0)
                {
                    currentReloadTimer = reloadTimer;
                }
            }
            else if (currentReloadTimer > 0)
            {
                currentReloadTimer -= Time.deltaTime;

                currentBullets = 6;
            }
            else if (currentShootTimer > 0)
            {
                currentShootTimer -= Time.deltaTime;
            }

            //Debug.log("SHOOOOT");
        }
    }

    /// <summary>
    /// Checks if the player is close enough, and wanders if they are.
    /// Uses raycasting to detect whether there is a wall in front of them or the gorund ends, in which case they 
    /// will turn around.
    /// </summary>
    public void Wander()
    {
        // Check if the player is close enough to begin wandering
        if (playerDistance < wanderLimit)
        {
            bWander = true;
        }
        else
        {
            bWander = true;
        }

        if (bWander && !bShoot) // If the player is close enough, do below code
        {
            LayerMask mask = LayerMask.GetMask("Collision");

            // Left Ray
            tempPosition = transform.position;

            tempPosition.x -= sprite.bounds.extents.x - .5f;
            //tempPosition.y -= sprite.bounds.extents.y;

            Debug.DrawLine(tempPosition, tempPosition + new Vector2(0, -1) * sprite.bounds.extents.y, Color.black);

            RaycastHit2D hit1 = Physics2D.Raycast(tempPosition, -Vector2.up, sprite.bounds.extents.y); // starting from tempPosition, cast a ray down


            // Right Ray
            tempPosition = transform.position;

            tempPosition.x += sprite.bounds.extents.x - .5f;
            //tempPosition.y -= sprite.bounds.extents.y;

            Debug.DrawLine(tempPosition, tempPosition + new Vector2(0, -1) * sprite.bounds.extents.y, Color.blue);

            RaycastHit2D hit2 = Physics2D.Raycast(tempPosition, -Vector2.up, sprite.bounds.extents.y); // start from tempPosition, cast ray down


            // Conditionals
            if (hit1.collider == null && hit2.collider == null) // check if enemy should fall
            {
                Debug.Log("FAAAAALL");
                ApplyForce(new Vector2(0f, fallSpeed));
            }

            if (backToStart) // left
            {
                ApplyForce(new Vector3(-enemySpeed, 0f, 0f)); // Move the enemy left

                // Check the down raycast to see if there is no longer a floor beneath the enemy on the left side
                if ((hit2.distance < hit1.distance && hit2.collider != null) || (hit1.collider == null && hit2.collider != null))
                {
                    backToStart = false; // if so, turn around
                }

                // Read for wall to the left
                tempPosition.x = transform.position.x - (sprite.bounds.extents.x - .7f); // reset temp pos

                RaycastHit2D forward = Physics2D.Raycast(tempPosition, Vector2.left, .5f); // cast ray left

                Debug.DrawLine(tempPosition, tempPosition + Vector2.left * 1f, Color.black);

                if (forward.collider != null) // if the ray hits something, turn around
                {
                    velocity = Vector2.zero;
                    backToStart = false;
                }
            }
            else if (!backToStart) // right
            {
                ApplyForce(new Vector3(enemySpeed, 0f, 0f));

                // Check the down raycasts to see if there is no longer a floor beneath the enemy on the right
                if ((hit1.distance < hit2.distance && hit1.collider != null) || (hit2.collider == null && hit1.collider != null))
                {
                    backToStart = true; // if so, turn around
                }

                // Read for wall to the right
                tempPosition.x = transform.position.x + (sprite.bounds.extents.x - .7f); // reset temp pos

                RaycastHit2D forward = Physics2D.Raycast(tempPosition, Vector2.right, .5f); // cast ray right

                Debug.DrawLine(tempPosition, tempPosition + Vector2.right * 1f, Color.blue);

                if (forward.collider != null) // if the ray hits, turn around
                {
                    velocity = Vector2.zero;
                    backToStart = true;
                }
            }

            /*if (enemyPosition.x >= startPos.x + 2f)
            {
                backToStart = true;
            }
            else if (enemyPosition.x <= startPos.x)
            {
                backToStart = false;
            }

            // Move left
            if (backToStart)
            {
                ApplyForce(new Vector3(-2f, 0f, 0f));
                //Debug.Log("left");
            }
            // Move right
            else if (backToStart == false)
            {
                ApplyForce(new Vector3(2f, 0f, 0f));
                //Debug.Log("right");
            }*/
        }
        else
        {
            velocity = Vector3.zero;
        }

        
    }

    public void TakeDamage()
    {
        alive = false;
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
