using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private GameControls controls;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        //Need to make sure in the script execution order that the GameManager comes BEFORE this.
        if (GameManager.Instance == null)
        {
            Debug.LogError("No instance of GameManager found");
        }
        else
        {
            controls = GameManager.Instance.controls;
        }

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found");
        }


        controls.Player.Enable();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("pause menu pressed");
            if (canvasGroup.interactable)
            {
                //GameManager.Instance.menuOpen = false;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                Time.timeScale = 1f;
                Cursor.visible = false;
                controls.Player.Enable();
            }
            else
            {
                //if (!GameManager.Instance.menuOpen)
                //{
                    //GameManager.Instance.menuOpen = true;
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    canvasGroup.alpha = 1f;
                    Time.timeScale = 0f;
                    Cursor.visible = true;
                    controls.Player.Disable();
                //}
            }
        }
    }
}
