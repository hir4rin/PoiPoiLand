using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class movieManager : MonoBehaviour
{
   public  GameObject _chest;
    public GameObject _mimic;

   public  bool isChange = false;
   public  Animator animator;         // Animatorコンポーネントを保持
    bool animEndFlag = false; // アニメが終わったかどうか
    string endStateName = "Chest_Close 0"; // Animator上のステート名

    //2回目
   public  Animator animator2;         // Animatorコンポーネントを保持
    bool animEndFlag2 = false; // アニメが終わったかどうか
    string endStateName2 = "anim_Mimic_BattleStand"; // Animator上のステート名
    bool _fade = false;

    [SerializeField] GameObject _line;
    [SerializeField] CinemachineVirtualCamera lastCam;
    //Fade用
    [SerializeField] FadeController fadeController;
    float fadetimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        _mimic.SetActive(false);
        _line.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        // 現在のステート情報を取得
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        AnimatorStateInfo stateInfo2 = animator2.GetCurrentAnimatorStateInfo(0);
        // 第0レイヤー（通常のアニメ用レイヤー）
      

        // 現在のステートが「Attack」で、再生率が1.0以上＝アニメが終わった
        if (stateInfo.IsName(endStateName) && stateInfo.normalizedTime >= 2.0f)
        {
            if (!animEndFlag)
            {
                isChange = true;
                animEndFlag = true;
                Debug.Log("アニメーションが終了しました！");
                // ここにフラグを立てたあとに実行したい処理を追加する
            }
        }
        else
        {
            // Attack以外のステートなら、また次に備えてリセット
            animEndFlag = false;
        }
        //2回目(カメラ用)
        if (stateInfo2.IsName(endStateName2) && stateInfo2.normalizedTime >= 0.5f)
        {
            if (!animEndFlag2)
            {
                Debug.Log("aaaaaaaaaaaaaaaaaa");
                animEndFlag2 = true;
                lastCam.Priority = 30;
                _fade = true;
                fadetimer = 0;
            }
        }
        else
        {
            
            animEndFlag2 = false;
        }

        if (isChange)
        {
            _chest.SetActive(false);
            _mimic.SetActive(true);
            _line.SetActive(true) ;
            
        }
        if (_fade)
        {
            fadetimer += Time.deltaTime;
            if (fadetimer > 1)
            {
                StartCoroutine(fadeController.FadeOut());
            }
        }
        Debug.Log("現在のアニメステート: " + stateInfo2.IsName("anim_Mimic_BattleStand"));
    }
}
