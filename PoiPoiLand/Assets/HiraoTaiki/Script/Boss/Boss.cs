using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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


    //プレイヤー
    public Transform _player;




    //移動用
    Vector3 targetPosition;
    bool isMoving = false;

    Vector3 moveDir = Vector3.zero;

    public float moveSpeed = 90f;
    float stopDistance = 5.0f;

    //ワープ用
    Vector3 offset;//プレイヤーと敵の距離

    bool isRush = false;//突進中かどうか

    //魔法攻撃用
    AttackPos _attackPos;
    //魔法攻撃の連射防止用
    [SerializeField] private float shootInterval = 1.0f;//発射間隔//連射を防ぐ
    float shootTimer = 0.0f;

    //亀を投げる用

    //亀の連射防止用
    [SerializeField] private float throwInterval = 1.0f;//発射間隔//連射を防ぐ
    float throwTimer = 0.0f;

    //色違いゴースト用
    AttackGhost _attackGhost;
    AttackGhost _attackGhost2;
    //ゴーストの連射防止用
    [SerializeField] private float ghostInterval = 1.0f;//発射間隔//連射を防ぐ
    float ghostTimer = 0.0f;



    //ボスの移動
    Vector3 rightMove = new Vector3(2f, 0, 0);
    Vector3 leftMove = new Vector3(-2f, 0, 0);
    bool isRight = false;
    bool isMovingBoss = false;
    //移動の感覚
    float movetime = 0.5f;

    //bossのアニメーション用
    public BossFakeMove _mimic;


    // Start is called before the first frame update
    void Start()
    {
        _attackPos = GameObject.Find("AttackPos").GetComponent<AttackPos>();
        _attackGhost = GameObject.Find("RedGhostF").GetComponent<AttackGhost>();
        _attackGhost2 = GameObject.Find("RedGhostS").GetComponent<AttackGhost>();
        _mimic = GameObject.Find("Mimic").GetComponent<BossFakeMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRush)
        {
            LookAtPlayerQuaternion();
        }

        if (Input.GetKeyDown(KeyCode.M))//移動
        {
            isRush = true;
            StartMovePlayer();
            MoveToTarget();
        }

        if (Input.GetKeyDown(KeyCode.N))//ワープ
        {
            Warp();
        }

        if (Input.GetKeyDown(KeyCode.O))//移動中
        {
            movetime = 0;
            Debug.Log("移動中");
            isMovingBoss = true;
        }
        if (Input.GetKey(KeyCode.B))//魔法攻撃
        {
            if (shootTimer >= shootInterval)
            {
                shootTimer = 0.0f;
                _mimic.Action("Attack");
                StartCoroutine(WaitAndRelease(0.5f, "magic"));
            }

        }
        if (Input.GetKeyDown(KeyCode.P))//亀を投げる
        {
            if (throwTimer >= throwInterval)
            {
                throwTimer = 0.0f;
                _mimic.Action("Attack");
                StartCoroutine(WaitAndRelease(0.5f, "turtle"));
            }

        }
        if (Input.GetKeyDown(KeyCode.I))//色違いゴーストを出す
        {
            if (ghostTimer >= ghostInterval)
            {
                ghostTimer = 0.0f;
                _mimic.Action("Attack");
                StartCoroutine(WaitAndRelease(0.5f, "ghost"));
            }
        }



    }
    private void FixedUpdate()
    {


        shootTimer += Time.fixedDeltaTime;
        throwTimer += Time.fixedDeltaTime;
        ghostTimer += Time.fixedDeltaTime;
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
                Debug.Log("nowPos: " + nowPos + " / targetPosition: " + targetPosition);
                Debug.Log("距離: " + Vector3.Distance(nowPos, targetPosition));
                isRush = false;
                LookAtPlayerQuaternion();
                //}

            }

        }
        if (isMovingBoss)
        {
            movetime += Time.fixedDeltaTime;
            if (!isRight)
            {
                transform.position += rightMove * Time.fixedDeltaTime;

            }
            else
            {

                transform.position += leftMove * Time.fixedDeltaTime;

            }
            if (movetime > 4)
            {
                isRight = !isRight;
                movetime = 0;
                Debug.Log("移動");
                isMovingBoss = false;

            }
        }

    }

    void StartMovePlayer()
    {
        //ターゲットの位置を取得
        targetPosition = _player.position;
        targetPosition.y = transform.position.y;//y軸は動かさない
        isMoving = true;
    }
    void MoveToTarget()
    {
        moveDir = (targetPosition - transform.position).normalized;
        moveDir.y = 0;//y軸は動かさない

    }
    void Warp()//ワープ前にそこに移動するよっていうアニメーションを入れる
    {
        //マップの場所五個のうち、どれか一つに、ワープをするという仕様
        //----------------------------------------------------------------
        //今回のプレイヤーの周りにランダムにワープするというのは、なし
        //----------------------------------------------------------------

        offset = new Vector3(
            RandomExcept(),
            0,
            RandomExcept());
        Vector3 targetPos = _player.position;
        targetPos.y = transform.position.y;//y軸は動かさない
        transform.position = targetPos + offset;
        LookAtPlayerQuaternion();
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

    void LookAtPlayerQuaternion()
    {
        Vector3 dir = _player.position - transform.position;
        dir.y = 0;//y軸は動かさない
        if (dir.sqrMagnitude > 0.01f)//向く方向がある場合のみ
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir.normalized);
            transform.rotation = targetRotation;
        }
    }
    private IEnumerator WaitAndRelease(float delay, string name)
    {
        yield return new WaitForSeconds(delay);
        if (name == "magic")
        {
            _attackPos.MagicAttack();

        }
        if (name == "turtle")
        {
            _attackPos.TurtleAttack();

        }
        if (name == "ghost")
        {
            _attackGhost.GhostAttack();
            _attackGhost2.GhostAttack();

        }


    }

}
