using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class bonfire_ui : MonoBehaviour
{
    public GameObject manager;
    private GameObject player;
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("The component CanvasGroup is missing");
        }
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player is not found");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.E) && player.GetComponent<PlayerController>().enteredBonfire)
        {
            if (canvasGroup.interactable)
            {
                manager.GetComponent<manager>().switcher();
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                player.GetComponent<PlayerInputController>().enabled = true;
                Time.timeScale = 1f;
            }
            else
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                player.GetComponent<PlayerInputController>().enabled = false;
                Time.timeScale = 0f;
            }
        }
    }
}
