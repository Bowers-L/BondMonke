using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PromptTrigger: MonoBehaviour
{
    public string message;
    public GameObject promptUIPrefab;

    private GameObject promptInstance;

    // Start is called before the first frame update

    public void Awake()
    {
        
    }

    public void enableText()
    {
        Debug.Log("Called enable text");
        if (!promptInstance)
        {
            promptInstance = GameObject.Instantiate(promptUIPrefab);
        }

        promptInstance.GetComponentInChildren<Text>().text = message;
    }

    public void disableText()
    {
        GameObject.Destroy(promptInstance);
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
