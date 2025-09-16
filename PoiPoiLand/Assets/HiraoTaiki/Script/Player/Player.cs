using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//プレイヤーの状態
public enum PlayerState
{
    Idle,
    Walk,
    Run,
    Jump,
    Hold,
    Throw,
    Damage,
    Dead
}
public class Player : MonoBehaviour
{
   //プレイヤーの座標
   Vector3 playerPos = Vector3.zero;
    //プレイヤーに加える方向
    Vector3 playerVelocity = Vector3.zero;


    //移動方向
    Vector3 Forward = new Vector3(0,0,1);
    Vector3 Back = new Vector3(0,0,-1);
    Vector3 Right = new Vector3(1,0,0);
    Vector3 Left = new Vector3(-1,0,0);
    Vector3 JumpPower = new Vector3(0,15,0);



    //移動スピード
    float speed = 0.05f;

    //ジャンプのちから
    float jumpForce = 1.5f;
    bool isGround = false;//地面についているかどうか
    Rigidbody rb;

    Vector3 verticalSpeed;
    Vector3 velocity;

    //アニメーション
    public Animator _animator;

    //最初のプレイヤーの状態
    public PlayerState _state = PlayerState.Idle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"_stateは{_state}です");
    }

    private void FixedUpdate()
    {
        //_state = PlayerState.Idle;
        playerVelocity = Vector3.zero;
        
        if (!isGround)
        {
            transform.position += velocity * 2;//常にかかっている速度
        }
        
        //ジャンプの重力計算
        //verticalSpeed += gravity * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            playerVelocity += Forward * speed;
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerVelocity += Back * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerVelocity += Left * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerVelocity += Right * speed;
        }
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            Debug.Log("飛んでいます");
            rb.AddForce(JumpPower,ForceMode.Impulse);
            isGround = false;
        }
        //単位ベクトル化(斜め用)

        //加える
        transform.position += playerVelocity;

        //if (_state == PlayerState.Walk)
        //{
        //    _animator.SetTrigger("isWalk");
        //}
       
       
    }

    //地面に触れたら着地と判定
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            velocity.y = 0;
        }
    }
}
