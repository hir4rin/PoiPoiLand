using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Stage1Wave
{
    Wave1,
    Wave2,
    Wave3
}
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
    private int waveTimer;
    private Stage1Wave wave;
    void Start()
    {
        enemyPrefab = (GameObject)Resources.Load("Stage1_Ghost");
        stage1Manager = stage1.GetComponent<Stage1Manager>();
        waveTimer = 0;
        wave = Stage1Wave.Wave1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(waveTimer);
        stageState = stage1Manager.State;
        if (stageState != Stage1State.Start)
        {
            waveTimer = 0;
            return;
        }
        else
        {
            waveTimer++;

            switch (wave)
            {
                case Stage1Wave.Wave1:
                    if (enemyPrefab != null)
                    {

                        if (!isSpawn) // 初期スポーン
                        {
                            SpawnEnemyAll();
                            isSpawn = true;
                        }
                        if (waveTimer == 250) // 5秒後にスポーン
                        {
                            SpawnEnemyPoint1();
                        }
                        if (waveTimer == 375) // 7.5秒後にスポーン
                        {
                            SpawnEnemyPoint2();
                            SpawnEnemyPoint3();
                        }
                        if (waveTimer == 500) // 10秒後にスポーン
                        {
                            SpawnEnemyAll();
                            waveTimer = 0;
                            wave++;
                        }
                    }
                    break;
                case Stage1Wave.Wave2:
                    if(enemyPrefab != null)
                    {
                        if(waveTimer == 100) // 12秒後にスポーン
                        {
                            SpawnEnemyPoint4();
                        }
                        if (waveTimer == 250) // 15秒後にスポーン
                        {
                            SpawnEnemyPoint1();
                        }
                        if (waveTimer == 375) // 17.5秒後にスポーン
                        {
                            SpawnEnemyPoint2();
                            SpawnEnemyPoint3();
                        }
                        if (waveTimer == 500) // 20秒後にスポーン
                        {
                            SpawnEnemyAll();
                            waveTimer = 0;
                            wave++;
                        }
                    }
                    break;
                case Stage1Wave.Wave3:
                    if (enemyPrefab != null)
                    {
                        if (waveTimer == 50) // 21秒後にスポーン
                        {
                            SpawnEnemyPoint1();
                        }
                        if(waveTimer == 100)
                        {
                            SpawnEnemyPoint1();
                        }
                        if(waveTimer == 150)
                        {
                            SpawnEnemyPoint1();
                        }
                        if (waveTimer == 250) // 25秒後にスポーン
                        {
                            SpawnEnemyPoint2();
                        }
                        if (waveTimer == 375) // 27.5秒後にスポーン
                        {
                            SpawnEnemyPoint3();
                        }
                        if (waveTimer == 500) // 30秒後にスポーン
                        {
                            SpawnEnemyPoint1();
                            waveTimer = 0;
                        }
                    }
                    break;
                default:
                    Debug.Log("エラーデス!!");
                    break;
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
