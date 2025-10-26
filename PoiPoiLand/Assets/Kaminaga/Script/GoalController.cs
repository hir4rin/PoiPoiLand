using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField] private GameObject goalEffect;
    private GameObject effectInstance;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ゴール");
            SoundManager.Instance.ChangeBGMClip(5); // ゲームクリアのBGMに変更
            SoundManager.Instance.PlayBGMWithCrossFade(2.0f);
            // ゴールしたらクリア画面に遷移
            UnityEngine.SceneManagement.SceneManager.LoadScene("ClearScene");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        effectInstance = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            if (effectInstance == null)
            {
                Debug.Log("えっふぇくとう");
                effectInstance = Instantiate(goalEffect, this.transform.position, Quaternion.identity);
            }   
    }
}
