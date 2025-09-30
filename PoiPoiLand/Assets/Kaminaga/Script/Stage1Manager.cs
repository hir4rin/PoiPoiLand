using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stage1State
{
    Idle, // 未クリアで始まっていない
    Start, // 始まった
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
    private GameObject player;
    private bool isStart;
    private float stageTime; 

    // Start is called before the first frame update
    void Start()
    {
        state = Stage1State.Idle;
        player = GameObject.Find("Player");
        isStart = false;
        stageTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(state);
        if(player.transform.position == new Vector3(100.0f,21.5f,2.5f) || Input.GetKeyDown(KeyCode.P))
        {
            if (!isStart)
            {
                state = Stage1State.Start;
                isStart = true;
            }
        }
        if (state == Stage1State.Start)
        {
            stageTime += Time.deltaTime;
            if (stageTime > 30.0f)
            {
                state = Stage1State.Cleared;
            }
        }
    }
}
