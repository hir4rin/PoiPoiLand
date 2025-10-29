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
    [SerializeField] Boss _boss;//boss
    [SerializeField] GameObject _UIManagerObj; // UI管理用
    UIManager _UIManager; // UI管理用
    private bool isUISet; // ゲームクリアのUI表示フラグ

    public bool isDieBoss;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        _warp3.SetActive(false);
        _UIManager = _UIManagerObj.GetComponent<UIManager>();
        isUISet = false;
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
        if (!isUISet)
        {
            _UIManager.FadeOutImage(4, 3.0f); // すでに出ているUIをフェードアウト
            _UIManager.SetGameSceneUI(1, true);
            _UIManager.FadeInImage(1, 2.0f);
            isUISet = true;
        }
        Debug.Log("Boss Defeated");
        //PointNumを6にあげる
        PlayerPrefs.SetInt("PointNum", 6);
        //ボス死亡処理
        _boss.StartShrink();

    }
}
