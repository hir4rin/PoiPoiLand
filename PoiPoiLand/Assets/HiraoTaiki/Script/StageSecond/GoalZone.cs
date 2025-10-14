using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    [SerializeField] RabbitJenerator _RJ;
    [SerializeField] TurtleJenerator _TJ;
    [SerializeField] GameObject _warp2;

    public bool isNext = false;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        _warp2.SetActive(false);
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

                //
                isNext = false;
                _RJ.RabbitSporn();

                _TJ.turtleSpawn();//亀のリスポーン
            }

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
                if (_RJ.rabbitCount == 10)
                {
                    //パーフェクトのえんしゅつなり音なり

                }
            }

            
        }
        
            
    }
}
