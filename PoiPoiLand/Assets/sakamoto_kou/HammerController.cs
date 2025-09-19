using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HammerController : MonoBehaviour
{
    //�n���}�[�̃X�s�[�h
    private Vector3 speed = new Vector3(0.0f,10.0f,10.0f);
    //x�������ɂ��Ė��b6�x�A��]������Quaternion���쐬
    Quaternion throwRotation = Quaternion.AngleAxis(20, Vector3.right);
    Quaternion popRotation = Quaternion.AngleAxis(5, Vector3.up);
    //�n���}�[�̌X����p�x
    Quaternion popInclination = Quaternion.AngleAxis(21.0f, Vector3.forward);
    //�����ʒu��ݒ�
    private Vector3 startPos = new Vector3(0.0f,2.5f,0.0f);
    //�d�͂�ݒ�
    private Vector3 gravity = new Vector3(0.0f, -9.8f, 0.0f);
    //����
    private float time = 0.0f;
    //��x������������t���O
    bool isInclination = false;

    GameObject _player;
    Player _playerScript;
    Collider col;
    Rigidbody rb;
    Vector3 throwDir;//���������
    Transform playerTransform;//�v���C���[��Transform
    public bool isColHit = false;

    //�n���}�[�̃X�e�[�g
    public enum HammerState
    {
        pop,�@//�o����(Pop��)
        held, //������Ă���
        thrown//�������Ă���
    }
    HammerState currentState;

    public GameObject hammer;

    // Start is called before the first frame update
    void Start()
    {
        //���݂̃|�W�V�����ɏ����ʒu��ݒ肷��
        transform.position = startPos;
        //�����X����
        hammer.transform.rotation = popInclination;
        //������Ԃ��|�b�v�ɂ���
        currentState = HammerState.pop;

        _player = GameObject.Find("Player");
        _playerScript = _player.GetComponent<Player>();
        col = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerTransform = transform.root;//�e�I�u�W�F�N�g(player)��Transform���擾

        throwDir = playerTransform.forward + transform.up * 0.5f;//����������̓v���C���[�̌����{������
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(currentState)
        {
            case HammerState.pop: //pop��
                UpdatePop();
                break;
            case HammerState.held://������Ă�����
                UpdateHold();
                break;
            case HammerState.thrown://�Ȃ����Ă�����
                UpdateThrow();
                break;
        }

        Debug_sakamoto();

        //�������n�ʂ𒴂����ꍇ�͔j�󂷂�
        if (transform.position.y < 0.0f)
        {
            Destroy(gameObject);
            Debug.Log("�j��");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //�ǂɓ���������
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //�j�󂷂�
            Debug.Log("�G�ɓ�������");
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
        //�X�y�[�X����������X�e�[�g��thrown�ɂ���
        if (Input.GetKey(KeyCode.Space))
        {
            currentState = HammerState.thrown;
        }

        //H����������X�e�[�g��held�ɂ���
        if (Input.GetKey(KeyCode.H))
        {
            currentState = HammerState.held;
        }

        //P����������X�e�[�g��pop�ɂ���
        if (Input.GetKey(KeyCode.P))
        {
            currentState = HammerState.pop;
        }
    }

    void UpdatePop() //Pop����Update
    {
        Debug.Log("Pop��");
        //���݂̎��g�̉�]�̏����擾����B
        Quaternion q = hammer.transform.rotation;
        //�������Ď��g�ɐݒ�
        hammer.transform.rotation = popRotation * q;
    }
    
    //private void UpdateThrown() //�Ȃ����Ă����Ԃ�Update
    //{
    //    //�ŏ�������]��0�ɂ���
    //    if (!isInclination)
    //    {
    //        hammer.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
    //        isInclination = true;
    //    }

    //    Debug.Log("�Ȃ����Ă���");
    //    //���Ԃ��X�V
    //    time += Time.deltaTime;

    //    //���݂̎��g�̉�]�̏����擾����B
    //    Quaternion q = this.transform.rotation;
    //    //�������Ď��g�ɐݒ�
    //    this.transform.rotation = throwRotation * q;
    //}

    //private void UpdateHeld() //������Ă����Ԃ�Update
    //{
    //    Debug.Log("������Ă���");
    //    hammer.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
    //}

    public void UpdateHold() //�͂�ł���
    {
        this.transform.SetParent(_player.transform, false);
        this.transform.localPosition = new Vector3(0.5f, 1, 0.5f);
        col.enabled = false;
        rb.useGravity = false;
        rb.isKinematic = true;

        hammer.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
    }
    public void QuitHold() //�������Ƃ�
    {
        this.transform.SetParent(null);
        col.enabled = true;
        rb.useGravity = true;
        rb.isKinematic = false;

        currentState = HammerState.pop;
    }
    public void UpdateThrow()//�Ȃ����Ƃ�
    {
        //�ŏ�������]��0�ɂ���
        if (!isInclination)
        {
            hammer.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            isInclination = true;
        }

        this.transform.SetParent(null);
        col.enabled = true;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(throwDir.normalized * 10f, ForceMode.Impulse);
    }
}
