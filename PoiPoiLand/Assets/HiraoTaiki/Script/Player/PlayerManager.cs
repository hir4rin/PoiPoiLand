using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Player _player;
    PlayerState _playerState;
    HoldManager _holdManager;//つかみ管理
    Ball _ball;//ボール

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _holdManager = GameObject.Find("HoldManager").GetComponent<HoldManager>();
        _ball = GameObject.Find("Ball").GetComponent<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerState = _player._stete;//_playerStateの更新

        //Debug.Log($"PlayerStateは{_playerState}です");

        if (_holdManager.isColHit　&& _ball.isColHit)
        {
            if (Input.GetKeyDown(KeyCode.J))//現在、結構ラグがある感じ
            {
                Debug.Log("持ちました");

                //ここにPlayer側で持つ処理(玉のポジションを変えてplayerのそばに置いとく処理(ここわからん))
                //Unityには子オブジェクトにする機能があるらしい(DX_Libraryだとわからん)
            }
        }

    }
    private void FixedUpdate()
    {
       
    }
}
