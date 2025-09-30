using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum BowlingNokonokoState
{
    pop, //出現中(Pop中)
    held, //持たれている
    thrownzerogravity, //投げられた瞬間、無重力
    throwngravity,//重力付き
    move,//移動中
    Boss,//ボス用

}

//平尾------------------------------
//：動いているとき、回転する見た目をすること
//：動きが止まったら、pop状態に戻ること
//:できたら、回転とスピードを同じくらいの量に変わるようにすること
//：ボス用とプレイヤー用に分けること
//----------------------------------

public class BowlingNokonokoController : MonoBehaviour
{
    private Rigidbody rb;
    //private bool isGrounded = false;//地面に着地したか
    //private Vector3 moveDir;      //投げた後の進行方向
    //public float speed = 0.1f;    //地面を進むスピード
    private Vector3 startPos = new Vector3(4.18f, 2.0f, 0.0f);

    bool isThrow = false;

    GameObject _player;
    Player _playerScript;
    Collider col;
    Vector3 throwDir;//投げる向き
    Transform playerTransform;//プレイヤーのTransform
    public bool isColHit = false;
    public BowlingNokonokoState currentState;

    //Boss用
    public GameObject boss;
    Vector3 throwDirBoss;//投げる向き
    Transform bossTransform;//ボスのTransform

    // Start is called before the first frame update
    void Start()
    {
        // 現在の位置を開始位置に
        // transform.position = startPos;

        _player = GameObject.Find("Player");
        _playerScript = _player.GetComponent<Player>();
        col = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();
    
        if (currentState == 0) // enum の初期値 (pop) のときだけ
        {
            //初期状態をポップにする
            currentState = BowlingNokonokoState.pop;
            rb.useGravity = false;
            rb.isKinematic = true;
        }
      
        boss = GameObject.Find("Boss");
    }

    private void Update()
    {
        playerTransform = transform.root;//親オブジェクト(player)のTransformを取得

        throwDir = playerTransform.forward;//投げる向きはプレイヤーの向き＋少し上

        //Boss用
        if (boss == null)
        {
            GameObject obj = GameObject.FindWithTag("Boss"); // ボスタグのオブジェクトを探す
            if (obj != null)
            {
                boss = obj;
            }
        }
        bossTransform = boss.transform;
        throwDirBoss = bossTransform.forward + Vector3.down * 0.5f;//投げる向きはボスの向き+少し下
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case BowlingNokonokoState.pop: //pop中
                UpdatePop();
                QuitHold();
                break;
            case BowlingNokonokoState.held://持たれている状態
                UpdateHold();
                break;
            case BowlingNokonokoState.thrownzerogravity://なげられている状態
                UpdateThrow(throwDir);
                break;
            case BowlingNokonokoState.Boss:
                BossThrow(throwDirBoss);
                break;

        }

        //Debug_akasaki();
    }



    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            isColHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            isColHit = false;
        }
    }

    //void Debug_akasaki()
    //{
    //    //スペースを押したらステートをthrownにする
    //    if (Input.GetKey(KeyCode.T))//SpaceをTに変更
    //    {
    //        currentState = BowlingNokonokoState.thrownzerogravity;
    //    }

    //    //Hを押したらステートをheldにする
    //    if (Input.GetKey(KeyCode.H))
    //    {
    //        currentState = BowlingNokonokoState.held;
    //    }

    //    //Pを押したらステートをpopにする
    //    if (Input.GetKey(KeyCode.P))
    //    {
    //        currentState = BowlingNokonokoState.pop;
    //    }

    //    //Rを押したらハンマーを消す
    //    if (Input.GetKey(KeyCode.R))
    //    {
    //        Destroy(gameObject);
    //    }
    //}


    void UpdatePop() //Pop中のUpdate
    {
       // Debug.Log("Pop中");
        //現在の自身の回転の情報を取得する。
        Quaternion q = this.transform.rotation;
        //合成して自身に設定
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void UpdateHold() //掴んでる状態
    {
        //Debug.Log("つかみぎゃん");
        this.transform.SetParent(_player.transform, false);
        this.transform.localPosition = new Vector3(0.5f, 0.2f, 0.5f);
        this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        this.transform.rotation = Quaternion.Euler(30.0f, 90.0f, 0.0f);
        col.enabled = false;
        rb.useGravity = false;
        rb.isKinematic = true;

        this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

    }
    public void QuitHold() //離したとき
    {
        this.transform.SetParent(null);
        col.enabled = true;
    }

    // 投げる
    public void UpdateThrow(Vector3 direction)
    {
        if (!isThrow)
        {
            


            this.transform.SetParent(null);
            col.enabled = true;
            rb.useGravity = true; // 重力状態
            rb.isKinematic = false;
            rb.AddForce(throwDir.normalized * 20f, ForceMode.Impulse);
            ////現在の自身の回転の情報を取得する。
            //Quaternion q = this.transform.rotation;
            ////合成して自身に設定
            //this.transform.rotation = popRotation * q;
            isThrow = true;
        }
    }
    public void BossThrow(Vector3 direction)
    {
      
        if (!isThrow)
        {
            col.enabled = true;
            rb.useGravity = true; // 重力状態
            rb.isKinematic = false;
            rb.AddForce(direction.normalized * 20f, ForceMode.Impulse);
            isThrow = true;
        }
    }
    public void SetTarget(Transform target)
    {
        bossTransform = target;
    }
}
