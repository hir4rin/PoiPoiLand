using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject _stage1;
    [SerializeField] private GameObject _stage2;
    [SerializeField] private GameObject _stage3;
    [SerializeField] private GameObject _uiObj;
    private UIManager _UIManager;
    private bool _isStage2Active;
    private bool _isStage3Active;

    // Start is called before the first frame update
    void Start()
    {
        _stage1.SetActive(false);
        _stage2.SetActive(false);
        _stage3.SetActive(false);
        _UIManager = _uiObj.GetComponent<UIManager>();
        _isStage2Active = false;
        _isStage3Active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("PointNum") == 1)
        {
            _stage1.SetActive(true);
        }
        if (PlayerPrefs.GetInt("PointNum") == 3)
        {
            if(!_isStage2Active)
            {
                _UIManager.SetGameSceneUI(3, true);
                _UIManager.FrameInFromRight(3, 1.0f);
                _isStage2Active = true;
            }
            _stage2.SetActive(true);
        }
        if(PlayerPrefs.GetInt("PointNum") == 4)
        {
            if(_isStage2Active)
            {
                _UIManager.FadeOutImage(1, 2.0f);
                _isStage2Active = false;
            }
        }
        if (PlayerPrefs.GetInt("PointNum") == 5)
        {
            if (!_isStage3Active)
            {
                _UIManager.SetGameSceneUI(4, true); // ボスをたおそうをセット
                _UIManager.FrameInFromRight(4, 1.0f);
                _UIManager.ScaleAnimationImage(4, 1.0f, 1.5f, 14.0f); // 画像の拡大縮小を行う処理(どこかで止めなければならない)
                _isStage3Active = true;
            }
            _stage3.SetActive(true);
        }
        if(PlayerPrefs.GetInt("PointNum") == 6)
        {
            if (_isStage3Active)
            {
                _UIManager.FadeOutImage(1, 2.0f);
                _isStage3Active = false;
            }
        }
    }
}
