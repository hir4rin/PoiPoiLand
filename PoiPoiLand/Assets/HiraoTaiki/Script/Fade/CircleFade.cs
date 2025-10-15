using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFade : MonoBehaviour
{
    [SerializeField] private RectTransform maskRect;//CircleMaskÇäÑÇËìñÇƒ
    [SerializeField] float fadespeed = 0.5f;

    bool isFadingIn = false;
    bool isFadingOut = false;

  
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            FadeIn();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            FadeOut();
        }
    }
    private void FixedUpdate()
    {
        if (isFadingIn)
        {
            maskRect.localScale = Vector3.Lerp(maskRect.localScale, Vector3.zero, Time.fixedDeltaTime * fadespeed);
            if (maskRect.localScale.magnitude < 0.05f)
            {
                maskRect.localScale = Vector3.zero;
                isFadingIn = false;
            }
        }

        if (isFadingOut)
        {
            maskRect.localScale = Vector3.Lerp(maskRect.localScale, Vector3.one * 5f, Time.fixedDeltaTime * fadespeed);
            if (maskRect.localScale.magnitude > 2.8f)
            {
                maskRect.localScale = Vector3.one * 5f;
                isFadingOut = false;
            }
        }
    }
    public void FadeIn()  // ä€Ç™è¨Ç≥Ç≠ Å® âÊñ Ç™ñæÇÈÇ≠
    {
        maskRect.localScale = Vector3.one * 3f;
        isFadingIn = true;
        isFadingOut = false;
    }

    public void FadeOut() // ä€Ç™ëÂÇ´Ç≠ Å® âÊñ Ç™à√Ç≠
    {
        maskRect.localScale = Vector3.zero;
        isFadingOut = true;
        isFadingIn = false;
    }
}
