using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{
    public Image _start;
    public Image _operation;
    public Image _bgm;
    public Image _start2;
    public Image _operation2;
    public Image _bgm2;
    // 矢印ボタン
    //public Image _right;
    //public Image _right2;
    //public Image _left;
    //public Image _left2;
    public Image _operationBack;
    public Image _operationBack2;
    public Image _bgmBack;
    public Image _bgmBack2;
    int _num;
    int _num2;
    int _num3;
    int _phase;

    // パッド対応
    float prevHorizontal = 0f;
    float prevVertical = 0f;
    float h;
    float v;

    // シーンを呼ぶ用
    public ManualController _manual;

    // Start is called before the first frame update
    void Start()
    {
        _num = 0;
        _phase = 0;

    }

    // Update is called once per frame
    void Update()
    {
        // 前フレーム値を更新
        prevHorizontal = h;
        prevVertical = v;

        // 現在の入力値を取得
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        // → 右を押した瞬間
        if (h > 0 && prevHorizontal <= 0)
        {
            if (_num2 < 2 && _phase == 1)
            {
                _num2++;
            }
            Debug.Log("右を押した瞬間！");
        }
        // ← 左を押した瞬間
        if (h < 0 && prevHorizontal >= 0)
        {
            if (_num2 > 0 && _phase == 1)
            {
                _num2--;
            }
            Debug.Log("左を押した瞬間！");
        }

        // ↑ 上を押した瞬間
        if (v > 0 && prevVertical <= 0)
        {
            if(_num > 0 && _phase == 0)
            {
                _num--;
            }
            Debug.Log("上を押した瞬間！");
        }
        // ↓ 下を押した瞬間
        if (v < 0 && prevVertical >= 0)
        {
            if (_num <= 2 && _phase == 0)
            {
                _num++;
            }
            Debug.Log("下を押した瞬間！");
        }

        

        switch (_num)
        {
            case 0:
        _start.enabled = false;
        _start2.enabled = true;
        _operation.enabled = true;
        _operation2.enabled = false;
        _bgm.enabled = true;
        _bgm2.enabled = false;

                break;
            case 1:
        _operation.enabled = false;
        _operation2.enabled = true;
                _start.enabled = true;
                _start2.enabled = false;
                _bgm.enabled = true;
                _bgm2.enabled = false;
                break;
            case 2:
                _bgm.enabled = false;
                _bgm2.enabled = true;
                _start.enabled = true;
                _start2.enabled = false;
                _operation.enabled = true;
                _operation2.enabled = false;
                break;
        }
        switch (_num2)
        {
            case 0:// 左矢印
                _operationBack.enabled = true;
                _operationBack2.enabled = false;
                break;
            case 1:// 右矢印
                _operationBack.enabled = true;
                _operationBack2.enabled = false;
                break;
            case 2:// 戻る
                _operationBack.enabled = false;
                _operationBack2.enabled = true;
                break;
        }
        switch (_num3)
        {
            case 0:// 左矢印
                _operationBack.enabled = true;
                _operationBack2.enabled = false;
                break;
            case 1:// 右矢印
                _operationBack.enabled = true;
                _operationBack2.enabled = false;
                break;
            case 2:// 戻る
                _operationBack.enabled = false;
                _operationBack2.enabled = true;
                break;
        }
        if (Input.GetButtonDown("AButton"))
        {
            if (_phase == 0)
            {
                switch (_num)
                {
                    case 0:
                        _manual.GameStart();
                        break;
                    case 1:
                        _manual.SetTutorialUI();
                        _phase = 1;
                        break;
                    case 2:
                        _manual.SetSoundUI();
                        _phase = 2;
                        break;
                }
            }
            else if (_phase == 1)
            {
                switch (_num2)
                {
                    case 0:
                       
                        break;
                    case 1:
                        
                       
                        break;
                    case 2:
                        _manual.BackSelect();
                        _phase = 0;
                        break;
                }
            }
            
        }
    }
}
