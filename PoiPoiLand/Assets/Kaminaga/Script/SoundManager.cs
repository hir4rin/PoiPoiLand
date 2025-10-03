using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

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

    public void SetBGMVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void SetSEVolume(float volume)
    {
        seAudioSource.volume = volume;
    }
}
