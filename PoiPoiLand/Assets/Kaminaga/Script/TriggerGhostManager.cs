using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerGhostManager : MonoBehaviour
{
    [SerializeField] private GameObject stage1Manager;
    [SerializeField] private GameObject player;
    private Stage1Manager manager;
    private Vector3 ghostMove;
    private Vector3 firstPos;
    private Vector3 maxPos;
    private Vector3 minPos;
    private Vector3 lookPlayer;
    private bool isRight;
    // Start is called before the first frame update
    void Start()
    {
        manager = stage1Manager.GetComponent<Stage1Manager>();
        ghostMove = new Vector3(0.01f, 0.0f, 0.0f);
        firstPos = this.transform.position;
        maxPos = firstPos + new Vector3(6.0f,0.0f,0.0f);
        minPos = firstPos - new Vector3(7.0f,0.0f,0.0f);
        lookPlayer = Vector3.zero;
        isRight = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lookPlayer = (player.transform.position - this.transform.position).normalized;
        lookPlayer.y = 0.0f;
        Quaternion rotation = Quaternion.LookRotation(lookPlayer);
        transform.rotation = rotation;
        if (transform.position.x >= maxPos.x)
        {
            transform.position = maxPos;
            isRight = false;
        }
        if (transform.position.x <= minPos.x)
        {
            transform.position = minPos;
            isRight = true;
        }

        if (manager.State == Stage1State.Wait)
        {
            
            if (isRight)
            {
                transform.position += ghostMove;
            }
            else
            {
                transform.position -= ghostMove;
            }
            if (Input.GetMouseButtonDown(0))
            {
                manager.State = Stage1State.Start;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hammer")
        {
            manager.State = Stage1State.Start;
            Destroy(this.gameObject);
        }
    }
}
