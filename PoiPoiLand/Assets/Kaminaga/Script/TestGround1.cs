using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGround1 : MonoBehaviour
{

    Rigidbody rb;
    Vector3 pos;
    Vector3 firstPos;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        firstPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var hori = Input.GetAxisRaw("Horizontal");
        var vert = Input.GetAxisRaw("Vertical");
        Vector3 moveVector = new Vector3(hori, 0, vert);

        rb.AddForce(moveVector, ForceMode.Impulse);

    }
}
