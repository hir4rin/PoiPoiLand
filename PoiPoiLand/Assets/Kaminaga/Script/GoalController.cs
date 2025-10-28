using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour
{
    [SerializeField] private GameObject goalEffect;
    private GameObject effectInstance;

    [SerializeField] Image whiteImage;//”’‚¢Image‚Å“o˜^
    float whiteDuration = 1f;//ˆÃ“]ŠÔ
    float fadeDuration = 2f;//ˆÃ“]ŠÔ
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ƒS[ƒ‹");
            StartCoroutine(whiteSequence());
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
                Debug.Log("‚¦‚Á‚Ó‚¥‚­‚Æ‚¤");
                effectInstance = Instantiate(goalEffect, this.transform.position, Quaternion.identity);
            }   
    }
    private IEnumerator whiteSequence()
    {
        yield return StartCoroutine(FadeOut2());

        yield return new WaitForSeconds(1f);
        SoundManager.Instance.ChangeBGMClip(5); // ƒQ[ƒ€ƒNƒŠƒA‚ÌBGM‚É•ÏX
        SoundManager.Instance.PlayBGMWithCrossFade(2.0f);
        // ƒS[ƒ‹‚µ‚½‚çƒNƒŠƒA‰æ–Ê‚É‘JˆÚ
        UnityEngine.SceneManagement.SceneManager.LoadScene("ClearScene");
    }

    private IEnumerator FadeOut2()
    {
        float elapsed = 0f;
        Color c = whiteImage.color;

        while (elapsed < (fadeDuration * 0.1f))
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / (fadeDuration * 0.1f)); // “§–¾¨”’
            whiteImage.color = c;
            yield return null;
        }
    }
}
