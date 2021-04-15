using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCollectableUi : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<BallCollector>().final)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            player.GetComponent<PlayerInputController>().enabled = false;
            Time.timeScale = 0f;
        }
    }
}
