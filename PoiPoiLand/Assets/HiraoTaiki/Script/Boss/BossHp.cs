using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
    int maxHp = 10000;
    int currentHp;

    public Image hpBar;

    [SerializeField] GameObject _warp3;//クリア条件

    public bool isDieBoss;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        _warp3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("damage喰らいました");
        currentHp -= damage;
        if (currentHp < 0)
        {
            currentHp = 0;
        }

        UpdateHpBar();

        if (currentHp <= 0)
        {
            Die();
        }
        
    }

    public void UpdateHpBar()
    {
        //見た目の調整(割合)
        hpBar.fillAmount = (float)currentHp / maxHp;
    }
    public void Die()
    {
        _warp3.SetActive(true);
        Debug.Log("Boss Defeated");
        //ボス死亡処理
       Destroy(gameObject);

    }
}
