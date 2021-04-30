using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    public GameObject blackPanel;
    public string sceneName;
    private void Awake()
    {
    }
    public void StartGame()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut(bool fadeOut = true, int fadeSpeed = 2)
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
            SceneManager.LoadScene(sceneName);
        }
        else
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

    public void StartWithoutFade()
    {
        SceneManager.LoadScene(sceneName);
    }
}
