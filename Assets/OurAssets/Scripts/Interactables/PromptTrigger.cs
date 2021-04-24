using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PromptTrigger: MonoBehaviour
{
    public string message;
    public PromptAndPopUpManager ui;


    private GameControls controls;
    private bool firstCollision = true;
    private bool playerInTrigger = false;

    /* Have one canvas in the game and enable/disable it rather than creating new ones. (reduces the possibility of overlapping UIs, which we don't want)
    public GameObject promptUIPrefab;

    private GameObject promptInstance;
    */

    // Start is called before the first frame update

    public void Awake()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("Game Manager Not Found (might need to change script execution order)");
        } else
        {
            controls = GameManager.Instance.controls;
        }

        ui = GameObject.Find("PromptAndPopUp").GetComponent<PromptAndPopUpManager>();
        if (ui == null)
        {
            Debug.LogError("Could not find PromptAndPopUp");
        }

        firstCollision = true;
    }

    public void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            ui.switchCanvas();
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            if (firstCollision)
            {
                firstCollision = false;
                ui.setPopUpText(message);
                ui.enablePopUp();
            } else
            {
                ui.enablePrompt();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            ui.disablePrompt();
            ui.disablePopUp();
        }
    }
    
}
