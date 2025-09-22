using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    //----------------------------------------------------------------
    //実装する内容
    //1.動く瞬間に敵の座標をとって一定量動かす(動く時間はたまに)                            (完了)
    //2.ワープする(プレイヤーの周り二ワープ→攻撃)                                           (完了)
    ///3普通の玉攻撃(1と同じようにするか、ホーミングにしてy座標を下げてよけさせる)          　
    // 4.甲羅やゴースト(色違いゴーストを出す(場所はランダム))
    //
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
    Vector3 offset;//プレイヤーと敵の距離
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.M))//移動
        {
            StartMovePlayer();
            MoveToTarget();
        }

        if (Input.GetKeyDown(KeyCode.N))//ワープ
        {
            Warp();
        }
        if (Input.GetKey(KeyCode.B))//魔法攻撃
        {

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
    void Warp()
    {
        offset = new Vector3(
            RandomExcept(),
            0,
            RandomExcept());
        Vector3 targetPos = Hero.position;
        targetPos.y = transform.position.y;//y軸は動かさない
        transform.position = targetPos + offset;
    }

    float RandomExcept()
    {
        float val = 0f;
        //
        if (Random.value > 0.5f)
        {
            val = Random.Range(2f, 1f);
        }
        else
        {
            val = Random.Range(-2f, -1f);
        }
        return val;
    }

}
