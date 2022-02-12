using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public string name;
    public Transform firePoint;
    public GameObject bullet;
    public int holding;
    public int damage;
    public int bulletSpeed;
    public int waitTime;
    public int integrity;
    bool executed = true;

    Vector3 shootDirection;
    Vector2 lookDirection;
    float lookAngle;

    void Update()
    {
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection-transform.position;

        //Check if the mouse is clicked and weapon is being held
        //Only shoot if the co_routine has finished executing
        if (Input.GetMouseButtonDown(0) && holding == 1 && executed)
        {
            StartCoroutine("Shoot");
        }
    }

    //Shoots the bullet and then waits for the specified time in waitTime
    private IEnumerator Shoot()
    {
        executed = false;
        GameObject bulletInstance = Instantiate(bullet);
        bulletInstance.transform.position = firePoint.position;
        bulletInstance.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * bulletSpeed, shootDirection.y * bulletSpeed);
        
        //Lose integrity when shooting
        integrity = integrity - damage/2;
        Debug.Log("Weapon integrity = " + integrity);
        
        //If integrity reaches 0, start sequence to destroy item
        if(integrity == 0)
        {
            Destroy(gameObject);
            holding = 0;
            PickUp.left = false;
            GetComponent<PickUp>().item1 = null;
            
        }
        yield return new WaitForSeconds(waitTime);
        executed = true;
    }
    
}
