using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    public int health = 100;
    public GameObject enemySpawn;
    
    public void TakeDamage(int damage)
    {

        StartCoroutine("Blink");
        health -= damage;
        Debug.Log("Enemy health is " +  health);
        if(health <= 0)
        {
            Die();
        }
    }

    private IEnumerator Blink()
    {
       
            GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            GetComponent<Renderer>().material.color = Color.white;
            StopCoroutine("Blink");
        
    }


    void Die()
    {
        Destroy(gameObject);
        enemySpawn.GetComponent<SpawnEnemy>().enemyCount--;
    }
}
