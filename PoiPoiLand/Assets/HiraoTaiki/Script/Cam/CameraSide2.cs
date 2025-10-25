using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSide2 : MonoBehaviour
{

    public CameraSwitcher _cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cam.SideChange2();
        }
    }

}
