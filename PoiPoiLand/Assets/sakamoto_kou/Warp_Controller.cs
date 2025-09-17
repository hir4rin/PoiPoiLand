using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp_Controller : MonoBehaviour
{
    [SerializeField] private Transform destinationPortal;


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
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                {
                    controller.enabled = false; //ˆê“I‚É–³Œø‰»‚µ‚ÄˆÊ’u•ÏX
                }
            }
        }
    }
}
