using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    [SerializeField] private List<Image> TutorialRedButtonImages;
    [SerializeField] private List<Image> TutorialYellowButtonImages;
    [SerializeField] private List<Image> OptionRedButtonImages;
    [SerializeField] private List<Image> OptionYellowButtonImages;

    private int selectedIndex = 0;
    private int sliderIndex = 0;
    [SerializeField] private GameObject manualObj;
    private ManualController manualController;
    TitleState titleState;
    private float prevHorizontalInput;
    private float prevVerticalInput;
    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        manualController = manualObj.GetComponent<ManualController>();
        titleState = TitleState.Default;
        horizontalInput = 0;
        verticalInput = 0;
    }

    void FixedUpdate()
    {
        // 前フレーム値を更新
        prevHorizontalInput = horizontalInput;
        prevVerticalInput = verticalInput;

        // 現在の入力値を取得
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

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
        switch(verticalInput)
        {
            case 1:
                selectedIndex = (selectedIndex - 1 + mainButtons.Count) % mainButtons.Count;
                HighlightButton(selectedIndex);
                break;
            case -1:
                selectedIndex = (selectedIndex + 1) % mainButtons.Count;
                HighlightButton(selectedIndex);
                break;
        }
    }

    private void TutorialUpdate()
    {
       
    }

    private void OptionUpdate()
    {
        
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
