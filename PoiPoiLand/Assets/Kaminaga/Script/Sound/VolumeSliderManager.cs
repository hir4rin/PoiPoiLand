using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スライダーと音量の状態をリンクさせるためのスクリプト
public class VolumeSliderManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 現在のボリュームを取得する

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void SetMasterVolume(float volume)
    {
        SoundManager.Instance.SetMasterVolume(volume);
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
