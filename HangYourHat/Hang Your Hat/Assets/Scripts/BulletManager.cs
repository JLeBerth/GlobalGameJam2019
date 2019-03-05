using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletManager : MonoBehaviour
{
    private Camera cam;

    RaycastHit2D hit;
    public float width;
    public float height;
    public static List<GameObject> bullets;
    public static Dictionary<GameObject, string> bulletDic = new Dictionary<GameObject, string>();
    public float speed;
    private LayerMask mask;
    public GameObject player;
    public float goldRatVar;



    void Awake()
    {

    }

	// Use this for initialization
	void Start ()
    {
        bullets = new List<GameObject>();
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;

        mask = LayerMask.GetMask("Collision");

        goldRatVar = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(bullets.Count);

        if (bullets.Count > 0)
        {
            for  (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].transform.Translate(speed * bullets[i].transform.right * Time.deltaTime, Space.World);

                Debug.Log("Using " + player.GetComponent<Player>().gunUsage);

                if (player.GetComponent<Player>().gunUsage == 3 && bulletDic[bullets[i]] == "Player")
                {
                    bullets[i].transform.rotation = Quaternion.Euler(0, 0, 10 * goldRatVar);

                    float rotation = .2f;
                    goldRatVar += rotation;

                }

                hit = Physics2D.Raycast(bullets[i].transform.position,
                    bullets[i].transform.right,
                    speed * Time.deltaTime,
                    mask);

                if (hit.collider != null)
                {

                    /*GameObject tempBullet = bullets[i];
                    bullets.Remove(bullets[i]);
                    Destroy(tempBullet);*/

                    if (hit.collider.gameObject.tag == "Enemy")
                    {
                        hit.collider.gameObject.GetComponent<Enemy>().TakeDamage();
                        GameObject tempBullet = bullets[i];
                        bullets.Remove(bullets[i]);
                        Destroy(tempBullet);
                        Player.coal += 10;
                    }

                    else if (hit.collider.gameObject.tag == "Bullet")
                    {
                        GameObject tempBullet2 = hit.collider.gameObject;
                        bullets.Remove(hit.collider.gameObject);
                        Destroy(tempBullet2);
                        GameObject tempBullet = bullets[i];
                        bullets.Remove(bullets[i]);
                        Destroy(tempBullet);
                    }
                    else if (hit.collider.gameObject.tag == "Player")
                    {
                        //hit player
                        if (!player.GetComponent<Player>().rolling)
                        {
                            player.GetComponent<Player>().TakeDamage();
                            GameObject tempBullet = bullets[i];
                            bullets.Remove(bullets[i]);
                            Destroy(tempBullet);
                        }
                        else
                        {
                            bullets[i].GetComponent<BoxCollider2D>().enabled = false;
                        }
                    }
                    else
                    {
                        GameObject tempBullet = bullets[i];
                        bullets.Remove(bullets[i]);
                        Destroy(tempBullet);

                        if (bullets[i].GetComponent<BoxCollider2D>().enabled == false)
                        {
                            bullets[i].GetComponent<BoxCollider2D>().enabled = true;
                        }
                    }
                }


                else if (bullets[i].transform.position.x < cam.transform.position.x -width ||
                    bullets[i].transform.position.x > cam.transform.position.x + width ||
                    bullets[i].transform.position.y < cam.transform.position.y - height ||
                    bullets[i].transform.position.y > cam.transform.position.y + height)
                {
                    GameObject tempBullet = bullets[i];
                    bullets.Remove(bullets[i]);
                    Destroy(tempBullet);
                }

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
