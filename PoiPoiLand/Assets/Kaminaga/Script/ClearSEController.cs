using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSEController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> clearSEList;
    private bool isPlayed;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayed)
        {
            ChangeSE(0);
            SoundManager.Instance.PlaySE(audioSource);
            ChangeSE(1);
            SoundManager.Instance.PlaySE(audioSource);
            isPlayed = true;
        }
    }

    private void ChangeSE(int index)
    {
        audioSource.clip = clearSEList[index];
    }
}
