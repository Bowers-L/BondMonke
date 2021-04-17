using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    private GameObject player;
    public string sceneName;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player is not found");
        }
    }
    public void StartGame()
    {
        player.GetComponent<PlayerInputController>().enabled = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
