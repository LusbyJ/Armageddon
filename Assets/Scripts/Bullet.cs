using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public Rigidbody2D rb;
    public float bulletSpeed;
    public Vector3 shootDirection;
    

    void Start()
    {
        shootDirection = Weapon.shootDirection;
        
        //Change the rotation of the bullet in relation to angle shot
        if (shootDirection.x < 0)
            rb.transform.rotation = Quaternion.Euler(0, 0, shootDirection.y*-36);
        else
            rb.transform.rotation = Quaternion.Euler(new Vector3(0, 180, shootDirection.y*-36));
        
        //adjustShotSpeed();
    }

    public void adjustShotSpeed()
    {
        //Adjusts speed of the bullet in relation to distance clicked from player
        if (shootDirection.x < -1)
            rb.velocity = new Vector2(shootDirection.x * bulletSpeed / 4, shootDirection.y * bulletSpeed / 4);

        if (shootDirection.x < 0 && shootDirection.x > -2)
            rb.velocity = new Vector2(shootDirection.x * (bulletSpeed * 3), shootDirection.y * (bulletSpeed * 3));


        else if (shootDirection.x < 1 && shootDirection.x >= 0.5)
            rb.velocity = new Vector2(shootDirection.x * (bulletSpeed * 4), shootDirection.y * (bulletSpeed * 4));

        else if (shootDirection.x < 2 && shootDirection.x >= 1)
            rb.velocity = new Vector2(shootDirection.x * (bulletSpeed * 2), shootDirection.y * (bulletSpeed * 2));

        else
            rb.velocity = new Vector2(shootDirection.x * bulletSpeed, shootDirection.y * bulletSpeed);
    }

    //If bullet hits object destroy it, if enemy hit cause damage
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {       
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
