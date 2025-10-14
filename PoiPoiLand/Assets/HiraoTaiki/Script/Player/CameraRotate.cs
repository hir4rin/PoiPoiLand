using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera introCam;
    CinemachineTransposer _transposer;//offset‚ð‚¢‚¶‚é‚Æ‚±‚ë

    float speed = 0.50f;
    public bool IsOpened;
    // Start is called before the first frame update
    void Start()
    {
        //Body‚ªTransposer‚È‚Ì‚Å
        _transposer = introCam.GetCinemachineComponent<CinemachineTransposer>();
        IsOpened = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _transposer.m_FollowOffset -= new Vector3(1, 0, 0) * speed;
        if (_transposer.m_FollowOffset.x <= -60)
        {
            _transposer.m_FollowOffset.x = -60;
            introCam.Priority = 10;
            IsOpened = true;
        }
    }
}
