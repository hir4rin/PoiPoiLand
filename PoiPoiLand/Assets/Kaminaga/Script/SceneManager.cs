using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); // escキーでゲーム終了
        }

        // シーン遷移条件
        // 条件は今後変更予定
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SelectScene"); // spaceキーでセレクト画面に遷移
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene"); // gキーでゲーム画面に遷移
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene"); // hキーでタイトル画面に遷移
        }
    }

    public void SceneChange(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    //public void GameStart()
    //{
    //    UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    //}
}
