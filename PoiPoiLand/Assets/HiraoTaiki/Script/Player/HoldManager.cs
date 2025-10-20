using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
public class HoldManager : MonoBehaviour
{

    public bool isColHit = false;
    public GameObject _playerManager;
    public Player _player;
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
        //        Debug.Log("持ちました");

        //    }
        //}
    }

    /// <summary>
    /// この範囲内でプレイヤーがボタンを押したときに持てるようにする
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground"))
        {

            isColHit = true;
            //ハンマーと当たった場合
            if (other.CompareTag("Hammer") && _player._state == PlayerState.Idle)
            {
                _playerManager.GetComponent<PlayerManager>().SetHammer(other.GetComponent<HammerController>());
            }
            //のこのこと当たった場合
            else if(other.CompareTag("Nokonoko") && _player._state == PlayerState.Idle)
            {
                _playerManager.GetComponent<PlayerManager>().SetNokonoko(other.GetComponent<NokonokoController>());
            }
            //ボウリングのこのこと当たった場合
            else if(other.CompareTag("Bowling") && _player._state == PlayerState.Idle)
            {
                _playerManager.GetComponent<PlayerManager>().SetBowling(other.GetComponent<BowlingNokonokoController>());
            }

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
