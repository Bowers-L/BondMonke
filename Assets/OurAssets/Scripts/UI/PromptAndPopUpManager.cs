using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptAndPopUpManager : MonoBehaviour
{

    public CanvasGroup promptCanvas;
    public CanvasGroup popUpCanvas;
    public TextMeshProUGUI descriptionText;

    private PlayerInputController controls;

    void Awake()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("Game Manager Not Found (might need to change script execution order)");
        }
        else
        {
            controls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputController>();
        }

        if (promptCanvas == null || popUpCanvas == null)
        {
            CanvasGroup[] canvases = GetComponentsInChildren<CanvasGroup>();
            foreach (CanvasGroup canvas in canvases)
            {
                if (canvas.gameObject.name.CompareTo("Prompt") == 0)
                {
                    promptCanvas = canvas;
                } else if (canvas.gameObject.name.CompareTo("PopUp") == 0)
                {
                    popUpCanvas = canvas;
                } 
            }

            if (promptCanvas == null)
            {
                Debug.LogError("Prompt Canvas not found");
            }

            if (popUpCanvas == null)
            {
                Debug.LogError("PopUp Canvas not found");
            }
        }

        if (descriptionText == null)
        {
            TextMeshProUGUI[] textObjects = popUpCanvas.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI tObj in textObjects)
            {
                if (tObj.gameObject.name.CompareTo("Description") == 0)
                {
                    descriptionText = tObj;
                }
            }

            if (descriptionText == null)
            {
                Debug.LogError("Description text not found.");
            }
        }

        disablePrompt();
        disablePopUp();
    }

    public void switchCanvas()
    {
        Debug.Log("Switching Canvas");
        if (popUpCanvas.interactable)
        {
            disablePopUp();
            enablePrompt();
        } else
        {
            disablePrompt();
            enablePopUp();
        }
    }

    public void enablePrompt()
    {
        Debug.Log("EnablingPrompt");
        promptCanvas.interactable = true;
        promptCanvas.blocksRaycasts = true;
        promptCanvas.alpha = 1f;
    }

    public void disablePrompt()
    {
        promptCanvas.interactable = false;
        promptCanvas.blocksRaycasts = false;
        promptCanvas.alpha = 0f;
    }

    public void enablePopUp()
    {
        //if (!GameManager.Instance.menuOpen)
        //{
        //GameManager.Instance.menuOpen = true;
        popUpCanvas.interactable = true;
        popUpCanvas.blocksRaycasts = true;
        popUpCanvas.alpha = 1f;
        Time.timeScale = 0f;
        Cursor.visible = true;
        controls.enabled = false;
        //}
    }

    public void disablePopUp()
    {
        //GameManager.Instance.menuOpen = false;
        popUpCanvas.interactable = false;
        popUpCanvas.blocksRaycasts = false;
        popUpCanvas.alpha = 0f;
        Time.timeScale = 1f;
        Cursor.visible = false;
        controls.enabled = true;
    }

    /*
    public void setPopUpText(string text)
    {
        descriptionText.text = text;
    }
    */
}
