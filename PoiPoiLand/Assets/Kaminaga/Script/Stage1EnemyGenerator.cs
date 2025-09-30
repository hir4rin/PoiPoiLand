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
    int counter;
    void Start()
    {
        enemyPrefab = (GameObject)Resources.Load("Stage1_Ghost");
        stage1Manager = stage1.GetComponent<Stage1Manager>();
        counter = 0;
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

            if (enemyPrefab != null)
            {
                counter++;

                if (!isSpawn) // 初期スポーン
                {
                    SpawnEnemyAll();
                    isSpawn = true;
                }
                if (counter == 250) // 5秒後にスポーン
                {
                    SpawnEnemyPoint1();
                }
                if (counter == 375) // 7.5秒後にスポーン
                {
                    SpawnEnemyPoint2();
                    SpawnEnemyPoint3();
                }
                if (counter == 500) // 10秒後にスポーン
                {
                    SpawnEnemyAll();
                    counter = 0;
                }
            }



        }

    }

    private void SpawnEnemyAll()
    {
        Instantiate(enemyPrefab, spawnPoint1.transform.position, Quaternion.identity);
        Instantiate(enemyPrefab, spawnPoint2.transform.position, Quaternion.identity);
        Instantiate(enemyPrefab, spawnPoint3.transform.position, Quaternion.identity);
        Instantiate(enemyPrefab, spawnPoint4.transform.position, Quaternion.identity);
    }
    private void SpawnEnemyPoint1()
    {
        Instantiate(enemyPrefab, spawnPoint1.transform.position, Quaternion.identity);
    }
    private void SpawnEnemyPoint2()
    {
        Instantiate(enemyPrefab, spawnPoint2.transform.position, Quaternion.identity);
    }
    private void SpawnEnemyPoint3()
    {
        Instantiate(enemyPrefab, spawnPoint3.transform.position, Quaternion.identity);
    }
    private void SpawnEnemyPoint4()
    {
        Instantiate(enemyPrefab, spawnPoint4.transform.position, Quaternion.identity);
    }
}
