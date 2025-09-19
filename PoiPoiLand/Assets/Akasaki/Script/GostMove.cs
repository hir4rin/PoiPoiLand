using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GostMove : MonoBehaviour
{
    Vector3 startPos; // 初期位置
    Vector3 pos; //更新された位置

    Transform playerTr; //プレイヤーのトランスフォーム
    [SerializeField] float speed = 2; // 敵の動くスピード
    [SerializeField] float followRange = 2.0f; // 追従距離
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

        // プレイヤーが一定範囲内に入った場合
        if(distance < followRange)
        {
            //プレイヤーに向かって進む
            transform.position = Vector3.MoveTowards(
                transform.position,
                playerTr.position,
                //new Vector3(playerTr.position.x, playerTr.position.y),
                speed * Time.deltaTime);

            pos = transform.position;
            Debug.Log("入ってきたやん");
        }

        else
        {
            //幽霊っぽい挙動
            float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            float xOffset = Mathf.Sin(Time.time * (floatSpeed * 0.5f)) * wanderRange;

            transform.position = new Vector3(
                pos.x + xOffset,
                pos.y + yOffset,
                pos.z);
            Debug.Log("誰も来ないやん");
        }

    }
}
