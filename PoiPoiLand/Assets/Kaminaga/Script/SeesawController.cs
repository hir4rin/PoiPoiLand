using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawController : MonoBehaviour
{
    private float temp;
    // Start is called before the first frame update
    void Start()
    {
        temp = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSeesaw();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //temp += 10.0f;
        }
    }

    private void MoveSeesaw()
    {
        this.transform.rotation = Quaternion.AngleAxis(temp, Vector3.right);
    }
}
