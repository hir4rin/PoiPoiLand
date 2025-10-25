using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] GameObject hitTurtleEffect;

    [SerializeField] List<AudioClip> hitSE; // 0:当たったSE(爆発音) 1: ボスボイス
    private AudioSource audioSource; // SE再生用

    // Start is called before the first frame update
    void Start()
    {
        //繋ぎ用
        _hammer = Resources.Load<GameObject>("Hammer_Prefab").GetComponent<HammerController>();
        _bowling = Resources.Load<GameObject>("GravityTurtle").GetComponent<BowlingNokonokoController>();
        _redGhost = Resources.Load<GameObject>("RedGhost").GetComponent<RedGostMove>();
        _mimic = GameObject.Find("Mimic").GetComponent<BossFakeMove>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeSE(int index)
    {
        audioSource.clip = hitSE[index];
    }
    public void OnTriggerEnter(Collider other)
    {
        //ハンマーをあてられた場合
        if (other.CompareTag("Hammer") && _hammerState == HammerState.pop)
        {
            _hammer = other.GetComponent<HammerController>();
            if (_hammer.currentState == HammerState.thrown)
            {
                //ヒット喰らい処理

                // 音再生
                ChangeSE(0);
                SoundManager.Instance.PlaySE(audioSource);
                ChangeSE(1);
                SoundManager.Instance.PlaySE(audioSource);

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

                // 音再生
                ChangeSE(0);
                SoundManager.Instance.PlaySE(audioSource);
                ChangeSE(1);
                SoundManager.Instance.PlaySE(audioSource);

                //ダメージ処理
                _hp.TakeDamage(3000);
                _mimic.HeavyDamage();
            }

        }
        else if (other.CompareTag("HitRedGhost"))
        {
            _redGhost = other.GetComponentInParent<RedGostMove>();
            if (_redGhost.isChasingBoss)
            {
                //ヒット喰らい処理

                // 音再生
                ChangeSE(0);
                SoundManager.Instance.PlaySE(audioSource);
                ChangeSE(1);
                SoundManager.Instance.PlaySE(audioSource);

                //ダメージ処理
                _hp.TakeDamage(1000);
                _mimic.HeavyDamage();

                //エフェクト
                //エフェクトを生成
                GameObject effect = Instantiate(hitTurtleEffect, this.transform.position, Quaternion.identity);
                Destroy(effect, 5f);
            }
        }

    }
}
