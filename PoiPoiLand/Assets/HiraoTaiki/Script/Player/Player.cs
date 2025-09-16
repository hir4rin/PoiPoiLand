using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�v���C���[�̏��
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
   //�v���C���[�̍��W
   Vector3 playerPos = Vector3.zero;
    //�v���C���[�ɉ��������
    Vector3 playerVelocity = Vector3.zero;


    //�ړ�����
    Vector3 Forward = new Vector3(0,0,1);
    Vector3 Back = new Vector3(0,0,-1);
    Vector3 Right = new Vector3(1,0,0);
    Vector3 Left = new Vector3(-1,0,0);
    Vector3 JumpPower = new Vector3(0,15,0);



    //�ړ��X�s�[�h
    float speed = 0.05f;

    //�W�����v�̂�����
    float jumpForce = 1.5f;
    bool isGround = false;//�n�ʂɂ��Ă��邩�ǂ���
    Rigidbody rb;

    Vector3 verticalSpeed;
    Vector3 velocity;

    //�A�j���[�V����
    public Animator _animator;

    //�ŏ��̃v���C���[�̏��
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
        Debug.Log($"_state��{_state}�ł�");
    }

    private void FixedUpdate()
    {
        //_state = PlayerState.Idle;
        playerVelocity = Vector3.zero;
        
        if (!isGround)
        {
            transform.position += velocity * 2;//��ɂ������Ă��鑬�x
        }
        
        //�W�����v�̏d�͌v�Z
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
            Debug.Log("���ł��܂�");
            rb.AddForce(JumpPower,ForceMode.Impulse);
            isGround = false;
        }
        //�P�ʃx�N�g����(�΂ߗp)

        //������
        transform.position += playerVelocity;

        //if (_state == PlayerState.Walk)
        //{
        //    _animator.SetTrigger("isWalk");
        //}
       
       
    }

    //�n�ʂɐG�ꂽ�璅�n�Ɣ���
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            velocity.y = 0;
        }
    }
}
