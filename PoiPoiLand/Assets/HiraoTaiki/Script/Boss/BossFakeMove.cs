using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFakeMove : MonoBehaviour
{


    //•‚—VŠ´
    float floatAmplitudeY = 1.0f; // U•
    float floatFrequencyY = 1.4f; // ü”g”(ã‰º‚Ì‘¬‚³)
    float floatFrequencyX = 0.7f;//flaotFrequencyY‚Ì”¼•ª‚Ì‘¬‚³(‘å‘Ì)
    float floatAmplitudeX = 1.0f; // U•(‘å‘Ì)

    //—h‚ê‚Ì’†S
    float centerX;
    float centerY;

    //‰ŠúˆÊ’u‚Ì”{—¦
    float Yura = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        centerY = transform.localPosition.y * Yura;
        centerX = transform.localPosition.x * Yura;
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //•‚—VŠ´
        float newY = centerY + Mathf.Sin(Time.time * floatFrequencyY) * floatAmplitudeY;
        float newX = centerX + Mathf.Sin(Time.time * floatFrequencyX) * floatAmplitudeX;
        transform.localPosition = new Vector3(newX, newY, transform.localPosition.z);
    }
}
