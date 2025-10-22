using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinEffect : MonoBehaviour
{
    //イルカのエフェクト
    [SerializeField] GameObject dolphin_effect;

    // Start is called before the first frame update
    void Start()
    {
        //エフェクトをまとわせる
        GameObject effect = Instantiate(dolphin_effect, this.transform.position, Quaternion.identity);
        //effectをゴーストの子オブジェクトにする
        effect.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
