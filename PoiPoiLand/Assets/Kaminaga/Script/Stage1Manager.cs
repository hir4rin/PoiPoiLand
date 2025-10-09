using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject player;
    private Player playerScript;
    private bool isWait;
    private float stageTime;
    public int enemyNum;

    // Start is called before the first frame update
    void Start()
    {
        state = Stage1State.Idle;
        warpPoint.SetActive(false);
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        isWait = false;
        stageTime = 0;
        enemyNum = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("stage1の状態は " + state);
        Debug.Log("enemyの数" + enemyNum.ToString());
        if(Input.GetKeyDown(KeyCode.Q))
        {
            state = Stage1State.Cleared;
        }
        if (PlayerPrefs.GetInt("PointNum") == 1 || Input.GetKeyDown(KeyCode.P))
        {
            if (!isWait)
            {
                state = Stage1State.Wait;
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
                stageTime += Time.deltaTime;
                //Debug.Log(stageTime);
                if (stageTime > 30.0f)
                {
                    state = Stage1State.Cleared;
                }
                break;
            case Stage1State.Cleared:
                warpPoint.SetActive(true);
                break;
            case Stage1State.Failed:
                if (Input.GetKeyDown(KeyCode.S)) // リセットの処理をどうするか考え中
                {
                    stageTime = 0.0f;
                    state = Stage1State.Start;
                }
                break;
            default:

                break;
        }
    }
}
