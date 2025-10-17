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

    private Vector3 lastPlayerPos; // 前のフレームでのプレイヤーの座標
    [SerializeField] private Vector3 threshold; // カメラの補正を行う閾値

    // Start is called before the first frame update
    void Start()
    {
        lastPlayerPos = currentPlayerPos.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = currentPlayerPos.position; // 現在のプレイヤーの座標

        // y軸の値が閾値未満なら、y座標を更新しない
        // (今のフレームのY座標 - 前回のフレームのY座標)の大きさが1.0未満なら更新しない
        if (player.isGround)
        {
            //Debug.Log("xの変化量" + Mathf.Abs(targetPos.x - lastPlayerPos.x).ToString());
            if (Mathf.Abs(targetPos.x - lastPlayerPos.x) < threshold.x)
            {
                targetPos.x = lastPlayerPos.x;
            }
            else
            {
                lastPlayerPos.x = targetPos.x;
            }

            if (Mathf.Abs(targetPos.y - lastPlayerPos.y) < threshold.y)
            {
                targetPos.y = lastPlayerPos.y;
            }
            else
            {
                lastPlayerPos.y = targetPos.y;
            }

            if (Mathf.Abs(targetPos.z - lastPlayerPos.z) < threshold.z)
            {
                targetPos.z = lastPlayerPos.z;
            }
            else
            {
                lastPlayerPos.z = targetPos.z;
            }

        }
        else
        {
            lastPlayerPos = targetPos;
            //lastPlayerY = targetPos.y;
        }

        // プレイヤーの位置に更新
        transform.position = targetPos;
    }
}
