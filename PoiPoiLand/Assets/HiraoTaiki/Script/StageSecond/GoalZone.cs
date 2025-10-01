using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    [SerializeField] RabbitJenerator _RJ;
    [SerializeField]

    bool isNext = false;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isNext)
        {
            
            timer += Time.deltaTime;
            if (timer > 3)
            {
                //生成関数呼び出し

                //
                isNext = false;
                _RJ.RabbitSporn();
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bowling"))
        {
            Debug.Log("終了");
            //クリアじゃなかったら、
            if (_RJ.rabbitCount < 7)
            {
                timer = 0;
                isNext = true;
                _RJ.AllReset();//消えなかった

            }
            if (_RJ.rabbitCount >= 7)
            {
                //ワープの出現
            }
            
        }
        
            
    }
}
