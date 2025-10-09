using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook behindCam;
    [SerializeField] private CinemachineFreeLook sidecam;
    [SerializeField] private CinemachineFreeLook _secondBehind;
    [SerializeField] private CinemachineFreeLook _secondSide;
    [SerializeField] private CinemachineVirtualCamera _stage2Cam;
    [SerializeField] private CinemachineFreeLook _stage3Cam;
    //public CinemachineFreeLook currentCamera;

    // Start is called before the first frame update
    void Start()
    {
        //currentCamera = behindCam;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("変わった");
            behindCam.Priority = 10;
            _secondBehind.Priority = 10;
            _secondSide.Priority = 10;
            sidecam.Priority = 20;//横が有効
            //currentCamera = sidecam;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            sidecam.Priority = 10;
            behindCam.Priority = 10;
            _secondSide.Priority = 10;
            _secondBehind.Priority = 20;//後ろが有効
            //currentCamera = _secondBehind;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _secondBehind.Priority = 10;
            behindCam.Priority = 10;
            sidecam.Priority = 10;
            _secondSide.Priority = 20;//横が有効
            //currentCamera = _secondSide;
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            _secondSide.Priority = 10;
            sidecam.Priority = 10;
            _secondBehind.Priority = 10;
            behindCam.Priority = 20;//後ろが有効
            //currentCamera = behindCam;
        }

        if(PlayerPrefs.GetInt("PointNum") == 3)
        {
            behindCam.Priority = 10;
            sidecam.Priority = 10;
            _secondBehind.Priority = 10;
            _secondSide.Priority = 10;
            _stage2Cam.Priority = 20;
            //currentCamera = _stage2Cam;
        }
        if(PlayerPrefs.GetInt("PointNum") == 4)
        {
            _stage2Cam.Priority = 10;
            behindCam.Priority = 20;
            //currentCamera = behindCam;
        }
        if (PlayerPrefs.GetInt("PointNum") == 5)
        {
            behindCam.Priority = 10;
            _stage3Cam.Priority = 20;
            //currentCamera = _stage3Cam;
        }
        if (PlayerPrefs.GetInt("PointNum") == 6)
        {
            _stage3Cam.Priority = 10;
            behindCam.Priority = 20;
            //currentCamera = behindCam;
        }
    }
    private void FixedUpdate()
    {
        
    }
    public void SwitchSide()
    {
        behindCam.Priority = 10;
        sidecam.Priority = 20;//横が有効
        //currentCamera = sidecam;
    }
}
