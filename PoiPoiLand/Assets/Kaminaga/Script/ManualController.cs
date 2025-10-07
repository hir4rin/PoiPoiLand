using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ManualController : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject soundCanvas;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Image pageDisplay;
    public int pageNum;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        soundCanvas.SetActive(false);
        pageNum = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(pageNum);
        if (canvas.activeSelf)
        {
            pageDisplay.sprite = sprites[pageNum];
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnPreviousPage();
        }
        if( Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnNextPage();
        }

    }

    public void SetUI()
    {
        if (!soundCanvas.activeSelf)
        {
            canvas.SetActive(!canvas.activeSelf);
        }
    }

    public void SetSoundUI()
    {
        if (!canvas.activeSelf)
        {
            soundCanvas.SetActive(!soundCanvas.activeSelf);
        }
    }

    public void OnNextPage()
    {
        if(pageNum < sprites.Count - 1)
        {
            pageNum++;
        }
    }

    public void OnPreviousPage()
    {
        if (pageNum > 0)
        {
            pageNum--;
        }
    }
}
