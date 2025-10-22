using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinController : MonoBehaviour
{
    private Animator m_animator;
    private float m_animTime;
    public float interval = 6.0f;   // 周期
    private float offset;           // 個体ごとのズレ
    private bool lastState = false;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_animTime = 0.0f;
        offset = Random.Range(0f,interval);//開始タイミングをランダムにずらす
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateAnimState();
    }

    void UpdateAnimState()
    {
        //絶対時間 + 個体のオフセットを基準に周期判定
        float time = Time.time + offset;
        int phase = Mathf.FloorToInt(time / interval);
        bool isMove = (phase % 2 == 1);

        if (isMove != lastState)
        {
            m_animator.SetBool("isMove", isMove);
            lastState = isMove;
        }
    }
}
