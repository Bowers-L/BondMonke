using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;


//InputSystem package is used for input
public class PlayerInputController : MonoBehaviour
{
    
    private PlayerController playerController;
    PlayerCamera playerCamera;
    private GameControls controls;
    private bool mapMovementToCircle = true;

    public Vector2 cameraInput;

    public float mouseX;
    public float mouseY;
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
        playerCamera = PlayerCamera.singleton;
        //Need to make sure in the script execution order that the GameManager comes BEFORE this.
        if (GameManager.Instance == null)
        {
            Debug.LogError("No instance of GameManager found");
        } else
        {
            controls = GameManager.Instance.controls;
        }
        controls.Player.Enable();

        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("Missing player controller component.");
        }

        //setup callbacks/actions associated with each control
        //Actions can be added/deleted by going under Assets/Input/PlayerControls and setting them in the UI.
        //They can also be added at runtime in code by using: var action = new InputAction("name", Binding: "<Configuration>/Binding");

        controls.Player.Movement.performed +=       ctx => Movement = GetMovement(ctx.ReadValue<Vector2>());
        controls.Player.Movement.canceled +=        ctx => Movement = Vector2.zero;

        controls.Player.Camera.performed +=         ctx => cameraInput = ctx.ReadValue<Vector2>();

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

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
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

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        if (playerCamera)
        {
            
            playerCamera.FollowTarget(delta);
            playerCamera.CameraRotation(delta, mouseX, mouseY);
        }
    }

    public void TickInput()
    {
        MouseUpdate();
    }

    private void MouseUpdate()
    {
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }


}
