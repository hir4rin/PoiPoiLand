using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearController : MonoBehaviour
{
    public float delayTime = 5f;
    private bool canPress = false;

    public GameObject _message;
    bool Flash = false;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        _message.SetActive(false);
        Invoke("EnableInput", delayTime);//ŽžŠÔŽw’è
    }

    void EnableInput()
    {
        canPress = true;
        _message.SetActive(true);
        timer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
       
        if (_message != null)
        {
            if (timer > 0.8f)
            {
                Flash = !Flash;
                Debug.Log("•Ï‚í‚è‚Ü‚µ‚½");
                timer = 0;
            }
        }

        if (Flash)
        {
            _message.GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("aaaaaaaaa");
        }
        else
        {
            _message.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (canPress && Input.GetButtonDown("Submit"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");

        }

    }
}
