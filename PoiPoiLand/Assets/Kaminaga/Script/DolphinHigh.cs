using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinHigh : MonoBehaviour
{

    private Animator m_animator;
    private float m_animTime;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_animTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_animTime);
        m_animTime += Time.deltaTime;
        if (m_animTime >= 3.0f)
        {
            if (!m_animator.GetBool("isMove"))
            {
                m_animator.SetBool("isMove", true);
            }
            else
            {
                m_animator.SetBool("isMove", false);
            }

            m_animTime = 0.0f;
        }
    }
}
