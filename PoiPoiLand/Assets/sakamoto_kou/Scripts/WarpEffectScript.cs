using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpEffectScript : MonoBehaviour
{
    //ボスステージのワープ
    [SerializeField] GameObject bossStage_warp;
    //ほかのステージのワープ
    [SerializeField] GameObject[] usually_warps;
    //ワープ時のエフェクト
    [SerializeField] GameObject bossStageEffect;
    [SerializeField] GameObject usuallyStageEffect;
    GameObject effect;

    //ボスを参照
    public BossHp boss;

    // Start is called before the first frame update

    void Start()
    {
        //ボスステージのワープのエフェクト
        effect = Instantiate(bossStageEffect, bossStage_warp.transform.position, Quaternion.identity);
        //ほかのステージのワープエフェクト
        foreach (GameObject effect in usually_warps)
        {
            Instantiate(usuallyStageEffect, effect.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            effect.SetActive(true);
        }
        else
        {
            effect.SetActive(false);
        }
    }
}
