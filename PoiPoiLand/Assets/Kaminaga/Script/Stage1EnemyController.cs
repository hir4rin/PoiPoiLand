using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1EnemyController : MonoBehaviour
{
    private GameObject stonePos;
    private Stage1Manager stage1Manager;
    private Stage1State stage1State;
    //private Transform stone; // stoneのTransformをInspectorに入れるようにする
    private Vector3 direction; // 移動方向
    private Vector3 pos; // 現在位置
    public float speed;

    void Start()
    {
        stonePos = GameObject.Find("StonePos"); // stoneの場所(移動用に補正してある)オブジェクトを探す
        stage1Manager = GameObject.Find("Stage1Manager").GetComponent<Stage1Manager>();
        pos = transform.position; // 初期位置を保存
        direction = (stonePos.transform.position - transform.position).normalized; // stoneの方向を向く
        speed = 0.02f; // 移動速度
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stage1State = stage1Manager.State;
        if(stage1State != Stage1State.Start)
        {
            stage1Manager.enemyNum--;
            Destroy(this.gameObject);
        }
        else
        {
            transform.position = pos;
            pos += direction * speed; // 移動量を加算
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
            
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Stone")
        {
            stage1Manager.enemyNum--;
            Destroy(this.gameObject);
        }
        if(other.gameObject.tag == "Hammer")
        {
            stage1Manager.enemyNum--;
            Destroy(this.gameObject);
        }
    }
}