using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonfirePrompt : MonoBehaviour
{
    private GameObject player;
    public GameObject manager;
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

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().enteredBonfire)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            if (Input.GetKeyUp(KeyCode.E))
            {
                manager.GetComponent<manager>().switcher();
            }
        } else
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
        }
    }
}
