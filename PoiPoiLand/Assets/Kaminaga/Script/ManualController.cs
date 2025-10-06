using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualController : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image page1;
    [SerializeField] private Image page2;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            page1.enabled = false;
        }
        if(Input.GetKeyDown(KeyCode.S))
            { page2.enabled = false; }
    }

    public void SetUI()
    {
        canvas.SetActive(true);
    }
}
