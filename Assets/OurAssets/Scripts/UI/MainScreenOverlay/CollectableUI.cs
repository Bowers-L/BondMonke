using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableUI : MonoBehaviour
{
    TextMeshProUGUI textObj;

    private void Awake()
    {
        textObj = GetComponentInChildren<TextMeshProUGUI>();
        if (textObj == null)
        {
            Debug.LogError("No text object attached to collectable UI.");
        }
    }

    public void SetCollectableScore(int value)
    {
        textObj.text = "" + value;
    }
}
