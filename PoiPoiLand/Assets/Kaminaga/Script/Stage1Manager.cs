using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Stage1State
{
    Idle, // 未クリアで始まっていない
    Wait, // ウェーブが始まる前
    Start, // ウェーブが始まった
    Cleared, // クリアされた
    Failed // 失敗した
}

public class Stage1Manager : MonoBehaviour
{
    private Stage1State state;
    public Stage1State State
    {
        get { return state; }
        set { state = value; }
    }
    [SerializeField] private GameObject warpPoint;
    [SerializeField] private GameObject stage1UI;
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject uiObj;
    [SerializeField] private Image timeGauge;
    private UIManager uiManager;
    private GameObject player;
    private Player playerScript;
    private bool isWait;
    private bool isClear;
    private float stageTime;
    public int enemyNum;
    const float maxStageTime = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        state = Stage1State.Idle;
        warpPoint.SetActive(false);
        stage1UI.SetActive(false);
        startUI.SetActive(false);
        uiManager = uiObj.GetComponent<UIManager>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        isWait = false;
        isClear = false;
        stageTime = 0;
        enemyNum = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("stage1の状態は " + state);
        //Debug.Log("enemyの数" + enemyNum.ToString());
        if(Input.GetKeyDown(KeyCode.Q))
        {
            state = Stage1State.Cleared;
        }
        if (PlayerPrefs.GetInt("PointNum") == 1 || Input.GetKeyDown(KeyCode.P))
        {
            if (!isWait)
            {
                stage1UI.SetActive(true);
                state = Stage1State.Wait;
                uiManager.FrameInFromRight(2, 1.0f);
                SoundManager.Instance.ChangeBGMClip(2); // ステージ1のBGMに変更
                SoundManager.Instance.PlayBGMWithCrossFade(1.0f);
                isWait = true;
            }
        }
        
        switch (state)
        {
            case Stage1State.Idle:

                break;
            case Stage1State.Wait:

                break;
            case Stage1State.Start:
                if(!startUI.activeSelf)
                {
                    uiManager.SetGameSceneUI(2, false); // ステージ開始前UIを非表示
                    startUI.SetActive(true);
                }
                stageTime += Time.deltaTime;
                //Debug.Log("ステージの経過時間は" + stageTime.ToString());
                timeGauge.fillAmount = 1.0f - stageTime / maxStageTime;
                if (stageTime > maxStageTime)
                {
                    state = Stage1State.Cleared;
                }
                break;
            case Stage1State.Cleared:
                if (!isClear)
                {
                    warpPoint.SetActive(true);
                    startUI.SetActive(false);
                    uiManager.SetGameSceneUI(1, true); // ステージクリアUIを表示
                    uiManager.FadeInImage(1, 2.0f); // ステージクリアUIをフェードイン
                    isClear = true;
                }
                if (PlayerPrefs.GetInt("PointNum") >= 2) // クリア状態からワープした後にUIを消す
                {
                    uiManager.FadeOutImage(1, 2.0f);
                    stage1UI.SetActive(false);
                    state = Stage1State.Idle;
                }
                break;
            case Stage1State.Failed:
                //if (Input.GetKeyDown(KeyCode.M)) // リセットの処理をどうするか考え中(プレイヤーが死んだらorこのときに表示されるエフェクトに触れたら)
                //{
                //    stageTime = 0.0f;
                //    state = Stage1State.Start;
                //}
                break;
            default:

                break;
        }
    }

    public void RestartStage()
    {
        stageTime = 0.0f;
        state = Stage1State.Start;
    }
}
