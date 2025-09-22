using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーの位置をロードする
        if(PlayerPrefs.HasKey("PlayerX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");
            transform.position = new Vector3(x, y, z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
