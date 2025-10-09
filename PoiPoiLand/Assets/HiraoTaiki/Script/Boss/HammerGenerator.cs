using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerGenerator : MonoBehaviour
{
    [SerializeField] GameObject _hammerPrefab;
    List<HammerController> _hammers = new List<HammerController>();

    //左上、右上、左下、右上の4つ
    Vector3 leftUp = new Vector3(215, 20, -1.5f);//0番
    Vector3 rightUp = new Vector3(238, 20, -1.5f);//1番
    Vector3 leftDown = new Vector3(214, 20, -9.3f);//2番
    Vector3 rightDown = new Vector3(238, 20, -9.1f);//3番
    Vector3 center = new Vector3(222, 20, -5.1f);//4番
    Vector3 stage1Left = new Vector3(95.0f, 21.0f, 4.0f); // ステージ1用の位置
    Vector3 stage1Right = new Vector3(110.0f, 21.0f, 4.0f); // ステージ1用の位置





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// ハンマーが消えたときに自動生成(ボス関連でなんか渡す必要があったかもしれないが忘れた)、ハンマーがきえたときにこれを呼ぶ
    /// </summary>
    /// <param name="num"></param>
    public void HammerSpawn(int num)
    {
        Vector3 pos = Vector3.zero;
        switch (num)
        {
            case 0:
                pos = leftUp;
                break;
            case 1:
                pos = rightUp;
                break;
            case 2:
                pos = leftDown;
                break;
            case 3:
                pos = rightDown;
                break;
            case 4:
                pos = center;
                break;
            case 5:
                pos = stage1Left;
                break;
            case 6:
                pos = stage1Right;
                break;
            default:
                break;
        }
        
        GameObject obj = Instantiate(_hammerPrefab, pos, Quaternion.identity);
        HammerController _HC = obj.GetComponent<HammerController>();
        _HC.posNum = num;

        _hammers.Add(_HC);//List

        _HC.currentState = HammerState.pop;
    }
    

}
