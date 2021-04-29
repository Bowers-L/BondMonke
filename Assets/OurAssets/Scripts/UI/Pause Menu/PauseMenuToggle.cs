using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuToggle : MonoBehaviour
{
    public CanvasGroup pauseMenu;
    public Canvas controlView;
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

        controls.Player.Enable();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!controlView.enabled)
            {
                if (pauseMenu.interactable)
                {
                    //GameManager.Instance.menuOpen = false;
                    pauseMenu.interactable = false;
                    pauseMenu.blocksRaycasts = false;
                    pauseMenu.alpha = 0f;
                    Time.timeScale = 1f;
                    Cursor.visible = false;
                    controls.Player.Enable();
                }
                else
                {
                    if (Time.timeScale != 0f) //if game isn't already paused
                    {
                        //GameManager.Instance.menuOpen = true;
                        pauseMenu.interactable = true;
                        pauseMenu.blocksRaycasts = true;
                        pauseMenu.alpha = 1f;
                        Time.timeScale = 0f;
                        Cursor.visible = true;
                        controls.Player.Disable();
                    }
                }
            } else
            {
                controlView.enabled = false;
            }
        }
    }
}
