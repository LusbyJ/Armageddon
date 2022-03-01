using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{

    public Transform firePoint;
    public Transform target;
    public bool attack = false;
    public GameObject bullet;
    public Vector3 shootDirection;
    public float waitTime = 3f;
    public int numBullets = 0;
    public Vector3 attackRange = new Vector3(10f, 5f, 0f);

    //void OnTriggerEnter2D (Collider2D other)
    //{
    //    attack = true;
    //    shootDirection = GetComponent<EnemyAI>().direction;
    //    StartCoroutine("Shoot");
    //}

    void Update()
    {
        Vector3 distance = target.position - gameObject.transform.position;
        if ((distance.x < attackRange.x && distance.x > -attackRange.x) && 
            (distance.y < attackRange.y && distance.y > -attackRange.y))
        {
            shootDirection = target.position;
            shootDirection.z = 0.0f;
            shootDirection = shootDirection - gameObject.transform.position;
            StartCoroutine("Shoot");
        }
    }


    private IEnumerator Shoot()
    {
        while (true)
        {
            if (numBullets == 0)
            {
                Debug.Log("Shooting");
                spawnBullet();
                numBullets++;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }

    void spawnBullet()
    {
        GameObject a = Instantiate(bullet, firePoint.position, firePoint.rotation);
        a.GetComponent<Bullet>().enemyFire = true;
        a.GetComponent<Bullet>().shootDirection = shootDirection;
        a.GetComponent<Bullet>().enemy = gameObject;
    }
}
