using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject _stage1;
    // Start is called before the first frame update
    void Start()
    {
        _stage1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("PointNum") == 1)
        {
            _stage1.SetActive(true);
        }
    }
}
