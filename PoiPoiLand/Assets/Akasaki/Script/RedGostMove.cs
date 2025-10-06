using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class RedGostMove : MonoBehaviour
{
    Vector3 basePos; // 初期位置
    Vector3 pos; //更新された位置

    public Transform playerTransform; //プレイヤーのトランスフォーム
    [SerializeField] float speed = 2; // 敵の動くスピード
    [SerializeField] float followRange = 2.0f; // 追従距離
    [SerializeField] float floatHeight = 0.5f; //幽霊っぽい縦の挙動
    [SerializeField] float floatSpeed = 2.0f;  //幽霊っぽい動きのスピード
    [SerializeField] float wanderRange = 1.0f; //幽霊っぽい横の挙動

    // ボスエネミーに突撃する
    public Transform boss; // Inspectorでボスを設定
    public float moveSpeed = 5.0f;
    private bool isChasingBoss = false;

    //ハンマー
    HammerController _hammer;//ハンマー

    // Start is called before the first frame update
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //  basePos = transform.position;//Gostの初期位置
        if (playerTransform == null)
        {
            GameObject obj = GameObject.FindWithTag("Player"); // Playerタグのオブジェクトを探す
            if (obj != null)
            {
                playerTransform = obj.transform;
            }
        }
        //Boss用
        if (boss == null)
        {
            GameObject objBoss = GameObject.FindWithTag("Boss"); // ボスタグのオブジェクトを探す
            if (objBoss != null)
            {
                boss = objBoss.transform;
            }
        }
        //ハンマー用
        _hammer = Resources.Load<GameObject>("Hammer_Prefab").GetComponent<HammerController>();
    }

    // Update is called once per frame
    void Update()
    {

        //キャラクターの方を向く
        if (!isChasingBoss)
        {
            LookAtPlayerQuaternion();
        }
        else if (isChasingBoss)
        {
            LookAtBossQuaternion();
        }

            float distance = Vector3.Distance(transform.position, playerTransform.position);

       


        // プレイヤーが一定範囲内に入った場合
        //if (distance < followRange)
        //{
            //プレイヤーに向かって進む
          
          //  Debug.Log("入ってきたやん");
        //}

        //else
        //{
            //幽霊っぽい挙動
            //float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            //float xOffset = Mathf.Sin(Time.time * (floatSpeed * 0.5f)) * wanderRange;

            //transform.position = basePos + new Vector3(xOffset, yOffset, 0);

            
          //  Debug.Log("誰もいない");
        //}

    }
    private void FixedUpdate()
    {
        if (!isChasingBoss)
        {
            transform.position = Vector3.MoveTowards(
           transform.position,
           playerTransform.position,
           //new Vector3(playerTransform.position.x, playerTransform.position.y),
           speed * Time.deltaTime);

            Vector3 pos = transform.position;
        }
        else if (isChasingBoss)
        {
            Vector3 toBoss = boss.position - transform.position;
            transform.position += toBoss.normalized * speed * Time.deltaTime * 2;

        }
     
    }
    void LookAtPlayerQuaternion()
    {
        Vector3 dir = playerTransform.position - transform.position;
        dir.y = 0;//y軸は動かさない
        if (dir.sqrMagnitude > 0.01f)//向く方向がある場合のみ
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir.normalized);
            transform.rotation = targetRotation;
        }
    }
    void LookAtBossQuaternion()
    {
        Vector3 dir = boss.position - transform.position;
        dir.y = 0;//y軸は動かさない
        if (dir.sqrMagnitude > 0.01f)//向く方向がある場合のみ
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir.normalized);
            transform.rotation = targetRotation;
        }
    }

    //ハンマーがRedGostに当たったらボスの方向に向かう
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hammer"))//ハンマーのタグが投げるだったら
        {
             _hammer = other.GetComponent<HammerController>();
            if (_hammer.currentState == HammerState.thrown)
            {
                isChasingBoss = true;
                Debug.Log("ハンマーに当たったからボスに突撃ー！");
            }
            

        }
        if (other.CompareTag("Boss"))
        {

            Debug.Log("ボスに当たったら消える");
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
         //   Debug.Log("プレイヤーに当たったら消える");
            Destroy(gameObject);
        }
    }
    public void SetTarget(Transform target)
    {
        playerTransform = target;
    }
    public void SetBoss(Transform target)
    {
        boss = target;
    }
    
}
