using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGostMove : MonoBehaviour
{
    Vector3 basePos; // 初期位置
    Vector3 pos; //更新された位置

    Transform playerTr; //プレイヤーのトランスフォーム
    [SerializeField] float speed = 2; // 敵の動くスピード
    [SerializeField] float followRange = 2.0f; // 追従距離
    [SerializeField] float floatHeight = 0.5f;
    [SerializeField] float floatSpeed = 2.0f;
    [SerializeField] float wanderRange = 1.0f;

    // ボスエネミーに突撃する
    public Transform boss; // Inspectorでボスを設定
    public float moveSpeed = 5.0f;
    private bool isChasingBoss = false;

    // Start is called before the first frame update
    private void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        basePos = transform.position;//Gostの初期位置
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTr.position);

        if(isChasingBoss && boss != null)
        {
            //ボスの方向ベクトル
            Vector3 direction = (boss.position - transform.position).normalized;

            //ボスの方へ移動
            transform.position += direction * moveSpeed * Time.deltaTime;


            transform.rotation = Quaternion.LookRotation(direction);
        }



        // プレイヤーが一定範囲内に入った場合
        if (distance < followRange)
        {
            //プレイヤーに向かって進む
            transform.position = Vector3.MoveTowards(
                transform.position,
                playerTr.position,
                //new Vector3(playerTr.position.x, playerTr.position.y),
                speed * Time.deltaTime);

            Vector3 pos = transform.position;
            Debug.Log("入ってきたやん");
        }

        else
        {
            //幽霊っぽい挙動
            //float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            //float xOffset = Mathf.Sin(Time.time * (floatSpeed * 0.5f)) * wanderRange;

            //transform.position = basePos + new Vector3(xOffset, yOffset, 0);

            
            Debug.Log("誰もいない");
        }

    }

    //ハンマーがRedGostに当たったらボスの方向に向かう
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hammer"))
        {
            isChasingBoss = true;
            Debug.Log("ハンマーに当たったからボスに突撃ー！");

        }
        if (other.CompareTag("Boss"))
        {

            Debug.Log("ボスに当たったら消える");
            Destroy(gameObject);
        }
    }

}
