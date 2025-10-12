using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera introCam;
    CinemachineTransposer _transposer;//offset‚ð‚¢‚¶‚é‚Æ‚±‚ë

    float speed = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        //Body‚ªTransposer‚È‚Ì‚Å
        _transposer = introCam.GetCinemachineComponent<CinemachineTransposer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            _transposer.m_FollowOffset -= new Vector3(1, 0, 0) * speed;
            if (_transposer.m_FollowOffset.x <= -60)
            {
                _transposer.m_FollowOffset.x = -60;
            }
        }
    }
}
