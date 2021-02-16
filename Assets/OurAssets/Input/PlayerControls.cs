// GENERATED AUTOMATICALLY FROM 'Assets/OurAssets/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""6d522688-d50a-4737-abd3-e9f2eb27238d"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""5d67c160-3f9b-4748-9b45-44a4cabbc43e"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LightAttack"",
                    ""type"": ""Button"",
                    ""id"": ""f99540c8-022b-4559-a410-51ce89ce0dd1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""HeavyAttack"",
                    ""type"": ""Button"",
                    ""id"": ""196de06e-c96d-4e50-9414-17d76ace1f57"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""290b990f-2231-4996-a1e9-cf29f7b3b870"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""ef729377-4842-4a78-86ff-0f2956589c6a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""StopBlock"",
                    ""type"": ""Button"",
                    ""id"": ""99a9e73c-a008-4bae-9956-13b3604df79f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""45ac3fc2-45fd-4a87-8f85-43103d5bf634"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d9b8fe78-89a0-4170-aa36-fce3a0b8fd26"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""StopSprint"",
                    ""type"": ""Button"",
                    ""id"": ""cea0328e-9f1f-4aae-9c63-1f0b057c2b77"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d1157d59-0f28-4756-aa1b-361f5932cd35"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LightAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fa1fffd-322e-4abc-b44e-0112fda054e6"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""LightAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""54fdd440-5a5d-4893-b442-00377e04bce9"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""HeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e41d2504-d3b2-4af5-a009-dd86e1c3fb47"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""HeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8a83f4e-333b-45db-8e3e-5a9f7e0e29c0"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b73b2e4-ab24-42f2-9e5b-b18125753ec2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c3479de-1627-40b5-a92c-e58802e9d896"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f66a4b2-f621-4e97-af4e-bbbb41a5d801"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c1afb2df-0861-4c47-88a2-4902d05954b0"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41da2dd6-0891-4ed6-99a4-d6e9b838c35b"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Joystick"",
                    ""id"": ""82422ab1-ed4c-4cfe-8ab2-418b45df0fe2"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""91392944-5686-4a82-bb25-dd37695143ca"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f445204e-a0c9-4152-86b2-f52cc7c3f291"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f3a1932c-a996-4c0a-89db-142b62de7306"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4a0e0a13-b76f-4a05-a617-1f7ef1dead8e"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""f09c0502-20bf-40af-9e3b-2645fca206ef"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f0036989-444f-49a0-bebd-f23b8eca6381"",
                    ""path"": ""<Keyboard>/#(W)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a901c5d8-3d92-4f5f-a3be-bce812b20267"",
                    ""path"": ""<Keyboard>/#(S)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""630860e6-a639-4039-97cd-e9f7ac255c2d"",
                    ""path"": ""<Keyboard>/#(A)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""cb32fa35-2758-4475-8484-0e13bfd59495"",
                    ""path"": ""<Keyboard>/#(D)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bbaeaf84-aa91-4105-89df-cc0d661548da"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c46cab2-4afc-4c09-8067-485761b7973e"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae26dcd9-db2e-4220-9067-50e681c74628"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""StopSprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de182bb9-a3d0-4aa5-b664-e3b0ec76b654"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""StopSprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aec321c1-1b96-43b6-bbb5-542b1b9500aa"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""StopBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f32dd6a7-2cc7-4761-9cf1-62b8c9f1763a"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardAndMouse"",
                    ""action"": ""StopBlock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": []
        },
        {
            ""name"": ""KeyboardAndMouse"",
            ""bindingGroup"": ""KeyboardAndMouse"",
            ""devices"": []
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_LightAttack = m_Player.FindAction("LightAttack", throwIfNotFound: true);
        m_Player_HeavyAttack = m_Player.FindAction("HeavyAttack", throwIfNotFound: true);
        m_Player_Dodge = m_Player.FindAction("Dodge", throwIfNotFound: true);
        m_Player_Block = m_Player.FindAction("Block", throwIfNotFound: true);
        m_Player_StopBlock = m_Player.FindAction("StopBlock", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
        m_Player_StopSprint = m_Player.FindAction("StopSprint", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_LightAttack;
    private readonly InputAction m_Player_HeavyAttack;
    private readonly InputAction m_Player_Dodge;
    private readonly InputAction m_Player_Block;
    private readonly InputAction m_Player_StopBlock;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Sprint;
    private readonly InputAction m_Player_StopSprint;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @LightAttack => m_Wrapper.m_Player_LightAttack;
        public InputAction @HeavyAttack => m_Wrapper.m_Player_HeavyAttack;
        public InputAction @Dodge => m_Wrapper.m_Player_Dodge;
        public InputAction @Block => m_Wrapper.m_Player_Block;
        public InputAction @StopBlock => m_Wrapper.m_Player_StopBlock;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputAction @StopSprint => m_Wrapper.m_Player_StopSprint;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @LightAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLightAttack;
                @LightAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLightAttack;
                @LightAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLightAttack;
                @HeavyAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeavyAttack;
                @Dodge.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDodge;
                @Block.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBlock;
                @StopBlock.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStopBlock;
                @StopBlock.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStopBlock;
                @StopBlock.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStopBlock;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Sprint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @StopSprint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStopSprint;
                @StopSprint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStopSprint;
                @StopSprint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStopSprint;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @LightAttack.started += instance.OnLightAttack;
                @LightAttack.performed += instance.OnLightAttack;
                @LightAttack.canceled += instance.OnLightAttack;
                @HeavyAttack.started += instance.OnHeavyAttack;
                @HeavyAttack.performed += instance.OnHeavyAttack;
                @HeavyAttack.canceled += instance.OnHeavyAttack;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @StopBlock.started += instance.OnStopBlock;
                @StopBlock.performed += instance.OnStopBlock;
                @StopBlock.canceled += instance.OnStopBlock;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @StopSprint.started += instance.OnStopSprint;
                @StopSprint.performed += instance.OnStopSprint;
                @StopSprint.canceled += instance.OnStopSprint;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardAndMouseSchemeIndex = -1;
    public InputControlScheme KeyboardAndMouseScheme
    {
        get
        {
            if (m_KeyboardAndMouseSchemeIndex == -1) m_KeyboardAndMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardAndMouse");
            return asset.controlSchemes[m_KeyboardAndMouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnLightAttack(InputAction.CallbackContext context);
        void OnHeavyAttack(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnStopBlock(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnStopSprint(InputAction.CallbackContext context);
    }
}
