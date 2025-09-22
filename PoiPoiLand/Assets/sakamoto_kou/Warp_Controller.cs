using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp_Controller : MonoBehaviour
{
    public GameObject player;
    /// <summary>
    /// ÉèÅ[ÉvêÊ
    /// </summary>
    private Vector3 warpPoint = new Vector3(0.0f,0.63f, -7.417f);

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
        if(other.CompareTag("Player"))
        {
            player.transform.position = warpPoint;
        }
    }
}
