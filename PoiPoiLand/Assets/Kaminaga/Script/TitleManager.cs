using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public enum TitleState
{
    Default,
    Tutorial,
    Option
}
public class TitleManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> mainButtons; // Start, Tutorial, Sound
    [SerializeField] private GameObject soundUI;
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private List<Slider> soundSliders; // Master, BGM, SE

    // ボタンの画像リスト
    [SerializeField] private List<Image> defaultRedButtonImages;
    [SerializeField] private List<Image> defaultYellowButtonImages;
    [SerializeField] private List<Image> tutorialRedButtonImages;
    [SerializeField] private List<Image> tutorialYellowButtonImages;
    [SerializeField] private List<Image> optionRedButtonImages;
    [SerializeField] private List<Image> optionYellowButtonImages;

    private int selectedIndex = 0;
    private int sliderIndex = 0;
    [SerializeField] private GameObject manualObj;
    private ManualController manualController;
    TitleState titleState;
    private float prevHorizontalInput;
    private float prevVerticalInput;
    private float horizontalInput;
    private float verticalInput;

    int defaultNum;// デフォルト
    int tutorialNum; //操作説明
    int optionNum;// 設定画面

    void Start()
    {
        manualController = manualObj.GetComponent<ManualController>();
        titleState = TitleState.Default;
        horizontalInput = 0;
        verticalInput = 0;
    }

    void Update()
    {
        // 前フレーム値を更新
        prevHorizontalInput = horizontalInput;
        prevVerticalInput = verticalInput;

        // 現在の入力値を取得
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Debug.Log("番号" + defaultNum.ToString());
        switch (titleState)
        {
            case TitleState.Default:
                DefaultUpdate();
                break;
            case TitleState.Tutorial:
                TutorialUpdate();
                break;
            case TitleState.Option:
                OptionUpdate();
                break;
        }
    }

    private void DefaultUpdate()
    {
        // → 右を押した瞬間
        if (horizontalInput > 0 && prevHorizontalInput <= 0)
        {
            if (defaultNum == 0 || defaultNum == 1)
            {
                defaultNum = 2;
            }
            Debug.Log("右を押した瞬間！");
        }
        // ← 左を押した瞬間
        if (horizontalInput < 0 && prevHorizontalInput >= 0)
        {
            if (defaultNum == 2)
            {
                defaultNum = 0;
            }
            Debug.Log("左を押した瞬間！");
        }

        // ↑ 上を押した瞬間
        if (verticalInput > 0 && prevVerticalInput <= 0)
        {
            if(defaultNum == 1)
            {
                defaultNum = 0;
            }
            else if (defaultNum == 0)
            {
                defaultNum = 1;
            }
            Debug.Log("上を押した瞬間！");
        }
        // ↓ 下を押した瞬間
        if (verticalInput < 0 && prevVerticalInput >= 0)
        {
            if (defaultNum == 0)
            {
                defaultNum = 1;
            }
            else if (defaultNum == 1)
            {  
                defaultNum = 0;
            }
                Debug.Log("下を押した瞬間！");
        }
        switch (defaultNum)
        {
            // スタートのボタンにカーソルがあっているとき
            case 0:
                DefaultRedUI(1);
                DefaultRedUI(2);
                DefaultYellowUI(0);
                if (Input.GetButtonDown("AButton")|| Input.GetKeyDown(KeyCode.Return))
                {
                    manualController.GameStart();
                }
                break;
            // 操作説明のボタンにカーソルがあっているとき
            case 1:
                DefaultRedUI(0);
                DefaultRedUI(2);
                DefaultYellowUI(1);
                if (Input.GetButtonDown("AButton") || Input.GetKeyDown(KeyCode.Return))
                {
                    titleState = TitleState.Tutorial;
                    manualController.SetTutorialUI();
                }
                break;
            // 設定画面のボタンにカーソルがあっているとき
            case 2:
                DefaultRedUI(0);
                DefaultRedUI(1);
                DefaultYellowUI(2);
                if (Input.GetButtonDown("AButton") || Input.GetKeyDown(KeyCode.Return))
                {
                    Debug.Log("設定画面へ");
                    titleState = TitleState.Option;
                    manualController.SetSoundUI();
                }
                break;
        }
    }

    private void TutorialUpdate()
    {
        // ボタンのUIが一つしかないため、そのまま決定で戻る処理
        if (Input.GetButtonDown("AButton") || Input.GetKeyDown(KeyCode.Return))
        {
            manualController.BackSelect();
            titleState = TitleState.Default;
        }

    }

    private void OptionUpdate()
    {
        // → 右を押した瞬間
        if (horizontalInput > 0 && prevHorizontalInput <= 0)
        {
            if (optionNum == 0)
            {
                soundSliders[0].value += 0.05f;
            }
            else if (optionNum == 1)
            {
                soundSliders[1].value += 0.05f;
            }
            else if (optionNum == 2)
            {
                soundSliders[2].value += 0.05f;
            }
            else if (optionNum == 3)
            {
                optionNum = 4;
            }
            else if (optionNum == 4)
            {
                optionNum = 3;
            }
                Debug.Log("右を押した瞬間！");
        }
        // ← 左を押した瞬間
        if (horizontalInput < 0 && prevHorizontalInput >= 0)
        {
            if (optionNum == 0)
            {
                soundSliders[0].value -= 0.05f;
            }
            else if (optionNum == 1)
            {
                soundSliders[1].value -= 0.05f;
            }
            else if (optionNum == 2)
            {
                soundSliders[2].value -= 0.05f;
            }
            else if (optionNum == 3)
            {
                optionNum = 4;
            }
            else if (optionNum == 4)
            {
                optionNum = 3;
            }
            Debug.Log("左を押した瞬間！");
        }

        // ↑ 上を押した瞬間
        if (verticalInput > 0 && prevVerticalInput <= 0)
        {
            if(optionNum == 1)
            {
                optionNum = 0;
            }
            else if(optionNum == 2)
            {
                optionNum = 1;
            }
            else if (optionNum == 3 || optionNum == 4)
            {
                optionNum = 2;
            }
            Debug.Log("上を押した瞬間！");
        }
        // ↓ 下を押した瞬間
        if (verticalInput < 0 && prevVerticalInput >= 0)
        {
            if (optionNum == 0)
            {
                optionNum = 1;
            }
            else if (optionNum == 1)
            {
                optionNum = 2;
            }
            else if (optionNum == 2)
            {
                optionNum = 3;
            }
            Debug.Log("下を押した瞬間！");
        }
        switch (optionNum)
        {
            // マスターボタンにカーソルがあっているとき
            case 0:
                OptionRedUI(1);
                OptionRedUI(2);
                OptionRedUI(3);
                OptionRedUI(4);
                OptionYellowUI(0);
                break;
            // BGMのボタンにカーソルがあっているとき
            case 1:
                OptionRedUI(0);
                OptionRedUI(2);
                OptionRedUI(3);
                OptionRedUI(4);
                OptionYellowUI(1);
                break;
            // SEのボタンにカーソルがあっているとき
            case 2:
                OptionRedUI(0);
                OptionRedUI(1);
                OptionRedUI(3);
                OptionRedUI(4);
                OptionYellowUI(2);
                break;
            // 戻るのボタンにカーソルがあっているとき
            case 3:
                OptionRedUI(0);
                OptionRedUI(1);
                OptionRedUI(2);
                OptionRedUI(4);
                OptionYellowUI(3);
                if (Input.GetButtonDown("AButton") || Input.GetKeyDown(KeyCode.Return))
                {
                    titleState = TitleState.Default;
                    manualController.BackSelect();
                }
                break;
            // ゲーム終了のボタンにカーソルがあっているとき
            case 4:
                OptionRedUI(0);
                OptionRedUI(1);
                OptionRedUI(2);
                OptionRedUI(3);
                OptionYellowUI(4);
                if (Input.GetButtonDown("AButton") || Input.GetKeyDown(KeyCode.Return))
                {
                    titleState = TitleState.Default;
                    manualController.EndGame();
                }
                break;
        }
    }

    private void DefaultRedUI(int idx)
    {
        defaultRedButtonImages[idx].enabled = true;

        defaultYellowButtonImages[idx].enabled = false;

    }
    private void DefaultYellowUI(int idx)
    {
        defaultRedButtonImages[idx].enabled = false;

        defaultYellowButtonImages[idx].enabled = true;
    }
    private void OptionRedUI(int idx)
    {
        optionRedButtonImages[idx].enabled = true;

        optionYellowButtonImages[idx].enabled = false;
    }
    private void OptionYellowUI(int idx)
    {
        optionRedButtonImages[idx].enabled = false;

        optionYellowButtonImages[idx].enabled = true;
    }

    void HighlightButton(int index)
    {
        for (int i = 0; i < mainButtons.Count; i++)
        {
            bool isSelected = (i == index);
            //buttonImages[i].enabled = !isSelected;
            //currentButtonImages[i].enabled = isSelected;
        }
    }
}
