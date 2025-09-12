using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    //ハンマーのスピード
    private Vector3 speed = new Vector3(0.0f,10.0f,10.0f);
    //x軸を軸にして毎秒6度、回転させるQuaternionを作成
    Quaternion rotation = Quaternion.AngleAxis(20, Vector3.right);
    //初期位置を設定
    private Vector3 startPos = new Vector3(0.0f,5.0f,0.0f);
    //重力を設定
    private Vector3 gravity = new Vector3(0.0f, -9.8f, 0.0f);
    //時間
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //現在のポジションに初期位置を設定する
        transform.position = startPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //時間を更新
        time += Time.deltaTime;

        //現在の自身の回転の情報を取得する。
        Quaternion q = this.transform.rotation;
        //合成して自身に設定
        this.transform.rotation =rotation * q;
        //飛んでいくときの軌道を放物線にする
        Vector3 pos = startPos
                      + speed * time
                      + 0.5f * gravity * (time * time);

        transform.position = pos;
    }
}
