using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject _stage1;
    [SerializeField] private GameObject _stage2;
    [SerializeField] private GameObject _stage3;
    // Start is called before the first frame update
    void Start()
    {
        _stage1.SetActive(false);
        _stage2.SetActive(false);
        _stage3.SetActive(false);
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
            _stage2.SetActive(true);
        }
        if (PlayerPrefs.GetInt("PointNum") == 5)
        {
            _stage3.SetActive(true);
        }
    }
}
