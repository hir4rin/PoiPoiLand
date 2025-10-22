using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearController : MonoBehaviour
{
    public float delayTime = 5f;
    private bool canPress = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("EnableInput", delayTime);//ŽžŠÔŽw’è
    }

    void EnableInput()
    {
        canPress = true;
    }

    // Update is called once per frame
    void Update()
    {
        if( canPress&& Input.GetButtonDown("Submit"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");

        }
    }
}
