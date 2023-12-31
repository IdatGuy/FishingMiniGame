//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Settings/Input/GameInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @GameInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""442ffe83-90a7-4f0b-ac15-d49b39a975cd"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""3621267a-8173-4ac6-8e4d-e3419c672cce"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""2f9736ad-8e3a-415e-9241-e8a424de1f6d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""8285c04a-bc03-444c-9af5-dc9e0e0b8b8d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""8456de8f-dce8-4e63-b0fe-434b24407b6e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""eb10ede4-1efb-4fe2-8e9e-ee93c8c98e69"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SpawnBaitOne"",
                    ""type"": ""Value"",
                    ""id"": ""83454d44-a9fa-4bd9-80e8-7ca345af24b0"",
                    ""expectedControlType"": ""Integer"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SpawnBaitTwo"",
                    ""type"": ""Button"",
                    ""id"": ""73101da2-813b-44a2-aef2-e8fabb16f703"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SpawnBaitThree"",
                    ""type"": ""Button"",
                    ""id"": ""7b905e8d-fb40-4c3c-ae71-924c7ec4bf11"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SpawnBaitFour"",
                    ""type"": ""Button"",
                    ""id"": ""61435a06-9489-4b90-9b86-d91f7f2e20a6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard WASD"",
                    ""id"": ""d6768174-b93f-446e-9710-78a8d2dfd427"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3738a766-32d4-4c10-9870-7e6dfbd81164"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b3f8d3ff-dbe0-4af7-b13e-449c93e1509a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c30c8505-8b28-4d20-a922-9efd573b4c81"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5ec378d2-534e-4af8-b57d-03e90fb97926"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2ea8f725-820a-454e-8cf9-ae9a780b48e0"",
                    ""path"": ""<DualShockGamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS5"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""03eb4ecc-23ca-4e7f-9625-bcc8bb3aab35"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1880c518-e2d0-4f82-96aa-4027719ac6b4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f37fa948-114c-4a33-86c4-06798d2aa1d4"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f8bab06-af03-4228-a2a4-cf18e6213b9c"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpawnBaitOne"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36ab1b4d-ce35-45cd-b3a5-42209a3f1d65"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8cddab83-4238-43d0-8546-dc55a721c9f3"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpawnBaitTwo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a60cb8c2-5c6b-4f82-af10-929cb3009b8c"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpawnBaitThree"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""feabd02d-676c-417c-865c-8eb0cb902954"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpawnBaitFour"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
            ""devices"": []
        },
        {
            ""name"": ""PS5"",
            ""bindingGroup"": ""PS5"",
            ""devices"": []
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Attack = m_Gameplay.FindAction("Attack", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Aim = m_Gameplay.FindAction("Aim", throwIfNotFound: true);
        m_Gameplay_Look = m_Gameplay.FindAction("Look", throwIfNotFound: true);
        m_Gameplay_SpawnBaitOne = m_Gameplay.FindAction("SpawnBaitOne", throwIfNotFound: true);
        m_Gameplay_SpawnBaitTwo = m_Gameplay.FindAction("SpawnBaitTwo", throwIfNotFound: true);
        m_Gameplay_SpawnBaitThree = m_Gameplay.FindAction("SpawnBaitThree", throwIfNotFound: true);
        m_Gameplay_SpawnBaitFour = m_Gameplay.FindAction("SpawnBaitFour", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Attack;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Aim;
    private readonly InputAction m_Gameplay_Look;
    private readonly InputAction m_Gameplay_SpawnBaitOne;
    private readonly InputAction m_Gameplay_SpawnBaitTwo;
    private readonly InputAction m_Gameplay_SpawnBaitThree;
    private readonly InputAction m_Gameplay_SpawnBaitFour;
    public struct GameplayActions
    {
        private @GameInput m_Wrapper;
        public GameplayActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Attack => m_Wrapper.m_Gameplay_Attack;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Aim => m_Wrapper.m_Gameplay_Aim;
        public InputAction @Look => m_Wrapper.m_Gameplay_Look;
        public InputAction @SpawnBaitOne => m_Wrapper.m_Gameplay_SpawnBaitOne;
        public InputAction @SpawnBaitTwo => m_Wrapper.m_Gameplay_SpawnBaitTwo;
        public InputAction @SpawnBaitThree => m_Wrapper.m_Gameplay_SpawnBaitThree;
        public InputAction @SpawnBaitFour => m_Wrapper.m_Gameplay_SpawnBaitFour;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Attack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Aim.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                @Look.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @SpawnBaitOne.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpawnBaitOne;
                @SpawnBaitOne.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpawnBaitOne;
                @SpawnBaitOne.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpawnBaitOne;
                @SpawnBaitTwo.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpawnBaitTwo;
                @SpawnBaitTwo.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpawnBaitTwo;
                @SpawnBaitTwo.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpawnBaitTwo;
                @SpawnBaitThree.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpawnBaitThree;
                @SpawnBaitThree.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpawnBaitThree;
                @SpawnBaitThree.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpawnBaitThree;
                @SpawnBaitFour.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpawnBaitFour;
                @SpawnBaitFour.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpawnBaitFour;
                @SpawnBaitFour.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpawnBaitFour;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @SpawnBaitOne.started += instance.OnSpawnBaitOne;
                @SpawnBaitOne.performed += instance.OnSpawnBaitOne;
                @SpawnBaitOne.canceled += instance.OnSpawnBaitOne;
                @SpawnBaitTwo.started += instance.OnSpawnBaitTwo;
                @SpawnBaitTwo.performed += instance.OnSpawnBaitTwo;
                @SpawnBaitTwo.canceled += instance.OnSpawnBaitTwo;
                @SpawnBaitThree.started += instance.OnSpawnBaitThree;
                @SpawnBaitThree.performed += instance.OnSpawnBaitThree;
                @SpawnBaitThree.canceled += instance.OnSpawnBaitThree;
                @SpawnBaitFour.started += instance.OnSpawnBaitFour;
                @SpawnBaitFour.performed += instance.OnSpawnBaitFour;
                @SpawnBaitFour.canceled += instance.OnSpawnBaitFour;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_PS5SchemeIndex = -1;
    public InputControlScheme PS5Scheme
    {
        get
        {
            if (m_PS5SchemeIndex == -1) m_PS5SchemeIndex = asset.FindControlSchemeIndex("PS5");
            return asset.controlSchemes[m_PS5SchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnSpawnBaitOne(InputAction.CallbackContext context);
        void OnSpawnBaitTwo(InputAction.CallbackContext context);
        void OnSpawnBaitThree(InputAction.CallbackContext context);
        void OnSpawnBaitFour(InputAction.CallbackContext context);
    }
}
