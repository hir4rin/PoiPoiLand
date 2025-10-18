using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash2 : MonoBehaviour
{


    public GameObject[] panels;
    public float interval = 2f;//切り替え間隔
    float warningTime = 0.5f;//消える前に色を変える時間


    public Color warningColor = Color.red;

    float timer;
    bool toggleState = false; //今の状態

    Dictionary<GameObject, Color[]> originalColors = new Dictionary<GameObject, Color[]>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var key in panels)
        {
            if (key == null) continue;

            var renderer = key.GetComponent<Renderer>();

            if (renderer != null)
            {
                var mats = renderer.materials;
                Color[] colors = new Color[mats.Length];
                for (int i = 0; i < mats.Length; i++)
                {
                    colors[i] = mats[i].color;
                }
                originalColors[key] = colors;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;


        //消える直前の時間を過ぎたら色を変える
        if (interval - timer <= warningTime)
        {
            foreach (var key in panels)
            {
                if (key == null || !key.activeSelf) continue;

                var renderer = key.GetComponent<Renderer>();
                if (renderer != null)
                {
                    foreach (var mat in renderer.materials)
                    {
                        mat.color = warningColor;
                    }
                }
            }
        }
        else
        {
            //それ以外の時間は元の色に戻す
            foreach (var key in panels)
            {
                if (key != null && key.activeSelf)
                {
                    var renderer = key.GetComponent<Renderer>();
                    if (renderer != null && originalColors.ContainsKey(key))
                    {

                        var mats = renderer.materials;
                        var colors = originalColors[key];

                        for (int i = 0; i < mats.Length && i < colors.Length; i++)
                        {
                            mats[i].color = colors[i];
                        }
                    }
                }
            }

        }

        if (timer >= interval)
        {
            timer = 0;
            toggleState = !toggleState;

            //
            foreach (var key in panels)
            {
                if (key != null)
                {
                    key.SetActive(toggleState);
                }
            }
        }
    }
}
