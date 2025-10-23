using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // ListでUIを管理、表示非表示用
    [SerializeField] private List<GameObject> gameSceneUIList;

    // UIの透明度とかをいじる用
    //private Dictionary<string, Image> uiImageDictionary;

    // Imageの拡大縮小処理用
    private Coroutine scaleCoroutine;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            SetGameSceneUI(0, true);
            FadeOutImage(0, 2.0f);
        }
    }

    public void SetGameSceneUI(int index, bool isActive)
    {
        gameSceneUIList[index].SetActive(isActive);
    }

    public void FadeInImage(int index, float duration)
    {
        StartCoroutine(FadeInCoroutine(index, duration));
    }

    /// <summary>
    /// UIをフェードインさせる
    /// </summary>
    /// <param name="index">UIオブジェクトの番号</param>
    /// <param name="duration">フェード時間</param>
    /// <returns></returns>
    private IEnumerator FadeInCoroutine(int index, float duration)
    {
        Image uiImage = gameSceneUIList[index].GetComponentInChildren<Image>();
        Color originalColor = uiImage.color;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            uiImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        uiImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }

    /// <summary>
    /// UIをフェードアウトさせる(終わったらそのUIのsetActiveをfalseにする)
    /// </summary>
    /// <param name="index">UIオブジェクトの番号</param>
    /// <param name="duration">フェード時間</param>
    public void FadeOutImage(int index, float duration)
    {
        if(scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }
        StartCoroutine(FadeOutCoroutine(index, duration));
    }

    private IEnumerator FadeOutCoroutine(int index, float duration)
    {
        Image uiImage = gameSceneUIList[index].GetComponentInChildren<Image>();
        Color originalColor = uiImage.color;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            uiImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        uiImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        SetGameSceneUI(index, false);
    }

    public void FrameInFromRight(int index, float duration)
    {
        StartCoroutine(FrameInFromRightCoroutine(index, duration));
    }

    private IEnumerator FrameInFromRightCoroutine(int index, float duration)
    {
        RectTransform uiRectTransform = gameSceneUIList[index].GetComponentInChildren<RectTransform>();
        Vector3 originalPosition = uiRectTransform.anchoredPosition;
        Vector3 startPosition = new Vector3(Screen.width + uiRectTransform.rect.width, originalPosition.y, originalPosition.z);
        uiRectTransform.anchoredPosition = startPosition;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            uiRectTransform.anchoredPosition = Vector3.Lerp(startPosition, originalPosition, elapsedTime / duration);
            yield return null;
        }
        uiRectTransform.anchoredPosition = originalPosition;
    }

    public void MoveImage(int index)
    {
        Image uiImage = gameSceneUIList[index].GetComponentInChildren<Image>();
        uiImage.rectTransform.localScale += new Vector3(1.2f, 1.2f, 1.0f);
    }

    public void ScaleAnimationImage(int index, float minScale, float maxScale, float speed)
    {
        if(scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }
        scaleCoroutine = StartCoroutine(ScaleAnimationImageCoroutine(index, minScale, maxScale, speed));
    }

    /// <summary>
    /// 画像の拡大縮小のループを行う処理
    /// </summary>
    /// <param name="index">画像番号</param>
    /// <param name="minScale">最小サイズ</param>
    /// <param name="maxScale">最大サイズ</param>
    /// <param name="speed">ループの速さ</param>
    /// <returns></returns>
    private IEnumerator ScaleAnimationImageCoroutine(int index, float minScale, float maxScale, float speed)
    {
        // 画像の大きさを取得
        RectTransform uiRectTransform = gameSceneUIList[index].GetComponentInChildren<RectTransform>();
        // ループ計測用の時間を取得
        float time = 0.0f;

        while(true)
        {
            time += Time.deltaTime * speed; // 拡大縮小の時間を経過させる
            float t = (Mathf.Sin(time) + 1.0f) / 2.0f; // 値が0.0f~1.0fを繰り返す処理
            float scale = Mathf.Lerp(minScale, maxScale, t); // 最小値から最大値までの範囲を0.0f~1.0fで補間
            uiRectTransform.localScale = new Vector3(scale, scale, 1.0f); // 拡大縮小(zは必要ないので固定)
            yield return null;
        }
    }
}
