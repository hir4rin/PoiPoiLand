using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGhostHitBox : MonoBehaviour
{

   
    GameObject parentObject;
    private RedGostMove _redGhost; // 親のスクリプト参照用

    private bool hasHitPlayer = false; // プレイヤーに当たったかどうか

    // Start is called before the first frame update
    void Start()
    {
      
         parentObject = transform.parent.gameObject;//親のゲームオブジェクト
        _redGhost = GetComponentInParent<RedGostMove>();//親のスクリプト
        hasHitPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        // ★既に当たっている場合は何もしない
        if (hasHitPlayer)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("プレイヤーに当たったら消える");
            hasHitPlayer = true;
            if (parentObject != null)
            {
                Destroy(parentObject);
            }
        }
        // ★既に破壊されている場合は何もしない
        if (_redGhost.isDestroyed) return;
        //if (other.CompareTag("Hammer"))//ハンマーのタグが投げるだったら
        //{
        //    HammerHit(other);
        //}
        if (other.CompareTag("Boss"))
        {
            if (_redGhost.isChasingBoss)
            {
                //  Debug.Log("ボスに当たったら消える");
                _redGhost.DestroyGhost();
            }


         
        }
    }

}
