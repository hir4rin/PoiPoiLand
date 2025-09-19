using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HammerController;

public class Hammer_PrefabDirector : MonoBehaviour
{
    private float time = 0.0f;
    public GameObject hummerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug_sakamoto();
    }

    private void Debug_sakamoto()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Instantiate(hummerPrefab);
        }
    }
}
