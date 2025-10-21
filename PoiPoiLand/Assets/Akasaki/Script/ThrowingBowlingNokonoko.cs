using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowingBowlingNokonoko: MonoBehaviour
{
    private Rigidbody rb;
    private bool isGrounded = false;//地面に着地したか
    private Vector3 moveDir;      //投げた後の進行方向
    public float speed = 20.0f;    //地面を進むスピード
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    //public void Throw(Vector3 direction,float force)
    //{
    //    isGrounded = false;
    //    rb.isKinematic = false;
    //    rb.useGravity = true;
    //    rb.AddForce(direction.normalized * force, ForceMode.Impulse);
    //    moveDir = direction.normalized*speed;
    //}




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
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("地面に着地したよ");
            // Y方向を固定
            rb.constraints |= RigidbodyConstraints.FreezePositionY;
        }
        //// 地面に着地タグ
        //if(collision.gameObject.CompareTag("Ground"))
        //{
        //    isGrounded=false;
        //    rb.useGravity=false;
        //    rb.isKinematic = false;
        //}

        //壁に当たった場合のタグ
        if (collision.gameObject.CompareTag("Wall"))
        {
            SoundManager.Instance.PlaySE(audioSource);
            ////現在の速度ベクトル
            //Vector3 inVelocity = rb.velocity;

            ////衝突した面の法線ベクトル（AI参照）
            //if (Mathf.Abs(collision.transform.right.x) > 0.5f)
            //{
            //    inVelocity.x = -inVelocity.x;
            //}
            //else if (Mathf.Abs(collision.transform.forward.z) > 0.5)
            //{
            //    inVelocity.z = -inVelocity.z;
            //}
            ////反射ベクトルを計算
            ////moveDir = Vector3.Reflect(inVelocity, normal);
            //Debug.Log($"moveDir  = {inVelocity}");
            ////速度ベクトルを更新
            //rb.velocity = inVelocity;

            Debug.Log("壁に当たったから反射！");
        }
    }
    // 床から離れたとき
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Y固定を解除（他の制約は残す）
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        }
    }








}
