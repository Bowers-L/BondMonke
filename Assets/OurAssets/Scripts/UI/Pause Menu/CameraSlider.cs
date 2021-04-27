using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSlider : MonoBehaviour
{
    public PlayerCamera playerCamera;
    public Text myText;
    public Slider mySlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = "Current Mouse Sensitivity: " + mySlider.value + "%";
    }
    public void setLevel (float sliderValue)
    {
        playerCamera.GetComponent<PlayerCamera>().mouseSensitivity = .005f * (mySlider.value / 20);
    }
}
