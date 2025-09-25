using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ƒS[ƒ‹");
            // ƒS[ƒ‹‚µ‚½‚çƒNƒŠƒA‰æ–Ê‚É‘JˆÚ
            UnityEngine.SceneManagement.SceneManager.LoadScene("ClearScene");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
