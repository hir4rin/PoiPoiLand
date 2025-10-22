using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ManualController : MonoBehaviour
{
    [SerializeField] private GameObject soundUI;
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Image pageDisplay;
    public int pageNum;
    [SerializeField] private List<AudioClip> audioClipList;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Dictionary<string, AudioSource> spritesDictionary;
    // Start is called before the first frame update
    void Start()
    {
        soundUI.SetActive(false);
        tutorialUI.SetActive(false);
        pageNum = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(pageNum);
        if (tutorialUI.activeSelf)
        {
            pageDisplay.sprite = sprites[pageNum];
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                OnPreviousPage();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                OnNextPage();
            }
        }
        

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

    public void OnNextPage()
    {
        SoundManager.Instance.PlaySE(audioSource);
        if (pageNum < sprites.Count - 1)
        {
            pageNum++;
        }
    }

    public void OnPreviousPage()
    {
        if (pageNum > 0)
        {
            pageNum--;
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
}
