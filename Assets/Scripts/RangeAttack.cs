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
    public float waitTime = 3f;
    float shotAt;
    float currentTime;
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
            currentTime = Time.time;
            yield return new WaitForSeconds(waitTime);
            if (attack && (shotAt == null || currentTime >= shotAt+waitTime))
            {
                spawnBullet();
                if (numBullets >= maxBullets)
                    attack = false;
            }
        }
    }

    void spawnBullet()
    {
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.carpalGun);
        GameObject a = Instantiate(bullet, firePoint.position, firePoint.rotation);
        a.GetComponent<Bullet>().enemyFire = true;
        a.GetComponent<Bullet>().shootDirection = shootDirection;
        a.GetComponent<Bullet>().enemy = gameObject;
        a.GetComponent<Bullet>().damage = damage;
        a.GetComponent<Bullet>().shotAt = shotAt;
        numBullets++;
        shotAt = Time.time;
    }
}
