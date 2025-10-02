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

    // Start is called before the first frame update
    void Start()
    {
        state = Stage1State.Idle;
        warpPoint.SetActive(false);
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        isWait = false;
        stageTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(state);
        if(PlayerPrefs.GetInt("PointNum") == 1 || Input.GetKeyDown(KeyCode.P))
        {
            if (!isWait)
            {
                state = Stage1State.Wait;
                isWait = true;
            }
        }
        if (state == Stage1State.Start)
        {
            stageTime += Time.deltaTime;
            //Debug.Log(stageTime);
            if (stageTime > 30.0f)
            {
                state = Stage1State.Cleared;
            }
        }
        if(state == Stage1State.Failed && Input.GetKeyDown(KeyCode.S))
        {
            state = Stage1State.Wait;
        }
        if(state == Stage1State.Cleared)
        {
            warpPoint.SetActive(true);
        }
    }
}
