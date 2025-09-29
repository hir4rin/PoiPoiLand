using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1EnemyController : MonoBehaviour
{
    private GameObject stone;
    //private Transform stone; // stoneのTransformをInspectorに入れるようにする
    private Vector3 direction; // 移動方向
    private Vector3 pos; // 現在位置
    public float speed;

    void Start()
    {
        stone = GameObject.Find("Stone"); // stoneオブジェクトを探す
        pos = transform.position; // 初期位置を保存
        direction = (new Vector3(100.0f,4.8f,5.0f) - transform.position).normalized; // stoneの方向を向く
        speed = 0.01f; // 移動速度
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = pos;
        pos += direction * speed; // 移動量を加算
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Stone")
        {
            Destroy(this.gameObject);
        }
    }
}