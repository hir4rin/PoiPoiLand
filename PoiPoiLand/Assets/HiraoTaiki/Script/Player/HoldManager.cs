using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldManager : MonoBehaviour
{

    public bool isColHit = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (isColHit)
        //{

        //    if (Input.GetKeyDown(KeyCode.J))
        //    {
        //        Debug.Log("�����܂���");

        //    }
        //}
    }

    /// <summary>
    /// ���͈͓̔��Ńv���C���[���{�^�����������Ƃ��Ɏ��Ă�悤�ɂ���
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground"))
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
}
