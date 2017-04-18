using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Speed = 0.1f;
    private float x;
    private float y;
    private float minX;
    private float maxX;
    public GameObject laserPrefab;
    public float projectileSpeed = 1.0f;
    public float padding = 1f;
    public float firingRate = 0.2f;
    public float health = 3;
    public AudioClip fire;
    public AudioClip iamhit;

	// Use this for initialization
	void Start () {

        //start position of ship
        x = 0.0f;

        this.transform.position = new Vector3(x,transform.position.y,0f);
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));
        minX = leftmost.x + padding;
        maxX = rightmost.x - padding;
    }
	

    void Fire()
    {
        //don't collide your own laser.
        Vector3 adjustPosition = transform.position + new Vector3(0, 1, 0);

        GameObject projectile = Instantiate(laserPrefab, adjustPosition, Quaternion.identity);

        projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fire, transform.position, 0.5f);
    }
	// FixedUpdate is called every 0.02 seconds.
    //provides smooth animations
	void FixedUpdate () {

        x += Input.GetAxis("Horizontal") * Speed;
       // y += Input.GetAxis("Vertical") * Speed;
        x = Mathf.Clamp(x, minX, maxX);
      //  y = Mathf.Clamp(y, minY, maxY);
        Vector3 shipPosition = new Vector3(x, transform.position.y, 0f);
        this.transform.position = shipPosition;

        
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.00000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }


    }

    private void OnTriggerEnter2D(Collider2D collider)
    {


        Projectile laser = collider.gameObject.GetComponent<Projectile>();

        if (laser)
        {
            health -= laser.GetDamage();
            AudioSource.PlayClipAtPoint(iamhit, transform.position, 0.5f);
            laser.Hit();
            if (health <= 0)
            {
                Destroy(gameObject);
            }

        }
    }


}
