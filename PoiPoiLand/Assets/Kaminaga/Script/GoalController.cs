using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ゴール");
            SoundManager.Instance.ChangeBGMClip(4); // ゲームクリアのBGMに変更
            SoundManager.Instance.PlayBGMWithCrossFade(2.0f);
            // ゴールしたらクリア画面に遷移
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
