using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    public float daySpeed = 5.0f; // 1“ú‚Ìi‚İ‹ï‡i¬‚³‚¢‚Ù‚Ç‚ä‚Á‚­‚èj

    void Update()
    {
        // X²‰ñ“]‚Å‘¾—z‚ª‹ó‚ğ“®‚­
        transform.Rotate(Vector3.right * daySpeed * Time.deltaTime);
    }
}
