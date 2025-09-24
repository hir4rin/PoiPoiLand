using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineFreeLook behindCam;
    public CinemachineFreeLook sidecam;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("•Ï‚í‚Á‚½");
            behindCam.Priority = 10;
            sidecam.Priority = 20;//‰¡‚ª—LŒø
        }
    }
    private void FixedUpdate()
    {
        
    }
    public void SwitchSide()
    {
        behindCam.Priority = 10;
        sidecam.Priority = 20;//‰¡‚ª—LŒø
    }
}
