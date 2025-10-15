using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
    NoGraThrow

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

    public bool isThrowBowling = false;

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
    float timer = 0;
    bool isBossThrow = false;
    //一度だけ発生するフラグ
    bool isInclination = false;
    bool isReset = false; // 回転をゼロに戻したかどうか
    Quaternion popInclination = Quaternion.AngleAxis(21.0f, Vector3.forward);
    Quaternion popRotation = Quaternion.AngleAxis(5, Vector3.up);
    Vector3 size = new Vector3(1.0f, 1.0f, 1.0f);
    float turtletimer = 0;//亀の滞在時間
    [SerializeField] GoalZone _respawn;//亀のリスポーン用


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
        
        _respawn = GameObject.Find("GoalZone").GetComponent<GoalZone>();
    }

    private void Update()
    {
        playerTransform = transform.root;//親オブジェクト(player)のTransformを取得

        throwDir = playerTransform.forward;//投げる向きはプレイヤーの向き
        throwDir.y = 0;
        //Boss用
        if (PlayerPrefs.GetInt("PointNum") == 5)
        {
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
       


        //y座標10以下で消去
        if (transform.position.y < 10)
        {
            Destroy(gameObject);
        }
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
            case BowlingNokonokoState.Boss://ボスの攻撃
                BossThrow(throwDirBoss);
                break;
            case BowlingNokonokoState.NoGraThrow://ボス戦での自分の投げ
                UpdateThrowZero(throwDir);
                break;


        }
        if (isBossThrow)
        {
            timer += Time.fixedDeltaTime;
            if (timer > 1.0f)
            {
                currentState = BowlingNokonokoState.pop;
                timer = 0;
                isBossThrow = false;
            }
        }

        //Debug_akasaki();
        if (turtletimer > 10.0f)//10fになったらリセット気味
        {
            //Debug.Log("時間です");
           // if (_respawn != null) return;
            _respawn.isNext = true;
            turtletimer = 0.0f;
            Destroy(gameObject);
          
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("地面に着地したよ");
            // Y方向を固定
            rb.constraints |= RigidbodyConstraints.FreezePositionY;
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            isColHit = true;
        }
        if (other.CompareTag("Boss"))
        {
            if (currentState == BowlingNokonokoState.NoGraThrow)
            {
                Destroy(gameObject);
            }
            
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
        //  Debug.Log(isReset);
        if (!isReset)
        {
            ResetRotate();
            //少し傾ける
            this.transform.rotation = popInclination;
            isReset = true;
            this.transform.localScale = size;
        }

        //Debug.Log("Pop中");
        //どのくらい回転するか
        Quaternion q = this.transform.rotation;
        //合成して自身に設定
        this.transform.rotation = popRotation * q;
        rb.useGravity = false;
        rb.isKinematic = true;
        isInclination = false;
        isThrowBowling = false;
    }
    public void QuitHoldTurtle() //離したとき
    {

        this.transform.SetParent(null);
        col.enabled = true;
        isReset = false;
        currentState = BowlingNokonokoState.pop;
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
        if (!isThrowBowling)
        {
            this.transform.SetParent(null);
            col.enabled = true;
            rb.useGravity = true; // 重力状態
            rb.isKinematic = false;
            rb.AddForce(throwDir.normalized * 20f, ForceMode.Impulse);
            isThrowBowling = true;
            turtletimer = 0;
        }
        turtletimer += Time.deltaTime;
    }
    // 投げる
    public void UpdateThrowZero(Vector3 direction)
    {

      //  Debug.Log("無重力です");
        col.enabled = true;
        rb.useGravity = false; // 無重力状態
        rb.isKinematic = false;
        if (!isThrowBowling)
        {
           
            this.transform.SetParent(null);
          
            rb.AddForce(throwDir.normalized * 20f, ForceMode.Impulse);
            isThrowBowling = true;
        }
    }
    public void BossThrow(Vector3 direction)
    {
      
        if (!isThrowBowling)
        {
            isBossThrow = true;
            col.enabled = true;
            rb.useGravity = true; // 重力状態
            rb.isKinematic = false;
            rb.AddForce(direction.normalized * 20f, ForceMode.Impulse);
            isThrowBowling = true;
        }
    }
    public void SetTarget(Transform target)
    {
        bossTransform = target;
    }
    private void ResetRotate()
    {
        this.transform.rotation = Quaternion.identity;
        isInclination = true;
        // Debug.Log("リセット");
    }
}
