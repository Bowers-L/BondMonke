using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptTrigger: MonoBehaviour
{
    public string message;
    public GameObject promptUIPrefab;

    private Text textComponent;

    // Start is called before the first frame update

    public void Awake()
    {
        textComponent = promptUIPrefab.GetComponent<Text>();
    }

    public void enableText()
    {
        promptUIPrefab.GetComponentInChildren<Text>();

    }

    public void disableText()
    {

    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {

    }
    */
}
