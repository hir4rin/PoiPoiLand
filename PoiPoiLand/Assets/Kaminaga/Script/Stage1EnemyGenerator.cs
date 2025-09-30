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
    [SerializeField] private GameObject stage1;
    private Stage1Manager stage1Manager;
    private Stage1State stageState;
    int counter = 0;
    void Start()
    {
        enemyPrefab = (GameObject)Resources.Load("Stage1_Ghost");
        stage1Manager = stage1.GetComponent<Stage1Manager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stageState = stage1Manager.State;

        if (stageState != Stage1State.Start)
        {
            return;
        }
        else
        {
            counter++;
            if (counter > 600) // 10ïbå„Ç…ÉXÉ|Å[Éì
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
        
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint1.transform.position, Quaternion.identity);
        Instantiate(enemyPrefab, spawnPoint2.transform.position, Quaternion.identity);
        Instantiate(enemyPrefab, spawnPoint3.transform.position, Quaternion.identity);
        Instantiate(enemyPrefab, spawnPoint4.transform.position, Quaternion.identity);
    }
}
