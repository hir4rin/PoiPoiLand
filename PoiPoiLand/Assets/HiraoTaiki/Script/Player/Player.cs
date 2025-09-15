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

    //最初のプレイヤーの状態
    public PlayerState _stete = PlayerState.Idle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log($"isGroundは{isGround}です");
    }

    private void FixedUpdate()
    {

       
        
        if (!isGround)
        {
            transform.position += velocity * 2;//常にかかっている速度
        }
        
        //ジャンプの重力計算
        //verticalSpeed += gravity * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Forward * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Back * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Left * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Right * speed;
        }
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            Debug.Log("飛んでいます");
            rb.AddForce(JumpPower,ForceMode.Impulse);
            isGround = false;
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
