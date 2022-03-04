using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject meleeEnemyPrefab;
    public GameObject groundRangeEnemyPrefab;
    public GameObject flyingRangeEnemyPrefab;
    public GameObject pinkey;
    public GameObject stopwatch;
    public float respawnTime = 1f;
    public Vector2 screenBounds;
    public GameObject character;
    public int enemyCount = 0;
    public int enemyMax = 5;
    public int groundRangeDamage = 15;
    public int flyingRangeDamage = 15;
    public int enemiesLeftToKill;
    bool pinkeySpawned;



    // Start is called before the first frame update
    void Start()
    {
        //Bounds hard coded. Need to adjust depending on the map or keep each map roughly the same size
        screenBounds = new Vector2 (46.5f, 11.2f);
        pinkeySpawned = false;
        StartCoroutine(enemyWave());
    }

    private void spawnMeleeEnemy()
    {
        GameObject a = Instantiate(meleeEnemyPrefab) as GameObject;
        a.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(3.44f, screenBounds.y));
        a.GetComponent<EnemyAI>().target = character.GetComponent<Transform>();
        a.GetComponent<Enemy>().enemySpawn = gameObject;
        a.GetComponent<EnemyAI>().stopwatch = stopwatch;
        enemyCount++;
    }

    private void spawnRangeGroundEnemy()
    {
        GameObject b = Instantiate(groundRangeEnemyPrefab) as GameObject;
        b.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(3.44f, screenBounds.y));
        b.GetComponent<EnemyAI>().target = character.GetComponent<Transform>();
        b.GetComponent<RangeAttack>().target = character.GetComponent<Transform>();
        b.GetComponent<Enemy>().enemySpawn = gameObject;
        b.GetComponent<EnemyAI>().stopwatch = stopwatch;
        b.GetComponent<RangeAttack>().damage = groundRangeDamage;
        enemyCount++;
    }

    private void spawnRangeFlyingEnemy()
    {
        GameObject c = Instantiate(flyingRangeEnemyPrefab) as GameObject;
        c.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(3.44f, screenBounds.y));
        c.GetComponent<EnemyAI>().target = character.GetComponent<Transform>();
        c.GetComponent<RangeAttack>().target = character.GetComponent<Transform>();
        c.GetComponent<Enemy>().enemySpawn = gameObject;
        c.GetComponent<EnemyAI>().stopwatch = stopwatch;
        c.GetComponent<EnemyAI>().flying = true;
        c.GetComponent<RangeAttack>().damage = flyingRangeDamage;
        enemyCount++;
    }

    private void spawnPinkey()
    {
        pinkey.SetActive(true);
        pinkeySpawned = true;
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
            if(enemiesLeftToKill <= 0 && !pinkeySpawned)
            {
                spawnPinkey();
            }
        }
    }


}
