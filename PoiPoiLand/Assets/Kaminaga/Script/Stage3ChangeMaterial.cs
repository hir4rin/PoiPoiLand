using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3ChangeMaterial : MonoBehaviour
{
    MeshRenderer[] rends;
    Material newMat;
    int materialIndex;
    // Start is called before the first frame update
    void Start()
    {
        rends = GetComponentsInChildren<MeshRenderer>();
        newMat = Resources.Load<Material>("Stage3_ChangeFloor");
        materialIndex = 7;
    }

    void ChangeMaterial(int index)
    {
        foreach (MeshRenderer renderer in rends)
        {
            Material[] mats = renderer.materials;
            mats[index] = newMat;
            renderer.materials = mats;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.P))
        {
            ChangeMaterial(materialIndex);
        }
    }
}
