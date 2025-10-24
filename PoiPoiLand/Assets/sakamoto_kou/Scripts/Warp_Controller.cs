using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warp_Controller : MonoBehaviour
{
    public GameObject player;
    /// <summary>
    /// ワープ先
    /// </summary>
    public Vector3 StartPos = new Vector3(-35.0f, 13.0f, -9.0f);// スタート地点
    public Vector3 warpToFirstStage = new Vector3(100.0f, 21.5f, 2.5f); // ステージ1に移動
    public Vector3 warpToMapFirst = new Vector3(-36.0f, 13.5f, 10.1f); // ステージ1からマップに戻ってくる
    public Vector3 warpToSecondStage = new Vector3(152.5f, 5.3f, -2.5f); // ステージ2に移動
    public Vector3 warpToMapSecond = new Vector3(-91.0f, 15.0f, -3.5f); // ステージ2から帰ってくる
    public Vector3 warpToThirdStage = new Vector3(225.0f, 4.0f, -4.0f);//ステージ3移動
    public Vector3 warpToMapThird = new Vector3(-65.5f, 15.8f, -3.5f);//ステージ3から帰ってくる

    private bool isMovieStart;
    private bool isBGMChange;
    float fadeDuration = 2f;

    [SerializeField] Image whiteImage;//白いImageで登録
    float whiteDuration = 1f;//暗転時間

    // Start is called before the first frame update
    void Start()
    {
        isMovieStart = false;
        isBGMChange = false;
        // StartCoroutine(DarkenRoutine());
        //  whiteImage.SetActive(false);
        if (whiteImage != null)
        {
            Color c = whiteImage.color;
            c.a = 0f;
            whiteImage.color = c;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayerPrefs.SetInt("PointNum", 0);
            Debug.Log("スタート地点に戻る");
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.SetInt("PointNum", 1);
            Debug.Log("現在のcheckは" + PlayerPrefs.GetInt("PointNum"));
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.SetInt("PointNum", 2);
            Debug.Log("現在のcheckは" + PlayerPrefs.GetInt("PointNum"));
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerPrefs.SetInt("PointNum", 3);
            Debug.Log("現在のcheckは" + PlayerPrefs.GetInt("PointNum"));
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerPrefs.SetInt("PointNum", 4);
            if (!isMovieStart)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("BossMovieScene");
                isMovieStart = true;
            }
            Debug.Log("現在のcheckは" + PlayerPrefs.GetInt("PointNum"));
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayerPrefs.SetInt("PointNum", 5);
            Debug.Log("現在のcheckは" + PlayerPrefs.GetInt("PointNum"));
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayerPrefs.SetInt("PointNum", 6);
            Debug.Log("現在のcheckは" + PlayerPrefs.GetInt("PointNum"));
        }

    }
    private IEnumerator whiteRoutine(Vector3 map)
    {
       // whiteImage.SetActive(true);
      player.transform.position = map;
        yield return new WaitForSeconds(whiteDuration);
        //whiteImage.SetActive(false);
    }
    private IEnumerator whiteSequence(Vector3 map)
    {

        //yield return new WaitForSeconds(0.5f);
        //ここでフェード
        yield return StartCoroutine(FadeOut2());

        yield return new WaitForSeconds(1f);
       player.transform.position = map;

        yield return StartCoroutine(FadeIn2());

    }
    private IEnumerator FadeOut2()
    {
        float elapsed = 0f;
        Color c = whiteImage.color;

        while (elapsed < (fadeDuration * 0.1f))
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / (fadeDuration * 0.1f)); // 透明→白
            whiteImage.color = c;
            yield return null;
        }
    }
    private IEnumerator FadeIn2()
    {

        Debug.Log("フェードイン中");
        float elapsed = 0f;
        Color c = whiteImage.color;

        while (elapsed < fadeDuration * 0.5f)
        {
            elapsed += Time.deltaTime;
            c.a = 1f - Mathf.Clamp01(elapsed / (fadeDuration * 0.5f)); // 白→透明
            whiteImage.color = c;
            yield return null;
            //isCoroutine = false;

        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("触れた");
            switch (PlayerPrefs.GetInt("PointNum"))
            {
                case 0:
                    StartCoroutine(whiteSequence(warpToFirstStage));

                   // player.transform.position = warpToFirstStage;
                    PlayerPrefs.SetInt("PointNum", 1);
                    Debug.Log("case0");
                    break;
                case 1:
                    SoundManager.Instance.ChangeBGMClip(1); // マップのBGMに変更
                    SoundManager.Instance.PlayBGMWithCrossFade(2.0f);
                    StartCoroutine(whiteSequence(warpToMapFirst));
                    //player.transform.position = warpToMapFirst;
                    PlayerPrefs.SetInt("PointNum", 2);
                    Debug.Log("case1");
                    break;
                case 2:
                    if(!isBGMChange)
                    {
                        SoundManager.Instance.ChangeBGMClip(3); // ステージ2のBGMに変更
                        SoundManager.Instance.PlayBGMWithCrossFade(2.0f);
                        isBGMChange = true;
                    }
                    StartCoroutine(whiteSequence(warpToSecondStage));
                   // player.transform.position = warpToSecondStage;
                    PlayerPrefs.SetInt("PointNum", 3);
                    Debug.Log("case2");
                    break;
                case 3:
                    SoundManager.Instance.ChangeBGMClip(1); // マップのBGMに変更
                    SoundManager.Instance.PlayBGMWithCrossFade(2.0f);
                    StartCoroutine(whiteSequence(warpToMapSecond));
                    //player.transform.position = warpToMapSecond;
                    PlayerPrefs.SetInt("PointNum", 4);
                    Debug.Log("case3");
                    break;
                case 4:
                    if (!isMovieStart)
                    {
                        // ここでmovie用の音に変更したい
                        UnityEngine.SceneManagement.SceneManager.LoadScene("BossMovieScene");
                        isMovieStart = true;
                    }
                    StartCoroutine(whiteSequence(warpToThirdStage));
                   // player.transform.position = warpToThirdStage;
                    PlayerPrefs.SetInt("PointNum", 5);
                    Debug.Log("case4");
                    break;
                case 5:
                    StartCoroutine(whiteSequence(warpToThirdStage));
                   // player.transform.position = warpToThirdStage;
                    PlayerPrefs.SetInt("PointNum", 6);
                    Debug.Log("case5");
                    break;
                case 6:
                    SoundManager.Instance.ChangeBGMClip(1); // マップのBGMに変更
                    SoundManager.Instance.PlayBGMWithCrossFade(2.0f);

                    StartCoroutine(whiteSequence(warpToMapThird));

                   // player.transform.position = warpToMapThird;
                    Debug.Log("case6");
                    break;
                default:
                    Debug.Log("PointNumがありません");
                    break;
            }
        }
    }
}
