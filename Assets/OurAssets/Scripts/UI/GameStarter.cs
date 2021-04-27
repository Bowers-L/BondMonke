using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public string sceneName;
    private void Awake()
    {
    }
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
