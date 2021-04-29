using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIEventManager : MonoBehaviour
{
    private UnityAction respawnListener;
    private UnityAction<bool> fadeListener;
    public GameObject blackPanel;
    public float fadeSpeed = 1;
    public float delay = 0.5f;

    void Awake()
    {
        fadeListener = new UnityAction<bool>(fadeHandler);
        respawnListener = new UnityAction(respawnHandler);
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
        EventManager.StartListening<RespawnEvent>(respawnListener);
    }
    private void OnDisable()
    {
        EventManager.StopListening<FadeEvent, bool>(fadeListener);
        EventManager.StopListening<RespawnEvent>(respawnListener);
    }

    void respawnHandler()
    {
        //Debug.Log("Handled fade");
        StartCoroutine(Respawn());
    }
    public IEnumerator Respawn()
    {
        Color panelColor = blackPanel.GetComponent<Image>().color;
        float fadeAmount;

        while (blackPanel.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = panelColor.a + (fadeSpeed * Time.deltaTime);
            panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, fadeAmount);
            blackPanel.GetComponent<Image>().color = panelColor;
            yield return null;
        }
        yield return new WaitForSeconds(delay);
        while (blackPanel.GetComponent<Image>().color.a > 0)
        {
            fadeAmount = panelColor.a - (fadeSpeed * Time.deltaTime);
            panelColor = new Color(panelColor.r, panelColor.g, panelColor.b, fadeAmount);
            blackPanel.GetComponent<Image>().color = panelColor;
            yield return null;
        }
    }


    void fadeHandler(bool fadeDir)
    {
        //Debug.Log("Handled fade in/out");
        StartCoroutine(FadeIO(fadeDir));
    }
    public IEnumerator FadeIO(bool fadeOut = true)
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
        }
        else {
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
