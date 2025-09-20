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
    //キャラクターの向き

    public Vector3 moveDirection;
   


    //移動スピード
    float speed = 0.08f;

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
        //キャラクターの向き
        moveDirection = playerVelocity;
        moveDirection.y = 0;//y軸を0にして、水平面のみ回転するようにする
        moveDirection.Normalize();
        // Debug.Log($"_stateは{_state}です");
        Debug.Log($"movedirectionは{moveDirection}");
    

    }

    private void FixedUpdate()
    {
        //if (playerVelocity.magnitude == 0 && _state != PlayerState.Hold)
        //{
        //    _state = PlayerState.Idle;
        //}

        playerVelocity = Vector3.zero;
        
        if (!isGround)
        {
            transform.position += velocity * 2;//常にかかっている速度
        }
        
        //ジャンプの重力計算
        //verticalSpeed += gravity * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))//前移動
        {
            playerVelocity += Forward * speed;
           
            Debug.Log("前に移動しています");
        }
        if (Input.GetKey(KeyCode.S))//後ろ移動
        {
            playerVelocity += Back * speed;
           
        }
        if (Input.GetKey(KeyCode.A))//左移動
        {
            playerVelocity += Left * speed;
           
        }
        if (Input.GetKey(KeyCode.D))//右移動
        {
           
            playerVelocity += Right * speed;
           
        }

        if (Input.GetKey(KeyCode.Space) && isGround)//ジャンプ
        {
            Debug.Log("飛んでいます");
            rb.AddForce(JumpPower,ForceMode.Impulse);
            isGround = false;
            if (_state != PlayerState.Hold)
            {
                _animator.SetTrigger("TriggerJump");
            }
            if (_state == PlayerState.Hold)
            {
                _animator.SetTrigger("TriggerJumpHold");
            }
        }

        //持ち歩き
        if (playerVelocity.magnitude != 0 && _state == PlayerState.Hold)
        {
            _animator.SetBool("isWalkHold", true);
        }
        //ただの歩き
        if (playerVelocity.magnitude != 0 && _state != PlayerState.Hold)
        {
            _animator.SetBool("isWalk", true);
        }
        

        //Debug.Log($"{playerVelocity}");
        //止まっているとき
        if (playerVelocity.magnitude < 0.00001f) //=0で動かなくてこんだけ小さくしてもfalseにならない//急にこうなった//一旦放置//今は解決(疑似)
        {
           
            _animator.SetBool("isWalk", false);
            _animator.SetBool("isRun",false);
            _animator.SetBool("isWalkHold", false);
            _animator.SetBool("isRunHold", false);
        }


        //単位ベクトル化(斜め用)
        playerVelocity = playerVelocity.normalized;
        playerVelocity = playerVelocity * speed;


        //走る
        if (Input.GetKey(KeyCode.LeftShift) && playerVelocity.magnitude != 0)
        {
            Debug.Log("走る");
            playerVelocity *= 1.5f;
            if (_state != PlayerState.Hold)
            {
                _animator.SetBool("isWalk", false);
                _animator.SetBool("isRun", true);
            }
            if (_state == PlayerState.Hold)
            {
                _animator.SetBool("isWalkHold", false);
                _animator.SetBool("isRunHold", true);
            }

        }
        else
        {
            _animator.SetBool("isRun", false);
            _animator.SetBool("isRunHold", false);
        }
            //加える
            transform.position += playerVelocity;

        //キャラの回転
        if (moveDirection.sqrMagnitude > 0.0001f)//ベクトルの長さの2乗
        {
            Debug.Log("回転しています");
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(rotation);
            transform.rotation = rotationMatrix.rotation;
        }

        

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
