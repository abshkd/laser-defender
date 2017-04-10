using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public float health = 20f;

    public GameObject laserPrefab;
    public float projectileSpeed = -1.0f;
    public float firingRate = 0.5f;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {


        Projectile laser = collider.gameObject.GetComponent<Projectile>();

        if (laser)
        {
            health -= laser.GetDamage();
            laser.Hit();
            if(health <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }

    void Fire()
    {
        Vector3 adjustPosition = transform.position + new Vector3(0, -1, 0);
        GameObject projectile = Instantiate(laserPrefab, adjustPosition, Quaternion.identity);

        projectile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }

    private void Update()
    {
        float probability = firingRate * Time.deltaTime;
        if(Random.value < probability)
            Fire();
    }
}
