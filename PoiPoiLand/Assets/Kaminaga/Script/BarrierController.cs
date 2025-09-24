using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("PointNum") >= 1)
        {
            this.gameObject.SetActive(false);

        }
        else
        {
            this.gameObject.SetActive(true);
        }



    }
}
