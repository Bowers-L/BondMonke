using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;


//Input System is used for input
public class PlayerInputController : MonoBehaviour
{
    public PlayerControls controls;
    public PlayerController playerController;

    private bool mapMovementToCircle = true;

    public Vector2 Movement
    {
        get;
        private set;
    }

    public bool LightAttack
    {
        get;
        private set;
    }

    public bool HeavyAttack
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

    public bool Sprint
    {
        get;
        private set;
    }

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Enable();

        //setup callbacks/actions associated with each control
        controls.Player.Movement.performed +=       ctx => Movement = GetMovement(ctx.ReadValue<Vector2>());
        controls.Player.Movement.canceled +=        ctx => Movement = Vector2.zero;

        controls.Player.LightAttack.performed +=    ctx => playerController.OnLightAttack();

        controls.Player.HeavyAttack.performed +=    ctx => playerController.OnHeavyAttack();

        controls.Player.Dodge.performed +=          ctx => playerController.OnDodge();

        controls.Player.Block.performed +=          ctx =>  Block = true;

        controls.Player.Sprint.performed +=         ctx => Sprint = true;
        controls.Player.Sprint.canceled +=          ctx => Sprint = false;
    }

    Vector2 GetMovement(Vector2 rawInput)
    {
        if (mapMovementToCircle)
        {
            //Map axes to unit circle
            return MapSquareToCircle(rawInput);
        } else
        {
            //stick with square input
            return rawInput;
        }
    }

    private Vector2 MapSquareToCircle(Vector2 squareInput)
    {
        //Dpad mapping from square to circle as demonstrated in Milestone 1
        //based on http://mathproofs.blogspot.com/2005/07/mapping-square-to-circle.html

        Vector2 circleInput;
        circleInput.x = squareInput.x * Mathf.Sqrt(1.0f - 0.5f * squareInput.y * squareInput.y);
        circleInput.y = squareInput.y * Mathf.Sqrt(1.0f - 0.5f * squareInput.x * squareInput.x);

        return circleInput;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
}
