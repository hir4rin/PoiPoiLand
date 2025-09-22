using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum NokonokoState
{
    pop, //出現中(Pop中)
    held, //持たれている
    thrown,//投げられた瞬間
    move//移動中
}

public class NokonokoController : MonoBehaviour
{
    public float speed = 0.1f;
    private Rigidbody rb;
    private Vector3 startPos = new Vector3(4.18f, 2.0f, 0.0f);

    bool isThrow = false;

    GameObject _player;
    Player _playerScript;
    Collider col;
    Vector3 throwDir;//投げる向き
    Transform playerTransform;//プレイヤーのTransform
    public bool isColHit = false;
    public NokonokoState currentState;

    // Start is called before the first frame update
    void Start()
    {
        // 現在の位置を開始位置に
        transform.position = startPos;

        //初期状態をポップにする
        currentState = NokonokoState.pop;

        _player = GameObject.Find("Player");
        _playerScript = _player.GetComponent<Player>();
        col = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerTransform = transform.root;//親オブジェクト(player)のTransformを取得

        throwDir = playerTransform.forward;//投げる向きはプレイヤーの向き＋少し上
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case NokonokoState.pop: //pop中
                UpdatePop();
                QuitHold();
                break;
            case NokonokoState.held://持たれている状態
                UpdateHold();
                break;
            case NokonokoState.thrown://なげられている状態
                UpdateThrow(throwDir);
                break;
        }

        Debug_akasaki();
    }

    

    // 壁にぶつかったとき反射
    //private void OnCollisionEnter(Collision collision)
    //{
    //    // 現在の進行方向
    //    Vector3 inDirection = rb.velocity;

    //    // 壁の法線ベクトル
    //    Vector3 normal = collision.contacts[0].normal;

    //    // 反射ベクトルを計算
    //    Vector3 reflectDir = Vector3.Reflect(inDirection, normal);


    //    // 速度を反射方向に更新
    //    //rb.velocity = reflectDir.normalized*speed;
    //}
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

    void Debug_akasaki()
    {
        //スペースを押したらステートをthrownにする
        if (Input.GetKey(KeyCode.T))//SpaceをTに変更
        {
            currentState = NokonokoState.thrown;
        }

        //Hを押したらステートをheldにする
        if (Input.GetKey(KeyCode.H))
        {
            currentState = NokonokoState.held;
        }

        //Pを押したらステートをpopにする
        if (Input.GetKey(KeyCode.P))
        {
            currentState = NokonokoState.pop;
        }

        //Rを押したらハンマーを消す
        if (Input.GetKey(KeyCode.R))
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
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void UpdateHold() //掴んでる状態
    {
        Debug.Log("つかみぎゃん");
        this.transform.SetParent(_player.transform, false);
        this.transform.localPosition = new Vector3(0.5f, 1, 0.5f);
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
