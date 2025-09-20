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
    //�L�����N�^�[�̌���

    public Vector3 moveDirection;
   


    //�ړ��X�s�[�h
    float speed = 0.08f;

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
        //�L�����N�^�[�̌���
        moveDirection = playerVelocity;
        moveDirection.y = 0;//y����0�ɂ��āA�����ʂ̂݉�]����悤�ɂ���
        moveDirection.Normalize();
        // Debug.Log($"_state��{_state}�ł�");
        Debug.Log($"movedirection��{moveDirection}");
    

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
            transform.position += velocity * 2;//��ɂ������Ă��鑬�x
        }
        
        //�W�����v�̏d�͌v�Z
        //verticalSpeed += gravity * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))//�O�ړ�
        {
            playerVelocity += Forward * speed;
           
            Debug.Log("�O�Ɉړ����Ă��܂�");
        }
        if (Input.GetKey(KeyCode.S))//���ړ�
        {
            playerVelocity += Back * speed;
           
        }
        if (Input.GetKey(KeyCode.A))//���ړ�
        {
            playerVelocity += Left * speed;
           
        }
        if (Input.GetKey(KeyCode.D))//�E�ړ�
        {
           
            playerVelocity += Right * speed;
           
        }

        if (Input.GetKey(KeyCode.Space) && isGround)//�W�����v
        {
            Debug.Log("���ł��܂�");
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

        //��������
        if (playerVelocity.magnitude != 0 && _state == PlayerState.Hold)
        {
            _animator.SetBool("isWalkHold", true);
        }
        //�����̕���
        if (playerVelocity.magnitude != 0 && _state != PlayerState.Hold)
        {
            _animator.SetBool("isWalk", true);
        }
        

        //Debug.Log($"{playerVelocity}");
        //�~�܂��Ă���Ƃ�
        if (playerVelocity.magnitude < 0.00001f) //=0�œ����Ȃ��Ă��񂾂����������Ă�false�ɂȂ�Ȃ�//�}�ɂ����Ȃ���//��U���u//���͉���(�^��)
        {
           
            _animator.SetBool("isWalk", false);
            _animator.SetBool("isRun",false);
            _animator.SetBool("isWalkHold", false);
            _animator.SetBool("isRunHold", false);
        }


        //�P�ʃx�N�g����(�΂ߗp)
        playerVelocity = playerVelocity.normalized;
        playerVelocity = playerVelocity * speed;


        //����
        if (Input.GetKey(KeyCode.LeftShift) && playerVelocity.magnitude != 0)
        {
            Debug.Log("����");
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
            //������
            transform.position += playerVelocity;

        //�L�����̉�]
        if (moveDirection.sqrMagnitude > 0.0001f)//�x�N�g���̒�����2��
        {
            Debug.Log("��]���Ă��܂�");
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(rotation);
            transform.rotation = rotationMatrix.rotation;
        }

        

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
