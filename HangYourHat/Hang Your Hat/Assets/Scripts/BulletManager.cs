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
    public float speed;
    private LayerMask mask;
    public Player player;



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
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (bullets.Count > 0)
        {
            for  (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].transform.Translate(speed * bullets[i].transform.right * Time.deltaTime, Space.World);
                Debug.Log("Speed: " + speed * bullets[i].transform.right * Time.deltaTime);
                

                hit = Physics2D.Raycast(bullets[i].transform.position,
                    bullets[i].transform.right,
                    speed * Time.deltaTime,
                    mask);

                if (hit.collider != null)
                {
                    GameObject tempBullet = bullets[i];
                    bullets.Remove(bullets[i]);
                    Destroy(tempBullet);

                    if (hit.collider.gameObject.tag == "Enemy")
                    {
                        //hit.collider.gameObject.GetCompnent<Enemy>().TakeDamage();
                    }

                    else if (hit.collider.gameObject.tag == "Bullet")
                    {
                        GameObject tempBullet2 = hit.collider.gameObject;
                        bullets.Remove(hit.collider.gameObject);
                        Destroy(tempBullet2);
                    }
                    else if (hit.collider.gameObject.tag == "Player")
                    {
                        //hit player
                        player.TakeDamage();
                    }
                    Debug.Log("This shit ded");
                }


                else if (bullets[i].transform.position.x < -width ||
                    bullets[i].transform.position.x > width ||
                    bullets[i].transform.position.y < -height ||
                    bullets[i].transform.position.y > height)
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
