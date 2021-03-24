using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonfirePrompt : MonoBehaviour
{
    public GameObject m_player;
    public GameObject manager;
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("The component CanvasGroup is missing");
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_player.GetComponent<PlayerController>().enteredBonfire)
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
