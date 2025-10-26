using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{

    const float clear_time = 3.0f;

    [SerializeField] RabbitJenerator _RJ;
    [SerializeField] TurtleJenerator _TJ;
    [SerializeField] GameObject _warp2;
    [SerializeField] GameObject _UIObj;
    private UIManager _UIManager;

    public bool isNext = false;
    float timer = 0;
    public bool isClear = false;
    float clearTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        _warp2.SetActive(false);
        _UIManager = _UIObj.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNext)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                _RJ.AllReset();//消えなかった
                //生成関数呼び出し
                isNext = false;
                _RJ.RabbitSporn();

                _TJ.turtleSpawn();//亀のリスポーン
            }
        }
    }

    private void FixedUpdate()
    {
        if (clearTimer > clear_time)
        {
            //クリアの画面を少しの間見せる
            isClear = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bowling"))
        {
            Debug.Log("終了");
            //クリアじゃなかったら、
            if (_RJ.rabbitCount < 8)
            {
                timer = 0;
                isNext = true;
             

            }
            if (_RJ.rabbitCount >= 8)
            {
                //ワープの出現]
                _warp2.SetActive(true);
                _UIManager.FadeOutImage(3, 0.50f);
                _UIManager.SetGameSceneUI(1, true); // クリアUI表示
                _UIManager.FadeInImage(1, 2.0f);

                if (_RJ.rabbitCount == 10)
                {
                    //パーフェクトのえんしゅつなり音なり
                }
                clearTimer += Time.deltaTime;
            }
        }
    }
}
