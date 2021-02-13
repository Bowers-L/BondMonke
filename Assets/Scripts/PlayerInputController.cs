using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInputController : MonoBehaviour
{
    public float X
    {
        get;
        private set;
    }

    public float Y
    {
        get;
        private set;
    }



    public bool Light {
        get;
        private set;
    }

    public bool Heavy
    {
        get;
        private set;
    }

    public bool Interact
    {
        get;
        private set;
    }

    public bool Block
    {
        get;
        private set;
    }

    public bool Roll
    {
        get;
        private set;
    }

    private void Update()
    {
        Input.GetAxis("")
    }
}
