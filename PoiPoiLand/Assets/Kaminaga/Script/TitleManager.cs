using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> mainButtons; // Start, Tutorial, Sound
    [SerializeField] private GameObject soundUI;
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private List<Slider> soundSliders; // Master, BGM, SE
    [SerializeField] private List<Image> buttonImages;
    [SerializeField] private List<Image> currentButtonImages;
    private int selectedIndex = 0;
    private int phase = 0; // 0: ÉÅÉCÉì, 1: ëÄçÏê‡ñæ, 2: âπó í≤êÆ
    private int sliderIndex = 0;
    [SerializeField] private GameObject manualObj;
    private ManualController manualController;

    void Start()
    {
        manualController = manualObj.GetComponent<ManualController>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (phase == 0) // ÉÅÉCÉìëIë
        {
            if (v > 0) selectedIndex = Mathf.Max(0, selectedIndex - 1);
            if (v < 0) selectedIndex = Mathf.Min(mainButtons.Count - 1, selectedIndex + 1);

            HighlightButton(selectedIndex);

            if (Input.GetButtonDown("AButton"))
            {
                switch (selectedIndex)
                {
                    case 0: manualController.GameStart(); break;
                    case 1: manualController.SetTutorialUI(); phase = 1; break;
                    case 2: manualController.SetSoundUI(); phase = 2; break;
                }
            }
        }
        else if (phase == 2) // âπó í≤êÆ
        {
            if (v > 0) sliderIndex = Mathf.Max(0, sliderIndex - 1);
            if (v < 0) sliderIndex = Mathf.Min(soundSliders.Count - 1, sliderIndex + 1);

            if (h > 0) soundSliders[sliderIndex].value += 0.05f;
            if (h < 0) soundSliders[sliderIndex].value -= 0.05f;


            if (Input.GetButtonDown("BButton"))
            {
                soundUI.SetActive(false);
                phase = 0;
            }
        }
    }

    void HighlightButton(int index)
    {
        for (int i = 0; i < mainButtons.Count; i++)
        {
            bool isSelected = (i == index);
            buttonImages[i].enabled = !isSelected;
            currentButtonImages[i].enabled = isSelected;
        }
    }
}
