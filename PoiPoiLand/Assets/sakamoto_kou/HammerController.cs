using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;



//ハンマーのステート
public enum HammerState
{
    pop, //出現中(Pop中)
    held, //持たれている
    thrown//投げられている
}

public class HammerController : MonoBehaviour
{
    /// <summary>
    /// x軸を軸にして毎秒6度、回転させるQuaternion
    /// </summary>
    Quaternion throwRotation = Quaternion.AngleAxis(20, Vector3.right);

    /// <summary>
    /// Pop中に毎秒5度、y軸を軸にして回転させるQuaternion
    /// </summary>
    Quaternion popRotation = Quaternion.AngleAxis(5, Vector3.up);
    /// <summary>
    /// ハンマーを傾ける角度
    /// </summary>
    Quaternion popInclination = Quaternion.AngleAxis(21.0f, Vector3.forward);
    //初期位置を設定
    private Vector3 startPos = new Vector3(0.0f,2.5f,0.0f);
    //一度だけ発生するフラグ
    bool isInclination = false;
    bool isThrow = false;

    GameObject _player;
    Player _playerScript;
    Collider col;
    Rigidbody rb;
    Vector3 throwDir;//投げる向き
    Transform playerTransform;//プレイヤーのTransform
    public bool isColHit = false;
  
    public HammerState currentState;

    // Start is called before the first frame update
    void Start()
    {
        //現在のポジションに初期位置を設定する
        transform.position = startPos;
        //少し傾ける
        this.transform.rotation = popInclination;
        //初期状態をポップにする
        currentState = HammerState.pop;

        _player = GameObject.Find("Player");
        _playerScript = _player.GetComponent<Player>();
        col = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
        //rb.isKinematic = true;        
    }

    private void Update()
    {
        playerTransform = transform.root;//親オブジェクト(player)のTransformを取得

        throwDir = playerTransform.forward + transform.up * 0.7f;//投げる向きはプレイヤーの向き＋少し上
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(currentState)
        {
            case HammerState.pop: //pop中
                UpdatePop();
                QuitHold();
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
        //敵に当たったら
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
        if (Input.GetKey(KeyCode.T))//SpaceをTに変更
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

        //Rを押したらハンマーを消す
        if(Input.GetKey(KeyCode.R))
        {
            Destroy(gameObject);
        }
    }

    void UpdatePop() //Pop中のUpdate
    {
        Debug.Log("Pop中");
        //現在の自身の回転の情報を取得する。
        Quaternion q = this.transform.rotation;
        //合成して自身に設定
        this.transform.rotation = popRotation * q;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void UpdateHold() //掴んでる状態
    {
        this.transform.SetParent(_player.transform, false);
        this.transform.localPosition = new Vector3(0.5f, 1, 0.5f);
        this.transform.localScale = new Vector3(40.0f, 40.0f, 40.0f);
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
    public void UpdateThrow()//なげたとき
    {
        //最初だけ回転を0にする
        if (!isInclination)
        {
            this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            isInclination = true;
        }

        if(!isThrow)
        {
            this.transform.SetParent(null);
            col.enabled = true;
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.AddForce(throwDir.normalized * 10f, ForceMode.Impulse);
            ////現在の自身の回転の情報を取得する。
            //Quaternion q = this.transform.rotation;
            ////合成して自身に設定
            //this.transform.rotation = popRotation * q;
            isThrow = true;
        }
    }
}
