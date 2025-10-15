using UnityEngine;

public class MakeTransparentAll : MonoBehaviour
{
    [Range(0f, 1f)] public float alpha = 0.5f;

    void Start()
    {
        SetTransparentRecursively(transform, alpha);
    }

    void SetTransparentRecursively(Transform obj, float alpha)
    {
        var renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            foreach (var mat in renderer.materials)
            {
                mat.SetFloat("_Mode", 3);
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;

                Color color = mat.color;
                color.a = alpha;
                mat.color = color;
            }
        }

        // 子オブジェクトも再帰的に処理
        foreach (Transform child in obj)
        {
            SetTransparentRecursively(child, alpha);
        }
    }
}
