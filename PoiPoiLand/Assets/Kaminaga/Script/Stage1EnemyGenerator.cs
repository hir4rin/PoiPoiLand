using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint1;
    [SerializeField] private GameObject spawnPoint2;
    [SerializeField] private GameObject spawnPoint3;
    [SerializeField] private GameObject spawnPoint4;
    private GameObject enemyPrefab;
    bool isSpawn = false;
    int counter = 0;
    void Start()
    {
        enemyPrefab = (GameObject)Resources.Load("Stage1_Ghost");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;
        if(counter > 600) // 10ïbå„Ç…ÉXÉ|Å[Éì
        {
            isSpawn = false;
            counter = 0;
        }
        if (enemyPrefab != null)
        {
            if (!isSpawn)
            {
                SpawnEnemy();
                isSpawn = true;
            }
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint1.transform.position, Quaternion.identity);
        Instantiate(enemyPrefab, spawnPoint2.transform.position, Quaternion.identity);
        Instantiate(enemyPrefab, spawnPoint3.transform.position, Quaternion.identity);
        Instantiate(enemyPrefab, spawnPoint4.transform.position, Quaternion.identity);
    }
}
