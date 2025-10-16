using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpEffectScript : MonoBehaviour
{
    //ワープ時のエフェクト
    [SerializeField] GameObject effect;

    //ボスを参照
    BossHp boss;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(effect, this.transform.position, Quaternion.identity);
        effect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("PointNum") == 5)
        {
            effect.SetActive(false);
        }
        if (boss.isDieBoss )
        {
            effect.SetActive(true);
        }
    }
}
