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
    bool executed = true;

    Vector2 lookDirection;
    float lookAngle;

    void Update()
    {
        //Get user input from mouse to determine direction shooting
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        //Check if the mouse is clicked and weapon is being held
        if (Input.GetMouseButtonDown(0) && holding == 1 && executed)
        {
            StartCoroutine("Shoot");
        }
    }

    //Shoots the bullet and then waits for the specified time in waitTime
    private IEnumerator Shoot()
    {
        executed = false;
        GameObject bulletClone = Instantiate(bullet);
        bulletClone.transform.position = firePoint.position;
        bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
        bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
        yield return new WaitForSeconds(waitTime);
        executed = true;
     
    }
}
