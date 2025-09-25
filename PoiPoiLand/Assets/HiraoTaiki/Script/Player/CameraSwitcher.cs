using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineFreeLook behindCam;
    public CinemachineFreeLook sidecam;
    public CinemachineFreeLook _secondBehind;
    public CinemachineFreeLook _secondSide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("変わった");
            behindCam.Priority = 10;
            sidecam.Priority = 20;//横が有効
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            sidecam.Priority = 10;
            _secondBehind.Priority = 20;//後ろが有効
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            _secondBehind.Priority = 10;
            _secondSide.Priority = 20;//横が有効
        }
    }
    private void FixedUpdate()
    {
        
    }
    public void SwitchSide()
    {
        behindCam.Priority = 10;
        sidecam.Priority = 20;//横が有効
    }
}
