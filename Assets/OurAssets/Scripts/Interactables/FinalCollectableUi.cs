using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalCollectableUi : MonoBehaviour
{
    private GameObject player;
    private CanvasGroup canvasGroup;
    public TextMeshProUGUI bananaText;
    public TextMeshProUGUI enemiesText;
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
            bananaText.text = "Bananas Collected: " + player.GetComponent<BallCollector>().collectableCount + "/" + GameManager.Instance.totalCollectablesInScene;
            enemiesText.text = "Enemies Defeated: " + GameManager.Instance.uniqueEnemiesDefeated + "/" + GameManager.Instance.totalEnemiesInScene;

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            Cursor.visible = true;
            player.GetComponent<PlayerInputController>().enabled = false;
            Time.timeScale = 0f;
        }
    }
}
