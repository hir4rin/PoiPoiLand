using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// カメラ補正のオブジェクト用スクリプト
/// </summary>
public class CameraFollowCollection : MonoBehaviour
{

    [SerializeField] private Transform currentPlayerPos; // プレイヤーの座標を取得
    [SerializeField] private Player player;
    private float yThreshold; // カメラの補正を行う閾値


    float lastPlayerY; // 前のフレームでのプレイヤーのy座標

    // Start is called before the first frame update
    void Start()
    {
        lastPlayerY = currentPlayerPos.position.y;
        yThreshold = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = currentPlayerPos.position; // 現在のプレイヤーの座標

        // y軸の値が閾値未満なら、y座標を更新しない
        // (今のフレームのY座標 - 前回のフレームのY座標)の大きさが1.0未満なら更新しない
        if (player.isGround)
        {
            if (Mathf.Abs(targetPos.y - lastPlayerY) < yThreshold)
            {
                targetPos.y = lastPlayerY;
            }
            else
            {
                lastPlayerY = targetPos.y;
            }

        }
        else
        {
            lastPlayerY = targetPos.y;
        }

        // プレイヤーの位置に更新
        transform.position = targetPos;
    }
}
