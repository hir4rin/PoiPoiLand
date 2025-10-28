using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ManualController : MonoBehaviour
{
    [SerializeField] private GameObject soundUI;
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private List<AudioClip> audioClipList;
    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        soundUI.SetActive(false);
        tutorialUI.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        


    }

    public void SetTutorialUI()
    {
        if (!soundUI.activeSelf)
        {
            // 操作説明画面の表示フラグを反転させる
            tutorialUI.SetActive(!tutorialUI.activeSelf);
        }
    }

    public void SetSoundUI()
    {
        if (!tutorialUI.activeSelf)
        {
            // 音量設定画面の表示フラグを反転させる
            soundUI.SetActive(!soundUI.activeSelf);
        }
    }

    public void BackSelect()
    {
        ChangeSe();
        SoundManager.Instance.PlaySE(audioSource);
        if (tutorialUI.activeSelf)
        {
            tutorialUI.SetActive(false);
        }
        if(soundUI.activeSelf)
        {
            soundUI.SetActive(false);
        }
    }

    public void GameStart()
    {
        //セーブデータを保存する処理
        PlayerPrefs.SetInt("PointNum", 0);
        PlayerPrefs.Save();
        SoundManager.Instance.PlaySE(audioSource);
        SoundManager.Instance.ChangeBGMClip(1);
        SoundManager.Instance.PlayBGMWithCrossFade(3.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    private void ChangeSe()
    {
        audioSource.clip = audioClipList[1];
    }
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
