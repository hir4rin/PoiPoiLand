using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class magicball : MonoBehaviour
{


    //移動用
    Vector3 targetPosition;
    bool isMoving = false;

    Vector3 moveDir = Vector3.zero;

    public float moveSpeed = 0.1f;

    float stopDistance = 1f;

   

    //プレイヤー(仮置き)
    public Transform Hero;


    // Start is called before the first frame update
    void Start()
    {
            if (Hero == null)
            {
                GameObject obj = GameObject.FindWithTag("Player"); // Playerタグのオブジェクトを探す
                if (obj != null)
                {
                    Hero = obj.transform;
                }
            }
            StartMovePlayer();
        MoveDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
           
        //}
    }
    private void FixedUpdate()
    {
     

        if (isMoving)
        {

            Debug.Log("移動中");
            transform.position += moveDir * moveSpeed;


            Vector3 nowPos = transform.position;
            nowPos.y = 0;//y軸は動かさない
            //ターゲット座標との現在ベクトルと移動方向ベクトルの内積をとる
            if ((Vector3.Dot(moveDir, (targetPosition - nowPos)) < 0) && (Vector3.Distance(nowPos, targetPosition) > stopDistance))//プレイヤーの奥になったら
            {
                Debug.Log("奥に行った");
                //距離がstopDistance以下になったら
                //  if (Vector3.Distance(nowPos, targetPosition) > stopDistance)
                // {
                moveDir = Vector3.zero;
                isMoving = false;
                Debug.Log("到着");
                //Debug.Log("nowPos: " + nowPos + " / targetPosition: " + targetPosition);
                //Debug.Log("距離: " + Vector3.Distance(nowPos, targetPosition));
                
                //}

            }

        }
    }

    void StartMovePlayer()
    {
        //ターゲットの位置を取得
        targetPosition = Hero.position;
        //targetPosition.y = transform.position.y;//y軸は動かさない
        isMoving = true;
    }

    void MoveDirection()
    {
        moveDir = (targetPosition - transform.position).normalized;
        //moveDir.y = 0;//y軸は動かさない
    }

    public void SetTarget(Transform target)
    {
        Hero = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")//地面に当たったら消える
        {
            Debug.Log("当たった");
            Destroy(this.gameObject);
        }

    }


}
