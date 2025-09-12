using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Test_1 : MonoBehaviour
{
    Vector3 pos;
    int testCount;
    int hitCount;
    private void OnCollisionEnter(Collision collision)
    {
        hitCount++;
        Debug.Log(hitCount);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("—£‚ê‚½");
    }
    // Start is called before the first frame update
    void Start()
    {
        //pos = new Vector3(0.0f,transform.position.y,0.0f);
        testCount = 0;
        hitCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        testCount++;
        if (testCount >= 120)
        {
            pos.x = 2.8f;
            pos.y = 4.0f;
            this.transform.position = pos;
            testCount = 0;
        }
        //pos.z += Input.GetAxis("Vertical");
        //pos.x += Input.GetAxis("Horizontal");
        //transform.position = pos;
    }
}
