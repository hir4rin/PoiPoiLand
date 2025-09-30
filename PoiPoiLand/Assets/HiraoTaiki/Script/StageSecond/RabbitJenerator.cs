using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitJenerator : MonoBehaviour
{
    [SerializeField] GameObject _rabbit;


    public int rabbitCount = 0;

    int count = 10;//生成数
    float xStep = 0.5f;//xのずらす量
    public float zPos;//zの位置

    float x = -3f; //xの初期位置

    int dir = 1;//(1なら正、-1なら負)

    // Start is called before the first frame update
    void Start()
    {
     
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(rabbitCount);
        }

        //ラビットを固定位置に生成(関数)
        //zは-3,xは0.5ずつずらす(-3から0を行ったり来たり)
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < count;i++)
            {
                Vector3 pos = new Vector3(154.3f + x,32.3f,6.5f + zPos);
                Instantiate(_rabbit,pos,Quaternion.Euler(0,180,0));
                //zをずらす
                zPos += 3;
                //xをずらす
                x += xStep * dir;


                //端に来たら折り返し
                if (x >= 0 || x <= -3)
                {
                    dir *= -1;
                }


            }
        }

        //ラビットが亀に触れると倒れる

    }

}
