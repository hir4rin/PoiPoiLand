using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    //�n���}�[�̃X�s�[�h
    private Vector3 speed = new Vector3(0.0f,10.0f,10.0f);
    //x�������ɂ��Ė��b6�x�A��]������Quaternion���쐬
    Quaternion rotation = Quaternion.AngleAxis(20, Vector3.right);
    //�����ʒu��ݒ�
    private Vector3 startPos = new Vector3(0.0f,5.0f,0.0f);
    //�d�͂�ݒ�
    private Vector3 gravity = new Vector3(0.0f, -9.8f, 0.0f);
    //����
    private float time = 0.0f;

    //�n���}�[�̃X�e�[�g
    public enum HammerState
    {
        pop,�@//�o����(Pop��)
        held, //������Ă���
        thrown//�������Ă���
    }

    HammerState currentState = HammerState.pop;

    // Start is called before the first frame update
    void Start()
    {
        //���݂̃|�W�V�����ɏ����ʒu��ݒ肷��
        transform.position = startPos;
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
                UpdateHeld();
                break;
            case HammerState.thrown://�Ȃ����Ă�����
                UpdateThrown();
                break;
        }
        
        //�X�y�[�X����������X�e�[�g��thrown�ɂ���
        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentState = HammerState.thrown;
        }

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

    private void UpdatePop() //Pop����Update
    {
       
    }
    
    private void UpdateThrown() //�Ȃ����Ă����Ԃ�Update
    {
        Debug.Log("�Ȃ����Ă���");
        //���Ԃ��X�V
        time += Time.deltaTime;

        //���݂̎��g�̉�]�̏����擾����B
        Quaternion q = this.transform.rotation;
        //�������Ď��g�ɐݒ�
        this.transform.rotation = rotation * q;
        //���ł����Ƃ��̋O����������ɂ���
        Vector3 pos = startPos
                      + speed * time
                      + 0.5f * gravity * (time * time);

        transform.position = pos;
    }

    private void UpdateHeld() //������Ă����Ԃ�Update
    {

    }
}
