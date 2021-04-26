using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFader : MonoBehaviour
{
    public float speed = 0.01f; // 0.6 looks good on xanders computer
    Color startColor;
    float startAlpha;
    float startTime;

    void OnEnable()
    {
        //Debug.Log("i was enabled");
        startTime = Time.time;
        startColor = GetComponent<Renderer>().material.color;

        //Make sure the fade always starts at 1
        startAlpha = 1.0f;
        GetComponent<Renderer>().material.color = new Color(startColor.r, startColor.g, startColor.b, startAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        Utility.SwitchRenderMode(GetComponent<Renderer>().material, Utility.RenderingModes.Fade);
        if (GetComponent<Renderer>().material.color.a != 0)
        {
            //Debug.Log("Fading dead enemy");
            float t = (Time.time - startTime) * speed;
            GetComponent<Renderer>().material.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1, 0, t * speed));
        } else
        {
            //Debug.Log("Enemy faded out");
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    //Moved this to Utility
    /*
    void SwitchRenderModeToFade(Material mat)
    {
        mat.SetInt("_SrcBlend", (int) BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int) BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
    }
    */
}
