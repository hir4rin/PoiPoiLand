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
        _playerState = _player._stete;//_playerState�̍X�V

        //Debug.Log($"PlayerState��{_playerState}�ł�");

        if (_holdManager.isColHit�@&& _ball.isColHit)
        {
            if (Input.GetKeyDown(KeyCode.J))//���݁A���\���O�����銴��
            {
                Debug.Log("�����܂���");

                //������Player���Ŏ�����(�ʂ̃|�W�V������ς���player�̂��΂ɒu���Ƃ�����(�����킩���))
                //Unity�ɂ͎q�I�u�W�F�N�g�ɂ���@�\������炵��(DX_Library���Ƃ킩���)
            }
        }

    }
    private void FixedUpdate()
    {
       
    }
}
