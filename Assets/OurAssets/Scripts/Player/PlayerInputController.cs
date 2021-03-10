using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;


//InputSystem package is used for input
public class PlayerInputController : MonoBehaviour
{
    
    public PlayerController playerController;

    private GameControls controls;
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

    private void Start()    //Start instead of Awake just to make sure the game manager has called its own Awake
    {
        controls = GameManager.Instance.controls;
        if (controls == null)
        {
            Debug.LogError("Could not find GameManager instance from PlayerInputController script.");
        }
        controls.Player.Enable();

        //setup callbacks/actions associated with each control
        //Actions can be added/deleted by going under Assets/Input/PlayerControls and setting them in the UI.
        //They can also be added at runtime in code by using: var action = new InputAction("name", Binding: "<Configuration>/Binding");

        controls.Player.Movement.performed +=       ctx => Movement = GetMovement(ctx.ReadValue<Vector2>());
        controls.Player.Movement.canceled +=        ctx => Movement = Vector2.zero;

        controls.Player.LightAttack.performed +=    ctx => playerController.OnLightAttack();

        controls.Player.HeavyAttack.performed +=    ctx => playerController.OnHeavyAttack();

        controls.Player.Dodge.performed +=          ctx => playerController.OnDodge();

        controls.Player.Interact.performed +=        ctx => playerController.OnInteract();

        //Im guessing there's a way to do these held actions more elegantly with only one action, but I haven't found it.
        controls.Player.Block.performed +=          ctx => Block = true;
        controls.Player.StopBlock.performed +=      ctx => Block = false;

        controls.Player.Sprint.performed +=         ctx => Sprint = true;
        controls.Player.StopSprint.performed +=     ctx => Sprint = false;
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
