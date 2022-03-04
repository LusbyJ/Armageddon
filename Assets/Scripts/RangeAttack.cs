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
    Vector3 distance;
    float waitTime = 50f;
    public int numBullets = 0;
    public int maxBullets = 5;
    public int damage;
    public Vector3 attackRange = new Vector3(10f, 5f, 0f);

    //void OnTriggerEnter2D (Collider2D other)
    //{
    //    attack = true;
    //    shootDirection = GetComponent<EnemyAI>().direction;
    //    StartCoroutine("Shoot");
    //}

    
    void Update()
    {
        distance = target.position - gameObject.transform.position;
        if ((distance.x < attackRange.x && distance.x > -attackRange.x) && 
            (distance.y < attackRange.y && distance.y > -attackRange.y))
        {
            shootDirection = target.position;
            shootDirection.z = 0.0f;
            shootDirection = shootDirection - gameObject.transform.position;
            if (numBullets == 0)
            {
                attack = true;
                StartCoroutine(Shoot());
            }
        }
    }


    private IEnumerator Shoot()
    {
        while (true)
        {
            if (attack)
            {
                Debug.Log("Shooting");
                spawnBullet();
                if (numBullets >= maxBullets)
                    attack = false;
            }
            yield return new WaitForSeconds(waitTime*2);
        }
    }

    void spawnBullet()
    {
            GameObject a = Instantiate(bullet, firePoint.position, firePoint.rotation);
            a.GetComponent<Bullet>().enemyFire = true;
            a.GetComponent<Bullet>().shootDirection = shootDirection;
            a.GetComponent<Bullet>().enemy = gameObject;
            a.GetComponent<Bullet>().damage = damage;
            numBullets++;
            Debug.Log("Bullet" + numBullets);
    }
}
