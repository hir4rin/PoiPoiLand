using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Test_1 : MonoBehaviour
{
    Rigidbody rb;
    Vector3 pos;
    Vector3 up;
    Vector3 x;
    Vector3 y;
    Vector3 z;
    int testCount;
    int hitCount;
    float test;
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
        rb = GetComponent<Rigidbody>();
        up = new Vector3(0.0f, -0.05f, 0.10f);
        x = new Vector3(0.30f, 0.0f, 0.0f);
        y = new Vector3(0.0f,0.30f, 0.0f);
        z = new Vector3(0.0f, 0.0f, 0.30f);
        testCount = 0;
        hitCount = 0;
        test = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(x, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(y, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(z, ForceMode.Impulse);
        }
        //testCount++;
        //if (testCount >= 120)
        //{
        //    this.transform.position += pos;
        //    testCount = 0;
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    pos.y -= 1.0f;
        //}
        //pos.z += Input.GetAxis("Vertical");
        //pos.x += Input.GetAxis("Horizontal");
        //transform.position = pos;
    }
}
