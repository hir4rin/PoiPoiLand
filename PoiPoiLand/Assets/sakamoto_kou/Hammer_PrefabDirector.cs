using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer_PrefabDirector : MonoBehaviour
{
    private float time = 0.0f;
    public GameObject hummerPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ŠÔ‚ğXV
        time++;

        if(time > 60.0f)
        {
            Instantiate(hummerPrefab);
            time = 0.0f;
        }
    }
}
