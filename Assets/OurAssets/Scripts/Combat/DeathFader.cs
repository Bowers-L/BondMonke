using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFader : MonoBehaviour
{
    public float speed = 0.01f; // 0.6 looks good on xanders computer
    Color startAlpha;
    float startTime;
    void OnEnable()
    {
        Debug.Log("i was enabled");
        startTime = Time.time;
        SwitchRenderModeToFade(GetComponent<Renderer>().material);
        startAlpha = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Renderer>().material.color.a != 0)
        {
            Debug.Log("Fading dead enemy");
            float t = (Time.time - startTime) * speed;
            GetComponent<Renderer>().material.color = new Color(startAlpha.r, startAlpha.g, startAlpha.b, Mathf.Lerp(1, 0, t * speed));
        } else
        {
            Debug.Log("Enemy faded out");
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    //https://answers.unity.com/questions/1004666/change-material-rendering-mode-in-runtime.html
    void SwitchRenderModeToFade(Material mat)
    {
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
    }
}
