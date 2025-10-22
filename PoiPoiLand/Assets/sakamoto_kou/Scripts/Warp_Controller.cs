using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Vector3 warpToMapSecond = new Vector3(-91.0f, 15.0f, 15.5f); // ステージ2から帰ってくる
    public Vector3 warpToThirdStage = new Vector3(225.0f, 4.0f, -4.0f);//ステージ3移動
    public Vector3 warpToMapThird = new Vector3(-65.5f, 15.8f, -3.5f);//ステージ3から帰ってくる

    private bool isMovieStart;
    private bool isBGMChange;

    // Start is called before the first frame update
    void Start()
    {
        isMovieStart = false;
        isBGMChange = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("触れた");
            switch (PlayerPrefs.GetInt("PointNum"))
            {
                case 0:
                    player.transform.position = warpToFirstStage;
                    PlayerPrefs.SetInt("PointNum", 1);
                    Debug.Log("case0");
                    break;
                case 1:
                    player.transform.position = warpToMapFirst;
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
                    player.transform.position = warpToSecondStage;
                    PlayerPrefs.SetInt("PointNum", 3);
                    Debug.Log("case2");
                    break;
                case 3:
                    player.transform.position = warpToMapSecond;
                    PlayerPrefs.SetInt("PointNum", 4);
                    Debug.Log("case3");
                    break;
                case 4:
                    if (!isMovieStart)
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene("BossMovieScene");
                        isMovieStart = true;
                    }
                    player.transform.position = warpToThirdStage;
                    PlayerPrefs.SetInt("PointNum", 5);
                    Debug.Log("case4");
                    break;
                case 5:
                    player.transform.position = warpToThirdStage;
                    PlayerPrefs.SetInt("PointNum", 6);
                    Debug.Log("case5");
                    break;
                case 6:
                    player.transform.position = warpToMapThird;
                    Debug.Log("case6");
                    break;
                default:
                    Debug.Log("PointNumがありません");
                    break;
            }
        }
    }
}
