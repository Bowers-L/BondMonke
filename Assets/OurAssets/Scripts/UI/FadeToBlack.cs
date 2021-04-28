using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{

    public GameObject blackPanel;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FadeOutOnClick()
    {
        StartCoroutine(FadeOut());
    }
    public IEnumerator FadeOut(bool fadeOut = true, int fadeSpeed = 5)
    {
        Color panelColor = blackPanel.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeOut)
        {
            while (blackPanel.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = panelColor.a + (fadeSpeed * Time.deltaTime);
                panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, fadeAmount);
                blackPanel.GetComponent<Image>().color = panelColor;
                yield return null;
            }
        } else
        {
            while (blackPanel.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = panelColor.a - (fadeSpeed * Time.deltaTime);
                panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, fadeAmount);
                blackPanel.GetComponent<Image>().color = panelColor;
                yield return null;
            }
        }
    }
}
