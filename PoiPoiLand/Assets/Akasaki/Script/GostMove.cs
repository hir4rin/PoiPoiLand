using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GostMove : MonoBehaviour
{
    Vector3 startPos; // �����ʒu
    Vector3 pos; //�X�V���ꂽ�ʒu

    Transform playerTr; //�v���C���[�̃g�����X�t�H�[��
    [SerializeField] float speed = 2; // �G�̓����X�s�[�h
    [SerializeField] float followRange = 2.0f; // �Ǐ]����
    [SerializeField] float floatHeight = 0.5f;
    [SerializeField] float floatSpeed = 2.0f;
    [SerializeField] float wanderRange = 1.0f;

    // Start is called before the first frame update
    private void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTr.position);

        // �v���C���[�����͈͓��ɓ������ꍇ
        if(distance < followRange)
        {
            //�v���C���[�Ɍ������Đi��
            transform.position = Vector3.MoveTowards(
                transform.position,
                playerTr.position,
                //new Vector3(playerTr.position.x, playerTr.position.y),
                speed * Time.deltaTime);

            pos = transform.position;
            Debug.Log("�����Ă������");
        }

        else
        {
            //�H����ۂ�����
            float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            float xOffset = Mathf.Sin(Time.time * (floatSpeed * 0.5f)) * wanderRange;

            transform.position = new Vector3(
                pos.x + xOffset,
                pos.y + yOffset,
                pos.z);
            Debug.Log("�N�����Ȃ����");
        }

    }
}
