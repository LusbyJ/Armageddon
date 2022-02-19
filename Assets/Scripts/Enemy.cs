using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    public int health = 100;
    public GameObject enemySpawn;
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy health is " +  health);
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        enemySpawn.GetComponent<SpawnEnemy>().enemyCount--;
    }
}
