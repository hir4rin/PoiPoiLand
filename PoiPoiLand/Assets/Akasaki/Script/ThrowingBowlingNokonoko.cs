using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingBowlingNokonoko: MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded = false;//地面に着地したか
    private Vector3 moveDir;      //投げた後の進行方向
    public float speed = 4f;    //地面を進むスピード


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Throw(Vector3 direction,float force)
    {
        isGrounded = false;
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        moveDir = direction.normalized;
    }




    // Update is called once per frame
    void FixedUpdate()
    {
        if(isGrounded)
        {
            //地面に着地したら進行方向にまっすぐ移動
            rb.velocity = moveDir*speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 地面に着地タグ
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded=false;
            rb.useGravity=false;
            rb.isKinematic = false;
        }

        //壁に当たった場合のタグ
        if(collision.gameObject.CompareTag("Wall"))
        {
            //現在の速度ベクトル
            Vector3 inVelocity = rb.velocity;

            //衝突した面の法線ベクトル（AI参照）
            Vector3 normal = collision.contacts[0].normal;

            //反射ベクトルを計算
            moveDir = Vector3.Reflect(inVelocity.normalized, normal).normalized;

            //速度ベクトルを更新
            rb.velocity = moveDir * speed;

            Debug.Log("壁に当たったから反射！");
        }
    }








}
