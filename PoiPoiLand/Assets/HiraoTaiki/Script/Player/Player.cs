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
    Vector3 Forward;
    Vector3 Back;
    Vector3 Right;
    Vector3 Left;
    //CameraSwitcher _cameraSwitch;
    Vector3 JumpPower = new Vector3(0, 15, 0);
    //キャラクターの向き
    public Vector3 moveDirection; //キャラクターの向き



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

    Warp_Controller _checkPoint;


    //最初のプレイヤーの状態
    public PlayerState _state = PlayerState.Idle;
    // Start is called before the first frame update
    void Start()
    {
        //移動方向
        //Forward = Camera.main.transform.forward.normalized;
        //Back = Camera.main.transform.forward.normalized * -1;
        //Right = Camera.main.transform.right.normalized;
        //Left = Camera.main.transform.right.normalized * -1;
        //_cameraSwitch = GameObject.Find("CameraSwitch").GetComponent<CameraSwitcher>();
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _checkPoint = GameObject.Find("CheckPoint").GetComponent<Warp_Controller>();

    }

    // Update is called once per frame
    void Update()
    {
        Transform camTransform = Camera.main.transform;
        Forward = camTransform.forward.normalized;
        Forward.y = 0;
        Back = -Forward;

        Right = camTransform.right.normalized;
        Right.y = 0;
        Left = -Right;

        //キャラクターの向き
        moveDirection = playerVelocity;
        moveDirection.y = 0;//y軸を0にして、水平面のみ回転するようにする
        moveDirection.Normalize();
        // Debug.Log($"_stateは{_state}です");
        //Debug.Log($"movedirectionは{moveDirection}");


    }

    private void FixedUpdate()
    {
        //if (playerVelocity.magnitude == 0 && _state != PlayerState.Hold)
        //{
        //    _state = PlayerState.Idle;
        //}
        Debug.Log(Forward);
        //Debug.Log(Back);
        //Debug.Log(Left);
        //Debug.Log(Right);
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
            rb.AddForce(JumpPower, ForceMode.Impulse);
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
            _animator.SetBool("isRun", false);
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
            //Debug.Log("回転しています");
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(rotation);
            transform.rotation = rotationMatrix.rotation;
        }

        if (transform.position.y < 9)
        {
            Death();
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
    public void Death()
    {
        //ここでチェックポイントによって座標を変える
        Debug.Log($"{PlayerPrefs.GetInt("PointNum")}");
        switch (PlayerPrefs.GetInt("PointNum"))
        {
            case 0:
                this.transform.position = _checkPoint.StartPos;
                break;
            case 1:
                this.transform.position = _checkPoint.warpToFirstStage;
                break;
            case 2:
                this.transform.position = _checkPoint.warpToMapFirst;
                break;
            case 3:
                this.transform.position = _checkPoint.warpToSecondStage;
                break;
            case 4:
                this.transform.position = _checkPoint.warpToMapSecond;
                break;
            case 5:
                this.transform.position = _checkPoint.warpToThirdStage;
                break;
            case 6:
                this.transform.position = _checkPoint.warpToMapThird;
                break;
            default:
                break;
        }
    }
}
