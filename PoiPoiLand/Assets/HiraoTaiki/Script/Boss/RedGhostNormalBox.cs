using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGhostNormalBox : MonoBehaviour
{

    RedGostMove _redGhost;

    private bool hasHitHammer = false; // ★フラグを追加

    // Start is called before the first frame update
    void Start()
    {
        _redGhost = GetComponentInParent<RedGostMove>();
        hasHitHammer = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("hasHitHammer" + hasHitHammer);
        // ★既に当たっている場合は何もしない
        //if (hasHitHammer || _redGhost == null)
        //{
        //    return;
        //}

        if (other.CompareTag("Hammer"))//ハンマーのタグが投げるだったら
        {
            _redGhost.HammerHit(other);
            hasHitHammer = true;
        }
    }
}
