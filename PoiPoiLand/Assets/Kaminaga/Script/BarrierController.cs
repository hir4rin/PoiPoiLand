using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{

    public GameObject[] Baria;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("PointNum") >= 1)
        {
            Baria[0].gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetInt("PointNum") >= 5)
        {
            Baria[1].gameObject.SetActive(false);
            Debug.Log("aaaaa");
        }
        



    }
}
