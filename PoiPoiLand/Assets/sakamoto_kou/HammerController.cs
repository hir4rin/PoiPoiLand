using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HammerController : MonoBehaviour
{
    //ハンマーのスピード
    private Vector3 speed = new Vector3(0.0f,10.0f,10.0f);
    //x軸を軸にして毎秒6度、回転させるQuaternionを作成
    Quaternion throwRotation = Quaternion.AngleAxis(20, Vector3.right);
    Quaternion popRotation = Quaternion.AngleAxis(5, Vector3.up);
    //ハンマーの傾ける角度
    Quaternion popInclination = Quaternion.AngleAxis(21.0f, Vector3.forward);
    //初期位置を設定
    private Vector3 startPos = new Vector3(0.0f,2.5f,0.0f);
    //重力を設定
    private Vector3 gravity = new Vector3(0.0f, -9.8f, 0.0f);
    //時間
    private float time = 0.0f;
    //一度だけ発生するフラグ
    bool isInclination = false;

    GameObject _player;
    Player _playerScript;
    Collider col;
    Rigidbody rb;
    Vector3 throwDir;//投げる向き
    Transform playerTransform;//プレイヤーのTransform
    public bool isColHit = false;

    //ハンマーのステート
    public enum HammerState
    {
        pop,　//出現中(Pop中)
        held, //持たれている
        thrown//投げられている
    }
    HammerState currentState;

    public GameObject hammer;

    // Start is called before the first frame update
    void Start()
    {
        //現在のポジションに初期位置を設定する
        transform.position = startPos;
        //少し傾ける
        hammer.transform.rotation = popInclination;
        //初期状態をポップにする
        currentState = HammerState.pop;

        _player = GameObject.Find("Player");
        _playerScript = _player.GetComponent<Player>();
        col = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerTransform = transform.root;//親オブジェクト(player)のTransformを取得

        throwDir = playerTransform.forward + transform.up * 0.5f;//投げる向きはプレイヤーの向き＋少し上
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(currentState)
        {
            case HammerState.pop: //pop中
                UpdatePop();
                break;
            case HammerState.held://持たれている状態
                UpdateHold();
                break;
            case HammerState.thrown://なげられている状態
                UpdateThrow();
                break;
        }

        Debug_sakamoto();

        //高さが地面を超えた場合は破壊する
        if (transform.position.y < 0.0f)
        {
            Destroy(gameObject);
            Debug.Log("破壊");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //壁に当たったら
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //破壊する
            Debug.Log("敵に当たった");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Ground"))
        {
            isColHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ground"))
        {
            isColHit = false;
        }
    }

    void Debug_sakamoto()
    {
        //スペースを押したらステートをthrownにする
        if (Input.GetKey(KeyCode.Space))
        {
            currentState = HammerState.thrown;
        }

        //Hを押したらステートをheldにする
        if (Input.GetKey(KeyCode.H))
        {
            currentState = HammerState.held;
        }

        //Pを押したらステートをpopにする
        if (Input.GetKey(KeyCode.P))
        {
            currentState = HammerState.pop;
        }
    }

    void UpdatePop() //Pop中のUpdate
    {
        Debug.Log("Pop中");
        //現在の自身の回転の情報を取得する。
        Quaternion q = hammer.transform.rotation;
        //合成して自身に設定
        hammer.transform.rotation = popRotation * q;
    }
    
    //private void UpdateThrown() //なげられている状態のUpdate
    //{
    //    //最初だけ回転を0にする
    //    if (!isInclination)
    //    {
    //        hammer.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
    //        isInclination = true;
    //    }

    //    Debug.Log("なげられている");
    //    //時間を更新
    //    time += Time.deltaTime;

    //    //現在の自身の回転の情報を取得する。
    //    Quaternion q = this.transform.rotation;
    //    //合成して自身に設定
    //    this.transform.rotation = throwRotation * q;
    //}

    //private void UpdateHeld() //持たれている状態のUpdate
    //{
    //    Debug.Log("持たれている");
    //    hammer.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
    //}

    public void UpdateHold() //掴んでる状態
    {
        this.transform.SetParent(_player.transform, false);
        this.transform.localPosition = new Vector3(0.5f, 1, 0.5f);
        col.enabled = false;
        rb.useGravity = false;
        rb.isKinematic = true;

        hammer.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
    }
    public void QuitHold() //離したとき
    {
        this.transform.SetParent(null);
        col.enabled = true;
        rb.useGravity = true;
        rb.isKinematic = false;

        currentState = HammerState.pop;
    }
    public void UpdateThrow()//なげたとき
    {
        //最初だけ回転を0にする
        if (!isInclination)
        {
            hammer.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            isInclination = true;
        }

        this.transform.SetParent(null);
        col.enabled = true;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(throwDir.normalized * 10f, ForceMode.Impulse);
    }
}
