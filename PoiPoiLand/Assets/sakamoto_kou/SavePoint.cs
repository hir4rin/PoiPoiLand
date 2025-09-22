using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //セーブポイントに触れたとき
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //プレイヤーンのポジションを保持する
            SaveGame(other.gameObject);
        }
    }

    void SaveGame(GameObject player)
    {
        //セーブデータを保存する処理
        Vector3 position = player.transform.position;
        PlayerPrefs.SetFloat("PlayerX", position.x);
        PlayerPrefs.SetFloat("PlayerY", position.y);
        PlayerPrefs.SetFloat("PlayerZ", position.z);
        PlayerPrefs.Save();

        Debug.Log("セーブした！");
    }
}
