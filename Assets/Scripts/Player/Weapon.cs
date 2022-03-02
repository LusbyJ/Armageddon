using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject character;
    public GameObject bullet;
    public int holding = 0;
    public static int holding1 = 0;
    public static int holding2 = 0;
    public int spot = 0;
    public int pickedUp = 0;
    public int damage;   //damage done to integrity when shot
    public float waitTime;
    public int maxIntegrity;
    public int integrity;
    public Sprite weaponIcon;
    public bool attacking;
    bool executed = true;

    public static Vector3 shootDirection;

    float lookAngle;

    void Update()
    {

        //Check if the mouse is clicked and weapon is being held
        //Only shoot if the co_routine has finished executing
        if (Input.GetMouseButton(0) && spot == 1 && executed)
        {
            holding1 = 1;
            holding2 = 0;
            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection - transform.position;


            if ((gameObject.tag == "Melee" || gameObject.tag == "Key") && !attacking)
            {
                StartCoroutine("Stab");
                StartCoroutine("Attack");
                
            }

            if (gameObject.tag != "Melee" && gameObject.tag != "Key") 
            {
                StartCoroutine("Shoot");
               
            }
        }

        //Check if the mouse is clicked and weapon is being held
        //Only shoot if the co_routine has finished executing
        if (Input.GetMouseButton(1) && spot == 2 && executed)
        {
            holding2 = 1; 
            holding1 = 0;
           
            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection - transform.position;


            if (gameObject.tag == "Melee" && !attacking)
            {
                StartCoroutine("Stab");
                StartCoroutine("Attack");
               
            }

            if (gameObject.tag != "Melee" && gameObject.tag != "Key")
            {
                StartCoroutine("Shoot");
                
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
        if (integrity <= 0)
        {
            if(spot == 1)
            {
                Debug.Log("Arm1");
                Destroy(gameObject);
                PickUp.left = false;
                GetComponent<PickUp>().item1 = null;
                holding1 = 0;
                spot = 0;
                GetComponent<PickUp>().item1.transform.parent = null;
            }
            
            if(spot == 2)
            {
                Destroy(gameObject);
                PickUp.right = false;
                GetComponent<PickUp>().item2 = null;
                holding2 = 0;
                spot = 0;
                GetComponent<PickUp>().item2.transform.parent = null;
            }
       
        }
        yield return new WaitForSeconds(waitTime);
        executed = true;

    }


    //Shoots the bullet and then waits for the specified time in waitTime
    private IEnumerator Stab()
    {
        executed = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        gameObject.transform.position = character.transform.Find("Melee").position;

        //Lose integrity when shooting
        integrity = integrity - damage;

        //If integrity reaches 0, start sequence to destroy item
        if (integrity <= 0)
        {
            Destroy(gameObject);

            if (spot == 1)
            {
                PickUp.left = false;
                GetComponent<PickUp>().item1 = null;

                spot = 0;
                GetComponent<PickUp>().item1.transform.parent = null;
            }
            if(spot == 2)
            {
                PickUp.right = false;
                GetComponent<PickUp>().item2 = null;

                spot = 0;
                GetComponent<PickUp>().item2.transform.parent = null;
            }
        }
        yield return new WaitForSeconds(waitTime);
        executed = true;
        gameObject.transform.position = character.transform.Find("Arm1").position;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;

    }

    //Shoots the bullet and then waits for the specified time in waitTime
    private IEnumerator Attack()
    {
        executed = false;
        attacking = true;
      


        yield return new WaitForSeconds(waitTime * 2);
        executed = true;
        attacking = false;
     
    }
}