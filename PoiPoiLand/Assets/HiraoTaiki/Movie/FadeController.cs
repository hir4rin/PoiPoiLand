using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvasGroup;
     private float fadeDuration = 3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public IEnumerator FadeIn()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = 1 - (time / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 0;
    }
    // フェードアウト
    public IEnumerator FadeOut()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = (time / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 1;
        SceneChange();
    }

    public void SceneChange()
    {
        SoundManager.Instance.ChangeBGMClip(4); // ステージ3のBGMに変更
        SoundManager.Instance.PlayBGMWithCrossFade(4.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
