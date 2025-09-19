using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Player _player;
    PlayerState _playerState;
    HoldManager _holdManager;//つかみ管理
    HammerController _hammer;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _holdManager = GameObject.Find("HoldManager").GetComponent<HoldManager>();
        _hammer = GameObject.Find("Hammer_Prefab").GetComponent<HammerController>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerState = _player._state;//_playerStateの更新

        //Debug.Log($"PlayerStateは{_playerState}です");

        if (_holdManager.isColHit　&& _hammer.isColHit)
        {
            if (Input.GetKeyDown(KeyCode.J))//現在、結構ラグがある感じ
            {
                Debug.Log("持ちました");
                _player._state = PlayerState.Hold;
                _hammer.UpdateHold();
            }
        }
        if (_player._state == PlayerState.Hold)
        {
            if (Input.GetKeyDown(KeyCode.K))//ものを落とすとき
            {
                _hammer.QuitHold();
                _player._state = PlayerState.Idle;
                _player._animator.SetBool("isHold", false);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                _hammer.UpdateThrow();
                _player._state = PlayerState.Idle;
                _player._animator.SetBool("isHold", false);
            }

        }
        

    }
    private void FixedUpdate()
    {
       
    }
}
