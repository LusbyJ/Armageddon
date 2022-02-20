using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public string name;
    public Transform firePoint;
    public GameObject bullet;
    public int holding = 0;
    public int damage;   //damage done to integrity when shot
    public float waitTime;
    public int maxIntegrity;
    public int integrity;
    public Sprite weaponIcon;
    bool executed = true;

    public static Vector3 shootDirection;
 
    float lookAngle;

    void Update()
    {
        //Check if the mouse is clicked and weapon is being held
        //Only shoot if the co_routine has finished executing
        if (Input.GetMouseButton(0) && holding == 1 && executed)
        {
            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection - transform.position;

            if (shootDirection.x > 0.5 || shootDirection.x < -0.5)
            {
            StartCoroutine("Shoot");
            //GetComponent<Weapon>().shootDirection = shootDirection;
            }
        }
    }

    //Shoots the bullet and then waits for the specified time in waitTime
    private IEnumerator Shoot()
    {
        executed = false;
        
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        

        //Lose integrity when shooting
        integrity = integrity - damage;


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