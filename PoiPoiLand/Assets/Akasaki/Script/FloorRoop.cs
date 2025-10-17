using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorRoop : MonoBehaviour
{
    private float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(Time.deltaTime * speed, 0, 0);
        
        if (transform.position.x <= -15f)
        {
            transform.position = new Vector3(6, -2, -8);
        }
    }
}
