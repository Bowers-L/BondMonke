using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public GameObject FirstCanvas;
    public GameObject SecondCanvas;
    // Start is called before the first frame update
    public void switcher()
    {
        if (FirstCanvas.gameObject.activeInHierarchy == false)
        {
            FirstCanvas.gameObject.SetActive(true);
        }
        else
        {
            FirstCanvas.gameObject.SetActive(false);
        }
    }

}
