using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public string name;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int holding;
    public Transform shootPosition;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && holding ==1)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
