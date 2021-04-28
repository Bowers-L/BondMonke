using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIEventManager : MonoBehaviour
{

    private UnityAction<bool> fadeListener;
    public GameObject blackPanel;
    public float fadeSpeed = 1;

    void Awake()
    {
        fadeListener = new UnityAction<bool>(fadeHandler);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        EventManager.StartListening<FadeEvent, bool>(fadeListener);
    }
    private void OnDisable()
    {
        EventManager.StopListening<FadeEvent, bool>(fadeListener);
    }

    void fadeHandler(bool fadeDir)
    {
        Debug.Log("Handled fade");
        StartCoroutine(FadeOut(fadeDir));
    }
    public IEnumerator FadeOut(bool fadeOut = true)
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
            yield return new WaitForSeconds(1);
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
