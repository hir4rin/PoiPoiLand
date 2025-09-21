using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    //----------------------------------------------------------------
    //実装する内容
    //1.動く瞬間に敵の座標をとって一定量動かす(動く時間はたまに)
    //2.ワープする
    ///3普通の玉攻撃(1と同じようにするか、ホーミングにしてy座標を下げてよけさせる)
    // 4.甲羅やゴースト(色違いゴーストを出す)
    //必要なモーション(移動時、攻撃時、)
    //5.体力バー
    //----------------------------------------------------------------


    //プレイヤー(仮置き)
    public Transform Hero;
   

    //移動用
    Vector3 targetPosition;
    bool isMoving = false;

    Vector3 moveDir = Vector3.zero;

    public float moveSpeed = 90f;
    float stopDistance = 5.0f;

    //ワープ用
    Vector3 offset = new Vector3(5,0,5);//プレイヤーと敵の距離
    Vector3 offsetMinus = new Vector3(-5, 0, -5);//プレイヤーと敵の距離(マイナス)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.M))
        {
            StartMovePlayer();
            MoveToTarget();
        }
       

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
            if ((Vector3.Dot(moveDir, (targetPosition - nowPos) ) < 0) && (Vector3.Distance(nowPos, targetPosition) > stopDistance))//プレイヤーの奥になったら
            {
                Debug.Log("奥に行った");
                //距離がstopDistance以下になったら
              //  if (Vector3.Distance(nowPos, targetPosition) > stopDistance)
               // {
                    moveDir = Vector3.zero;
                    isMoving = false;
                    Debug.Log("到着");
                    Debug.Log("nowPos: " + nowPos + " / targetPosition: " + targetPosition);
                    Debug.Log("距離: " + Vector3.Distance(nowPos, targetPosition));

                //}

            }
           
        }
    }

    void StartMovePlayer()
    {
        //ターゲットの位置を取得
        targetPosition = Hero.position;
        targetPosition.y = transform.position.y;//y軸は動かさない
        isMoving = true;
    }
    void MoveToTarget()
    {
        moveDir = (targetPosition - transform.position).normalized;
        moveDir.y = 0;//y軸は動かさない


    }
}
