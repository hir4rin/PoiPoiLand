using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallState
{
    Ball
}



public class Ball : MonoBehaviour
{

    public bool isColHit = false;
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
        if (!other.CompareTag("Ground"))
        {
            isColHit = true;


        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ground"))
        {
            isColHit = true;


        }
        
    }
}
