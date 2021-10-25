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
        },
        {
            ""name"": ""FPS"",
            ""id"": ""0f7d2966-e324-44e4-9426-d200912c0f75"",
            ""actions"": [
                {
                    ""name"": ""CameraMove"",
                    ""type"": ""Value"",
                    ""id"": ""a6d32359-5175-4fbd-971f-9939ee8b6138"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraMoveDelta"",
                    ""type"": ""Value"",
                    ""id"": ""f5b2b997-8775-4178-9f55-70fd9b2f7be3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""7c86d7da-0fd5-4af9-bb06-a95588e9012d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""32986f7a-e8a5-44ac-af30-faf8503506e5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b60ccba0-89d2-42a7-a08c-4193ecf954cb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Primary"",
                    ""type"": ""Button"",
                    ""id"": ""22eb34f7-b6c7-497d-a1d4-a7e6c17c133f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""47b0e451-52f8-4e6c-b2e6-6202d662e202"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CameraMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""261483c4-2d56-45a9-a0dc-b9b2b87c1770"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""CameraMoveDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""b3cd4af5-f198-4585-a494-b1222d0c0aaf"",
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
                    ""id"": ""795094ca-dd3e-4a36-9b8f-e67aafb27e66"",
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
                    ""id"": ""1ee54d86-141a-4927-8c03-b90b3a615018"",
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
                    ""id"": ""da0b7a7e-b353-4bd3-a7d1-ebf38c059fa5"",
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
                    ""id"": ""38419013-8fbd-48f1-bc54-b359c71eeef0"",
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
                    ""id"": ""7d72b6ee-bd1d-4a69-92b1-e573608cc068"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a6e8cdb-ffcb-4c49-a95f-9b7b711e8265"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11be1155-16e4-419d-a219-4752eb48d04b"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""416a367e-9076-45d4-86c8-a1faabaafe39"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5af331fa-746a-4f3d-a1b9-0ad88bde15aa"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d525f8ca-7b97-43ae-9912-b39433ffab63"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b5e77e4-eeb2-451f-96a3-f788cda21157"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Primary"",
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
        // FPS
        m_FPS = asset.FindActionMap("FPS", throwIfNotFound: true);
        m_FPS_CameraMove = m_FPS.FindAction("CameraMove", throwIfNotFound: true);
        m_FPS_CameraMoveDelta = m_FPS.FindAction("CameraMoveDelta", throwIfNotFound: true);
        m_FPS_Move = m_FPS.FindAction("Move", throwIfNotFound: true);
        m_FPS_Sprint = m_FPS.FindAction("Sprint", throwIfNotFound: true);
        m_FPS_Jump = m_FPS.FindAction("Jump", throwIfNotFound: true);
        m_FPS_Primary = m_FPS.FindAction("Primary", throwIfNotFound: true);
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

    // FPS
    private readonly InputActionMap m_FPS;
    private IFPSActions m_FPSActionsCallbackInterface;
    private readonly InputAction m_FPS_CameraMove;
    private readonly InputAction m_FPS_CameraMoveDelta;
    private readonly InputAction m_FPS_Move;
    private readonly InputAction m_FPS_Sprint;
    private readonly InputAction m_FPS_Jump;
    private readonly InputAction m_FPS_Primary;
    public struct FPSActions
    {
        private @PlayerInput m_Wrapper;
        public FPSActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @CameraMove => m_Wrapper.m_FPS_CameraMove;
        public InputAction @CameraMoveDelta => m_Wrapper.m_FPS_CameraMoveDelta;
        public InputAction @Move => m_Wrapper.m_FPS_Move;
        public InputAction @Sprint => m_Wrapper.m_FPS_Sprint;
        public InputAction @Jump => m_Wrapper.m_FPS_Jump;
        public InputAction @Primary => m_Wrapper.m_FPS_Primary;
        public InputActionMap Get() { return m_Wrapper.m_FPS; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FPSActions set) { return set.Get(); }
        public void SetCallbacks(IFPSActions instance)
        {
            if (m_Wrapper.m_FPSActionsCallbackInterface != null)
            {
                @CameraMove.started -= m_Wrapper.m_FPSActionsCallbackInterface.OnCameraMove;
                @CameraMove.performed -= m_Wrapper.m_FPSActionsCallbackInterface.OnCameraMove;
                @CameraMove.canceled -= m_Wrapper.m_FPSActionsCallbackInterface.OnCameraMove;
                @CameraMoveDelta.started -= m_Wrapper.m_FPSActionsCallbackInterface.OnCameraMoveDelta;
                @CameraMoveDelta.performed -= m_Wrapper.m_FPSActionsCallbackInterface.OnCameraMoveDelta;
                @CameraMoveDelta.canceled -= m_Wrapper.m_FPSActionsCallbackInterface.OnCameraMoveDelta;
                @Move.started -= m_Wrapper.m_FPSActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_FPSActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_FPSActionsCallbackInterface.OnMove;
                @Sprint.started -= m_Wrapper.m_FPSActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_FPSActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_FPSActionsCallbackInterface.OnSprint;
                @Jump.started -= m_Wrapper.m_FPSActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_FPSActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_FPSActionsCallbackInterface.OnJump;
                @Primary.started -= m_Wrapper.m_FPSActionsCallbackInterface.OnPrimary;
                @Primary.performed -= m_Wrapper.m_FPSActionsCallbackInterface.OnPrimary;
                @Primary.canceled -= m_Wrapper.m_FPSActionsCallbackInterface.OnPrimary;
            }
            m_Wrapper.m_FPSActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CameraMove.started += instance.OnCameraMove;
                @CameraMove.performed += instance.OnCameraMove;
                @CameraMove.canceled += instance.OnCameraMove;
                @CameraMoveDelta.started += instance.OnCameraMoveDelta;
                @CameraMoveDelta.performed += instance.OnCameraMoveDelta;
                @CameraMoveDelta.canceled += instance.OnCameraMoveDelta;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Primary.started += instance.OnPrimary;
                @Primary.performed += instance.OnPrimary;
                @Primary.canceled += instance.OnPrimary;
            }
        }
    }
    public FPSActions @FPS => new FPSActions(this);
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
    public interface IFPSActions
    {
        void OnCameraMove(InputAction.CallbackContext context);
        void OnCameraMoveDelta(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnPrimary(InputAction.CallbackContext context);
    }
}
