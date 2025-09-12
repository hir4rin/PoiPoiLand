using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Wキー（前方移動）
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * speed;
        }
        // Sキー（後方移動）
        if(Input.GetKey(KeyCode.S))
        {
            rb.velocity =-transform.forward * speed;
        }
        // Dキー（右移動）
        if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = transform.right * speed;
        }
        // Aキー（左移動）
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = -transform.right * speed;
        }
    }
}
