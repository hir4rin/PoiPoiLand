using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearController : MonoBehaviour
{
    public float delayTime = 5f;
    private bool canPress = false;

    public GameObject _message;
    bool Flash = false;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        _message.SetActive(false);
        Invoke("EnableInput", delayTime);//時間指定
    }

    void EnableInput()
    {
        canPress = true;
        _message.SetActive(true);
        timer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }
        timer += Time.deltaTime;
       
        if (_message != null)
        {
            if (timer > 0.8f)
            {
                Flash = !Flash;
                //Debug.Log("変わりました");
                timer = 0;
            }
        }

        if (Flash)
        {
            _message.GetComponent<SpriteRenderer>().enabled = true;
            //Debug.Log("aaaaaaaaa");
        }
        else
        {
            _message.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (canPress && Input.GetButtonDown("Submit"))
        {
            SoundManager.Instance.ChangeBGMClip(0); // タイトルのBGMに変更
            SoundManager.Instance.PlayBGMWithCrossFade(2.0f);
            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");

        }

    }
}
