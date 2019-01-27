using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    // Player Vars
    public GameObject playerPrefab;
    public Player playerScript;
    public GameObject playerObject;

    //guiElements
    public GameObject playerHealth;
    public GameObject playerAmmo;
    public List<Sprite> playerBullets;
    public List<Sprite> healthTextures;
    public List<GameObject> bullets;
    private int lastFrameBullets;
    private int lastFrameHealth;

    // Enemy Vars
    public GameObject enemyPrefab;
    public List<GameObject> enemies = new List<GameObject>();   // List to hold all enemies in scene
    public GameObject enemyObjectPlaceholder;                   // Placeholder for enemy object for adding enemies
    public Enemy enemyScript;

    // Bullet Vars
    public GameObject bulletPrefab;

    // Save Vars
    public CutsceneManager CM;
    public Saving saver;
    public int enemiesLeft;


	// Use this for initialization
	void Start () {
        enemyScript = enemies[0].GetComponent<Enemy>();

        enemyScript.playerObject = playerObject;

        for (int i = 0; i < 10; i++)
        {
            AddEnemy(Random.Range(-10, 10), Random.Range(-5, 5));
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (enemyScript.bShoot)
        {
            playerObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (enemyScript.bWander)
        {
            playerObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            playerObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        playerObject.transform.position += new Vector3(.05f, 0);

        // Enemy Updates
        for (int i = 0; i < enemies.Count; i++)
        {
            enemyScript = enemies[i].GetComponent<Enemy>();

            enemyScript.playerDistance = enemyScript.GetDistanceSqrd(playerObject);

            enemyScript.Shoot(playerObject, bulletPrefab); // Shoots at the player if they are nearby

            enemyScript.Wander(); // Wanders if player is nearby

            // Enemy Dying
            if (!enemyScript.alive)
            {
                enemyObjectPlaceholder = enemies[i];
                enemies.Remove(enemies[i]);
                Destroy(enemyObjectPlaceholder);
            }
        }

        // Saving of the Game
        if (Input.GetKeyDown(KeyCode.K))
        {
            saver.SaveCharacter(playerScript, CM, this, 1);
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            saver.SaveCharacter(playerScript, CM, this, 1);
        }

        //update bullet count
        SetBulletCounter();
        //update health count
        SetHealthCounter();
    }

    /// <summary>
    /// Changes on screen sprite to match the players current health
    /// </summary>
    private void SetHealthCounter()
    {
        if(playerScript.currentHealth < lastFrameHealth)
        {
            int currenthImage = playerScript.baseHealth - playerScript.currentHealth;
            playerHealth.GetComponent<SpriteRenderer>().sprite = healthTextures[currenthImage];
        }
    }

    private void SetBulletCounter()
    {
        if(playerScript.bulletsTillReload < lastFrameBullets)
        {
            int numberBullets = (int)playerScript.bulletsTillReload;
            for(int i=0; i< numberBullets; i++)
            {
                bullets[i].SetActive(true);
            }
            for(int y = numberBullets; numberBullets < 6; y++)
            {
                bullets[y].SetActive(false);
            }
        }
    }
    // Add enemies to the scene
    private void AddEnemy(int x, int y)
    {
        enemyObjectPlaceholder = GameObject.Instantiate(enemyPrefab, new Vector3(x, y), Quaternion.identity);

        enemies.Add(enemyObjectPlaceholder);
    }
}
