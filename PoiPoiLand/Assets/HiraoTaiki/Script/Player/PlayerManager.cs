using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Player _player;
    PlayerState _playerState;
    HoldManager _holdManager;//���݊Ǘ�
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
        _playerState = _player._state;//_playerState�̍X�V

        //Debug.Log($"PlayerState��{_playerState}�ł�");

        if (_holdManager.isColHit�@&& _hammer.isColHit)
        {
            if (Input.GetKeyDown(KeyCode.J))//���݁A���\���O�����銴��
            {
                Debug.Log("�����܂���");
                _player._state = PlayerState.Hold;
                _hammer.UpdateHold();
            }
        }
        if (_player._state == PlayerState.Hold)
        {
            if (Input.GetKeyDown(KeyCode.K))//���̂𗎂Ƃ��Ƃ�
            {
                _hammer.QuitHold();
                _player._state = PlayerState.Idle;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                _hammer.UpdateThrow();
                _player._state = PlayerState.Idle;
            }

        }
        

    }
    private void FixedUpdate()
    {
       
    }
}
