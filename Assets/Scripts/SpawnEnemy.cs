using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridGraph;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject meleeEnemyPrefab;
    public float respawnTime = 1f;
    private Vector2 screenBounds;


    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, screenBounds.height, Camera.main.transform.position.z));
        StartCoroutine(meleeEnemyWave());
    }


    private void spawnEnemey()
    {
        GameObject a = Instantiate(meleeEnemyPrefab) as GameObject;
        a.transform.position = new Vector2(screenBounds.x, Random.Range(screenBounds.y,
            .y);
    }

    IEnumerator meleeEnemyWave()
    {
        while (true)
        {
            yeild return new WaitForSeconds(respawnTime);
            spawnEnemey();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
