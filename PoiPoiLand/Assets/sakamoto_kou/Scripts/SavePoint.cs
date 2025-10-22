using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SaveGame(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SaveGame(int pointNum)
    {
        //セーブデータを保存する処理
        PlayerPrefs.SetInt("PointNum", pointNum);
        PlayerPrefs.Save();
        Debug.Log("セーブした！");
    }
}
