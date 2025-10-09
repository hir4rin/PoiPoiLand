using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RabbitJenerator : MonoBehaviour
{

    const int count = 10;//生成数

    private GameObject _rabbit;
    GameObject _arriveRabbit;
    List<Rabbitmove> rabbits = new List<Rabbitmove>();


    public int rabbitCount = 0;


    float xStep = 0.3f;//xのずらす量
    public float zPos;//zの位置

    float x = -3.0f; //xの初期位置

    int dir = 1;//(1なら正、-1なら負)

    // Start is called before the first frame update
    void Start()
    {
        _rabbit = (GameObject)Resources.Load("Rabbit");
        RabbitSporn();

    }

    // Update is called once per frame
    void Update()
    {
       

    }
    public void RabbitSporn()
    {
        //ラビットを固定位置に生成(関数)
        //zは-3,xは0.5ずつずらす(-3から0を行ったり来たり)
     
            //初期化組
            rabbitCount = 0;//rabbitcountのリセット
            zPos = 0;
            x = -3.0f;

            for (int i = 0; i < count; i++)
            {

                Vector3 pos = new Vector3(154.3f + x, 32.3f, 6.5f + zPos);
            _arriveRabbit =  Instantiate(_rabbit, pos, Quaternion.Euler(0, 180, 0));
            Rabbitmove  _rabittmove= _arriveRabbit.GetComponent<Rabbitmove>();
            rabbits.Add(_rabittmove);//List
                //zをずらす
                zPos += 3;
                //xをずらす
                x += xStep * dir;

            // 次のウサギを置く前に、もし端を超えたら折り返す
            if (x > 0)
            {
                x = 0;
                dir = -1;
            }
            else if (x < -3)
            {
                x = -3;
                dir = 1;
            }


        }
        

    }
    public void AllReset()
    {
       foreach (var _rabbitmove in rabbits)
        {
            if (_rabbitmove != null)
            {
                _rabbitmove.AllDeath();
            }
            
        }
    }

}
