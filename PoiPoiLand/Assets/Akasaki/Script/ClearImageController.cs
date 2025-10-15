using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearImageController : MonoBehaviour
{
    public Transform target; // 目標地点
    public float moveSpeed = 3f;
    public float rotationSpeed = 50f;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 回転
       transform.Rotate(Vector3.forward*rotationSpeed*Time.deltaTime);

        // 目標地点まで移動
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position,target.position)<0.01)
        {
            // 完全停止
            enabled = false;
        }
       

    }
}
