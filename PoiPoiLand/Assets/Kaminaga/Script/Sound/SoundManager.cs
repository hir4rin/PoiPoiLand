using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioMixer audioMixer;

    private AudioSource audioSource;

    float currentVolume;
    float lastVolume;
    [SerializeField] private AudioSource seAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        currentVolume = 0.0f;
        lastVolume = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentVolume = seAudioSource.volume;
        if (currentVolume != lastVolume)
        {
        }
        lastVolume = currentVolume;
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }

    public void SetBGMVolume(float volume)
    {
        //audioSource.volume = volume;
        audioMixer.SetFloat("BGM", volume);
    }

    public void SetSEVolume(float volume)
    {
        //seAudioSource.volume = volume;
        audioMixer.SetFloat("SE", volume);
    }
}
