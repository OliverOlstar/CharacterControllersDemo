// GENERATED AUTOMATICALLY FROM 'Assets/_SCR/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""SimpleCar"",
            ""id"": ""613b71e8-d519-456e-981b-716cd641e0ea"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e7bf5bd3-35bb-4de7-9358-1bcc2ffa4efe"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drift"",
                    ""type"": ""Button"",
                    ""id"": ""90190d30-277d-4528-8a4f-1ec89d0642f8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""38c6c936-43b7-4432-94fa-16a8c87f5b91"",
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
                    ""id"": ""f38b2134-f9ff-47ce-ab02-1431b57d6170"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3dc98ece-3c76-4aff-bb3f-f05d3e82d6d8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0a7e099f-3a61-429c-ac23-2b9c0b13ec1b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""06d3a484-e45e-4f38-887b-58b338ba6a16"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d3bb9a61-683e-46da-86e3-ee1575786454"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ede6f66-af16-4d1d-8f5a-07fc8ce77c4a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Drift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6896bc30-90da-4e68-95f2-b1b40eb56d6a"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Drift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Link"",
            ""id"": ""bc355be7-5207-4369-bdfa-c9b14493fccb"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""883e3417-553d-4bc5-8527-670178491f7e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""e24591cf-3d0b-4c14-8efb-183116035239"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""0cfe2365-b764-463a-bc79-3662ba4c5982"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3d1dd41a-311a-4230-8964-990244fb44f8"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""a8ae51cd-9a5d-436b-a953-da5d0dd576d6"",
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
                    ""id"": ""310dd839-304a-4ce1-8da2-7b814e72245d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""dbe514ac-d1c6-4e71-808f-9adbb2fe4401"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9f8f4148-7740-4561-9e77-b852553128fe"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bdf0a6f3-50de-4fc0-a913-f18c23af2b3c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""35354cd7-a469-4b5e-8a39-3462a460756c"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99799c37-b9dd-4ff1-927c-da08cb0d593e"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3290aeef-6b49-4ff9-ad23-9ab5dd4fd021"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b360bd6-1ab7-4574-8f78-342ebc28cd63"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // SimpleCar
        m_SimpleCar = asset.FindActionMap("SimpleCar", throwIfNotFound: true);
        m_SimpleCar_Move = m_SimpleCar.FindAction("Move", throwIfNotFound: true);
        m_SimpleCar_Drift = m_SimpleCar.FindAction("Drift", throwIfNotFound: true);
        // Link
        m_Link = asset.FindActionMap("Link", throwIfNotFound: true);
        m_Link_Move = m_Link.FindAction("Move", throwIfNotFound: true);
        m_Link_Crouch = m_Link.FindAction("Crouch", throwIfNotFound: true);
        m_Link_Roll = m_Link.FindAction("Roll", throwIfNotFound: true);
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

    // SimpleCar
    private readonly InputActionMap m_SimpleCar;
    private ISimpleCarActions m_SimpleCarActionsCallbackInterface;
    private readonly InputAction m_SimpleCar_Move;
    private readonly InputAction m_SimpleCar_Drift;
    public struct SimpleCarActions
    {
        private @PlayerInput m_Wrapper;
        public SimpleCarActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_SimpleCar_Move;
        public InputAction @Drift => m_Wrapper.m_SimpleCar_Drift;
        public InputActionMap Get() { return m_Wrapper.m_SimpleCar; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SimpleCarActions set) { return set.Get(); }
        public void SetCallbacks(ISimpleCarActions instance)
        {
            if (m_Wrapper.m_SimpleCarActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_SimpleCarActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_SimpleCarActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_SimpleCarActionsCallbackInterface.OnMove;
                @Drift.started -= m_Wrapper.m_SimpleCarActionsCallbackInterface.OnDrift;
                @Drift.performed -= m_Wrapper.m_SimpleCarActionsCallbackInterface.OnDrift;
                @Drift.canceled -= m_Wrapper.m_SimpleCarActionsCallbackInterface.OnDrift;
            }
            m_Wrapper.m_SimpleCarActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Drift.started += instance.OnDrift;
                @Drift.performed += instance.OnDrift;
                @Drift.canceled += instance.OnDrift;
            }
        }
    }
    public SimpleCarActions @SimpleCar => new SimpleCarActions(this);

    // Link
    private readonly InputActionMap m_Link;
    private ILinkActions m_LinkActionsCallbackInterface;
    private readonly InputAction m_Link_Move;
    private readonly InputAction m_Link_Crouch;
    private readonly InputAction m_Link_Roll;
    public struct LinkActions
    {
        private @PlayerInput m_Wrapper;
        public LinkActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Link_Move;
        public InputAction @Crouch => m_Wrapper.m_Link_Crouch;
        public InputAction @Roll => m_Wrapper.m_Link_Roll;
        public InputActionMap Get() { return m_Wrapper.m_Link; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LinkActions set) { return set.Get(); }
        public void SetCallbacks(ILinkActions instance)
        {
            if (m_Wrapper.m_LinkActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_LinkActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_LinkActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_LinkActionsCallbackInterface.OnMove;
                @Crouch.started -= m_Wrapper.m_LinkActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_LinkActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_LinkActionsCallbackInterface.OnCrouch;
                @Roll.started -= m_Wrapper.m_LinkActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_LinkActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_LinkActionsCallbackInterface.OnRoll;
            }
            m_Wrapper.m_LinkActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
            }
        }
    }
    public LinkActions @Link => new LinkActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface ISimpleCarActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnDrift(InputAction.CallbackContext context);
    }
    public interface ILinkActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
    }
}
