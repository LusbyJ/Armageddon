using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public string name;
    public Transform firePoint;
    public GameObject bullet;
    public int holding = 0;
    public int damage;   //damage done to integrity when shot
    public float bulletSpeed;
    public float waitTime;
    public int maxIntegrity;
    public int integrity;
    public Sprite weaponIcon;
    bool executed = true;

    Vector3 shootDirection;
 
    float lookAngle;

    void Update()
    {
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection-transform.position;

        //Check if the mouse is clicked and weapon is being held
        //Only shoot if the co_routine has finished executing
        if (Input.GetMouseButton(0) && holding == 1 && executed)
        {
            StartCoroutine("Shoot");
            GetComponent<Weapon>().shootDirection = shootDirection;
        }
    }

    //Shoots the bullet and then waits for the specified time in waitTime
    private IEnumerator Shoot()
    {
        executed = false;
        bullet= Instantiate(bullet, firePoint.position, firePoint.rotation);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        Debug.Log(shootDirection);

        if(shootDirection.x < 1)
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * (bulletSpeed * 3), shootDirection.y * (bulletSpeed * 3));
            Debug.Log("shooting close");
        }
        else if (shootDirection.x < 2)
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * (bulletSpeed*(3/2)), shootDirection.y * (bulletSpeed*(3/2)));
            Debug.Log("shooting close");
        }
        else
        {
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * bulletSpeed/2, shootDirection.y * bulletSpeed/2);
            Debug.Log("shooting far");
        }
        


        //Lose integrity when shooting
        integrity = integrity - damage;

        Debug.Log("Weapon integrity = " + integrity);

        //If integrity reaches 0, start sequence to destroy item
        if(integrity <= 0)
        {
            Destroy(gameObject);

            PickUp.left = false;
            GetComponent<PickUp>().item1 = null;

            holding = 0;
            GetComponent<PickUp>().item1.transform.parent = null;



        }
        yield return new WaitForSeconds(waitTime);
        executed = true;
    }
}