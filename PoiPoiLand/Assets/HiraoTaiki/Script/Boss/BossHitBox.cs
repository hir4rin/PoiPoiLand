using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBox : MonoBehaviour
{

    //bossのHP管理
    [SerializeField] BossHp _hp;
    //bossのアニメーション用
    public BossFakeMove _mimic;



    HammerController _hammer;//ハンマー
    HammerState _hammerState;

    BowlingNokonokoController _bowling;//のこのこボーリング
    BowlingNokonokoState _bowlingState;

    RedGostMove _redGhost;//赤いゴースト

    // Start is called before the first frame update
    void Start()
    {
        //繋ぎ用
        _hammer = Resources.Load<GameObject>("Hammer_Prefab").GetComponent<HammerController>();
        _bowling = Resources.Load<GameObject>("GravityTurtle").GetComponent<BowlingNokonokoController>();
        _redGhost = Resources.Load<GameObject>("RedGhost").GetComponent<RedGostMove>();
        _mimic = GameObject.Find("Mimic").GetComponent<BossFakeMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        //ハンマーをあてられた場合
        if (other.CompareTag("Hammer"))
        {
            _hammer = other.GetComponent<HammerController>();
            if (_hammer.currentState == HammerState.thrown)
            {
                //ヒット喰らい処理

                //ダメージ処理
                
                _hp.TakeDamage(300);
            }


        }
        //ボーリングのこのこをあてられた場合
        else if (other.CompareTag("Bowling"))
        {
            _bowling = other.GetComponent<BowlingNokonokoController>();
            if (_bowling.currentState == BowlingNokonokoState.NoGraThrow)
            {
                //ヒット喰らい処理
                Debug.Log("亀");
                //ダメージ処理
                _hp.TakeDamage(1500);
                _mimic.HeavyDamage();
            }

        }
        else if (other.CompareTag("HitRedGhost"))
        {
            _redGhost = other.GetComponentInParent<RedGostMove>();
            if (_redGhost.isChasingBoss)
            {
                //ヒット喰らい処理

                //ダメージ処理
                _hp.TakeDamage(1000);
                _mimic.HeavyDamage();
            }
        }

    }
}
