using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public Rigidbody2D rb;
    public float bulletSpeed;
    public Vector3 shootDirection;
    public bool enemyFire = false;
    public GameObject enemy;
    

    void Start()
    {
        if(!enemyFire)
            shootDirection = Weapon.shootDirection;

        //Change the rotation of the bullet in relation to angle shot
        if (!enemyFire)
        {
            if (shootDirection.x < 0)
                rb.transform.rotation = Quaternion.Euler(0, 0, shootDirection.y * -36);
            else
                rb.transform.rotation = Quaternion.Euler(new Vector3(0, 180, shootDirection.y * -36));
        }
        else
        {
            if (shootDirection.x < 0)
                rb.transform.rotation = Quaternion.Euler(0, 0, shootDirection.y * -36);
            else
                rb.transform.rotation = Quaternion.Euler(new Vector3(0, 180, shootDirection.y * -36));

        }

        rb.velocity = (new Vector2(shootDirection.x, shootDirection.y).normalized) * bulletSpeed;
      
    }

    //If bullet hits object destroy it, if enemy hit cause damage
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.gameObject.tag != "Player" && !enemyFire)
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if(enemy != null)
            {       
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if(hitInfo.gameObject.tag == "Player" && enemyFire)
        {
            Health health = hitInfo.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            Destroy(gameObject);
            enemy.GetComponent<RangeAttack>().numBullets--;
        }
    }
}
