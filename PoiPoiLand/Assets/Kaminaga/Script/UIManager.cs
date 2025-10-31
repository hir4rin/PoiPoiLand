using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // ListでUIを管理、表示非表示用
    // 0:ゴールを目指そう 1:ステージクリア 2:ゴーストをたおそう 3: ウサギをたおそう 4: ゴーストをたおそう 5: MISS 6: 操作説明
    [SerializeField] private List<GameObject> gameSceneUIList;

    // UIの透明度とかをいじる用
    //private Dictionary<string, Image> uiImageDictionary;

    // Imageの拡大縮小処理用
    private Coroutine scaleCoroutine;
    private Coroutine missCoroutine;
    private Vector3 missUIFirstPos; // missのUIの位置保存用


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Manual"))
        {
            gameSceneUIList[6].SetActive(!gameSceneUIList[6].activeSelf);
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

    /// <summary>
    /// 画面の右からスライドするようにUIが動いてくる関数
    /// 止まる地点は最初に配置してある部分
    /// </summary>
    /// <param name="index"></param>
    /// <param name="duration"></param>
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
        Image uiImage = gameSceneUIList[index].GetComponentInChildren<Image>();
        // 画像の大きさを取得
        RectTransform uiRectTransform = uiImage.GetComponent<RectTransform>();
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

    /// <summary>
    /// ミスしたときのUIアニメーション再生処理
    /// 画像が画面手前から来るような感じで拡大率が変わり、
    /// 特定の座標に来たら止まる
    /// 止まった後に少し傾く
    /// </summary>
    /// <param name="index">画像番号(5番以外で使うのは非推奨)</param>
    /// <param name="moveDuration">画面手前から定位置に来るまでの時間</param>
    /// <param name="tiltDuration">傾くまでの時間</param>
    /// <param name="endDuration">フェードアウトして消えるまでの時間</param>
    public void MissAnimation(int index, float moveDuration, float tiltDuration, float endDuration)
    {
        if(missCoroutine != null)
        {
            StopCoroutine(missCoroutine);
        }
        missCoroutine = StartCoroutine(MissAnimationCoroutine(index, moveDuration, tiltDuration, endDuration));
    }

    private IEnumerator MissAnimationCoroutine(int index, float moveDuration, float tiltDuration, float endDuration)
    {
        SetGameSceneUI(5, true);
        // 画像の大きさを取得
        RectTransform uiRectTransform = gameSceneUIList[index].GetComponentInChildren<RectTransform>();
        Vector3 startPos = new Vector3(0f, 500f, 0f); // 奥の画面
        if (missUIFirstPos == null)
        {
            missUIFirstPos = uiRectTransform.anchoredPosition; // MissUIの初期位置を保存
        }
        Vector3 targetPos = missUIFirstPos; // 止まる座標(関数が呼ばれた際の初期位置)
        uiRectTransform.localPosition = startPos;
        uiRectTransform.localRotation = Quaternion.identity;

        float elapsed = 0.0f; // カウンタ
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime; // 時間を計測
            float t = elapsed / moveDuration; // 0.0~1.0までの割合の作成
            uiRectTransform.localPosition = Vector3.Lerp(startPos, targetPos, t); // 位置を動かす
            yield return null;
        }
        transform.localPosition = targetPos; // 終わったら初期位置に移動

        elapsed = 0.0f; // カウンタをリセット
        Quaternion startRot = Quaternion.identity; // 初期回転
        Quaternion targetRot = Quaternion.Euler(0.0f,0.0f,-10.0f); // 最終的に回転させたい角度
        yield return new WaitForSeconds(0.2f);
        while (elapsed < tiltDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / tiltDuration;
            uiRectTransform.localRotation = Quaternion.Lerp(startRot, targetRot, t);
            yield return null;
        }
        uiRectTransform.localRotation = targetRot; // 終わったら終了位置に補正

        Vector3 endPos = new Vector3(0f,-500f, 0f);
        elapsed = 0.0f; // カウンタをリセット
        while (elapsed < endDuration)
        {
            elapsed += Time.deltaTime; // 時間を計測
            float t = elapsed / endDuration; // 0.0~1.0までの割合の作成
            uiRectTransform.localPosition = Vector3.Lerp(targetPos, endPos, t); // 位置を動かす
            yield return null;
        }

        SetGameSceneUI(index, false);
    }
}
