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

    // Start is called before the first frame update
    void Start()
    {
        //���݂̃|�W�V�����ɏ����ʒu��ݒ肷��
        transform.position = startPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //���Ԃ��X�V
        time += Time.deltaTime;

        //���݂̎��g�̉�]�̏����擾����B
        Quaternion q = this.transform.rotation;
        //�������Ď��g�ɐݒ�
        this.transform.rotation =rotation * q;
        //���ł����Ƃ��̋O����������ɂ���
        Vector3 pos = startPos
                      + speed * time
                      + 0.5f * gravity * (time * time);

        transform.position = pos;
    }
}
