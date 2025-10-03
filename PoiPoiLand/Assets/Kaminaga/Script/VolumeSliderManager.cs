using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSliderManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void SetBGMVolume(float volume)
    {
        SoundManager.Instance.SetBGMVolume(volume);
    }

    public void SetSEVolume(float volume)
    {
        SoundManager.Instance.SetSEVolume(volume);
    }
}
