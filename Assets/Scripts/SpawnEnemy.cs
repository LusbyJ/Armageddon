using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour   
{
    public GameObject meleeEnemyPrefab;
    public GameObject groundRangeEnemyPrefab;
    public GameObject flyingRangeEnemyPrefab;
    public GameObject stopwatch;
    public float respawnTime = 1f;
    public Vector2 screenBounds;
    public GameObject character;
    public int enemyCount = 0;
    public int enemyMax = 5;



    // Start is called before the first frame update
    void Start()
    {
        //Bounds hard coded. Need to adjust depending on the map or keep each map roughly the same size
        screenBounds = new Vector2 (29.8f, 38.6f);
        StartCoroutine(enemyWave());
    }

    private void spawnMeleeEnemy()
    {
        GameObject a = Instantiate(meleeEnemyPrefab) as GameObject;
        a.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(0f, screenBounds.y));
        a.GetComponent<EnemyAI>().target = character.GetComponent<Transform>();
   //     a.GetComponentInChildren<MeleeAttack>().target = character.GetComponent<Transform>();
        a.GetComponent<Enemy>().enemySpawn = gameObject;
        a.GetComponent<EnemyAI>().stopwatch = stopwatch;
        enemyCount++;
    }

    private void spawnRangeGroundEnemy()
    {
        GameObject b = Instantiate(groundRangeEnemyPrefab) as GameObject;
        b.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(0f, screenBounds.y));
        b.GetComponent<EnemyAI>().target = character.GetComponent<Transform>();
        b.GetComponent<RangeAttack>().target = character.GetComponent<Transform>();
        b.GetComponent<Enemy>().enemySpawn = gameObject;
        b.GetComponent<EnemyAI>().stopwatch = stopwatch;
        enemyCount++;
    }

    private void spawnRangeFlyingEnemy()
    {
        GameObject c = Instantiate(flyingRangeEnemyPrefab) as GameObject;
        c.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(0f, screenBounds.y));
        c.GetComponent<EnemyAI>().target = character.GetComponent<Transform>();
        c.GetComponent<RangeAttack>().target = character.GetComponent<Transform>();
        c.GetComponent<Enemy>().enemySpawn = gameObject;
        c.GetComponent<EnemyAI>().stopwatch = stopwatch;
        enemyCount++;
    }

    IEnumerator enemyWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            if (enemyCount < enemyMax)
            {
                int enemy = Random.Range(1, 4);
                if (enemy == 1)
                    spawnMeleeEnemy();
                else if (enemy == 2)
                    spawnRangeGroundEnemy();
                else
                    spawnRangeFlyingEnemy();
                    
            }
        }
    }

    
}
