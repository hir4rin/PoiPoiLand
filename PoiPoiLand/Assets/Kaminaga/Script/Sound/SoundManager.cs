using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource currentBGMSource;
    [SerializeField] private AudioSource nextBGMSource;
    // BGMのリスト 0:タイトル 1:ゲームシーン 2:ステージ1 3:ステージ2 4:ステージ3 5:ゲームクリア 6:ボス戦前のムービー
    [SerializeField] private List<AudioClip> bgmClipList;


    
    // Start is called before the first frame update
    void Awake()
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
    }
    void Start()
    {
        audioMixer.SetFloat("Master", 0.0f);
        audioMixer.SetFloat("SE", 0.0f);
        audioMixer.SetFloat("BGM", 0.0f);
        currentBGMSource.clip = bgmClipList[0];
        currentBGMSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBGMChannels(AudioSource sourseA, AudioSource sourseB)
    {
        currentBGMSource = sourseA;
        nextBGMSource = sourseB;
    }

    public void PlayBGMWithCrossFade(float duration)
    {
        if (currentBGMSource == null || nextBGMSource == null)
        {
            Debug.LogWarning("BGMがセットされていません");
            return;
        }
        if(nextBGMSource.clip == null)
        {
            Debug.LogWarning("次のBGMの内容がありません");
            return;
        }

        StartCoroutine(CrossFadeBGM(duration));
    }

    private IEnumerator CrossFadeBGM(float duration)
    {
        nextBGMSource.volume = 0.0f;
        nextBGMSource.Play();

        float time = 0.0f;
        while (time < duration)
        {
            float t = time / duration;
            currentBGMSource.volume = Mathf.Lerp(1.0f, 0.0f, t);
            nextBGMSource.volume = Mathf.Lerp(0.0f, 1.0f, t);
            time += Time.deltaTime;
            yield return null;
        }

        currentBGMSource.Stop();
        currentBGMSource.volume = 1.0f;

        // BGMを入れ替え
        var temp = currentBGMSource;
        currentBGMSource = nextBGMSource;
        nextBGMSource = temp;
    }

    public void SetMasterVolume(float volume)
    {
        float db = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f;
        audioMixer.SetFloat("Master", db);
    }

    public void SetBGMVolume(float volume)
    {
        float db = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f;
        audioMixer.SetFloat("BGM", db);
    }

    public void SetSEVolume(float volume)
    {
        float db = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f;
        audioMixer.SetFloat("SE", db);
    }

    public void PlaySE(AudioSource source)
    {
        source.PlayOneShot(source.clip);
    }

    public void PlayBGM(AudioSource source)
    {
        source.PlayOneShot(source.clip);
    }
    
    public void ChangeBGMClip(int index)
    {
        AudioClip selectedClip = bgmClipList[index];
        nextBGMSource.clip = selectedClip;
    }

}
