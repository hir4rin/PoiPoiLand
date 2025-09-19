using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Player _player;
    PlayerState _playerState;
    HoldManager _holdManager;//���݊Ǘ�
    Ball _ball;//�{�[��

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
        _playerState = _player._state;//_playerState�̍X�V

        //Debug.Log($"PlayerState��{_playerState}�ł�");

        if (_holdManager.isColHit�@&& _ball.isColHit)
        {
            if (Input.GetKeyDown(KeyCode.J))//���݁A���\���O�����銴��
            {
                Debug.Log("�����܂���");
                _player._state = PlayerState.Hold;
                _player._animator.SetBool("isHold", true);
                _ball.Hold();
            }
        }
        if (_player._state == PlayerState.Hold)
        {
            if (Input.GetKeyDown(KeyCode.K))//���̂𗎂Ƃ��Ƃ�
            {
                _ball.QuitHold();
                _player._state = PlayerState.Idle;
                _player._animator.SetBool("isHold", false);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                _ball.Throw();
                _player._state = PlayerState.Idle;
                _player._animator.SetBool("isHold", false);
            }

        }
        

    }
    private void FixedUpdate()
    {
       
    }
}
