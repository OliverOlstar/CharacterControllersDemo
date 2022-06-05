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
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""4b4dbb93-399d-4c03-b847-4809770ed0f4"",
                    ""expectedControlType"": ""Button"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""daf93b97-4687-4ba3-a81f-743f8232de2d"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4ace6a9-18ba-478b-a34a-aced23cbe390"",
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
                    ""id"": ""bbacae2d-3321-4422-abd0-d6cd7ca97d5b"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Simple2D"",
            ""id"": ""05feaf8d-442f-425b-b909-c250f07e4b0d"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d7dedb0b-5bc1-4570-9c8f-6268da55e617"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""2f8c227a-26ce-4ec6-9c79-36244d45f50f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""267f654d-8cf5-4250-8766-528658026fb7"",
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
                    ""id"": ""71128c83-089f-46e8-a1c8-fc147ed3b47c"",
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
                    ""id"": ""e631dd95-ade6-4bfd-b4fb-c287e54b0911"",
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
                    ""id"": ""e1c784ea-b94c-4a5f-993f-4ec7bdef59ed"",
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
                    ""id"": ""a7ace4f1-19ab-4543-a8ef-9a0fe84a7852"",
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
                    ""id"": ""4fc57d00-8a1c-4590-bb6b-a07bf8e911fa"",
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
                    ""id"": ""18fc9b31-85d8-4a11-8968-a2e7cdddabe6"",
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
                    ""id"": ""dabaa6aa-d91b-41e5-8068-2eb65f4f430a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Spectator Camera"",
            ""id"": ""1c37f976-30c9-44ff-a9d0-8b5e17ff0e6a"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""37607f15-9408-49a2-9c61-dfab4d8ef0ed"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookDelta"",
                    ""type"": ""PassThrough"",
                    ""id"": ""276b9d7d-1939-48d2-93a5-318b8f0c07e1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveHorizontal"",
                    ""type"": ""PassThrough"",
                    ""id"": ""23a272ce-5d15-43d7-a306-c1754d3a62cc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveVertical"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c70876ce-f28c-4fd7-bb35-e0f00cb65895"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6dfc14cd-cc8b-45a8-9616-e6cb53ad65b4"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""2e3f0408-2fdf-48ad-9123-1d488b8368be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ModeToggle"",
                    ""type"": ""Button"",
                    ""id"": ""9cc8fff5-ec13-4746-9ff0-add64d6d0e5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TargetToggle"",
                    ""type"": ""Button"",
                    ""id"": ""c600d64c-88f6-4354-9459-9343bca35844"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c49d2090-779d-4b3f-8061-ecf31cf3a5c3"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd7de8b3-10ae-45e8-a856-52d58a417a0d"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""LookDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ec11443-43ef-4c06-84aa-c9de3f2d0975"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""3a878f1b-d48d-453a-8e7f-c1a88529fe80"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""81a9894c-3cdc-473e-b9d7-03e0c1b46395"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""81c76cab-cbcb-41e4-b4ab-808bbf4a15d6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c383a881-cb6a-44de-a4fa-b3274c5bcf14"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2efe4a50-01e8-48c4-b8ca-3e76504d08ca"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Bumpers"",
                    ""id"": ""88a59fb1-7f5f-4248-aecb-fcea3215d866"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""28aa8559-07f7-421a-976a-9f4b9bb7a21e"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""92b05767-fbad-403f-81db-a79448d4fbe5"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""QE"",
                    ""id"": ""750e8d7d-d876-470c-8b81-e0529289342a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""988c5059-d711-4581-9981-b8117418b9dc"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6c5ca100-fd08-4148-8e25-10b8e961164d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""85535fea-6f93-4e5b-a62f-bd843ea71f08"",
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
                    ""id"": ""e6c4adb0-6cb7-410c-9ca4-7c8f750ee78f"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d0a36d3-a556-42c4-bc75-5fde49605d6e"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67c98706-393c-4792-8e71-ade76895dbe6"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa192966-6398-4c15-8826-f8a6c542c8a1"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbbfa0bd-bff4-4c0d-ad81-f07e3f41235b"",
                    ""path"": ""<Gamepad>/dpad/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d40e392d-d99f-4a6b-b921-ba4669cd9736"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ModeToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92b5bc89-81db-4b16-ae05-36043c79f419"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""ModeToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d7998cb-c286-411b-bc92-16ad88a137f9"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""TargetToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""633eb6a4-4830-4493-b1ad-e6107c539c3f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""TargetToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""89397d79-b5df-4c7a-9d2b-0a951b245e32"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""919b16f1-c408-4783-9de1-948634b11a71"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookDelta"",
                    ""type"": ""PassThrough"",
                    ""id"": ""49dbeebc-77c3-42e4-bfe7-6a9f2aaf7892"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""13a5e135-3b5f-4e66-a6b7-ce19a096f603"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a8c293b0-5ad7-4810-a848-5091a703618d"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a34fb713-a850-4371-8df1-ba40af7be33f"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""LookDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""746ec216-a3c6-4c4a-9535-886229bd5833"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a463468a-f429-4fe9-87db-a787bd97545f"",
                    ""path"": ""<Gamepad>/dpad/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""16c7761e-a5f0-43a4-a262-6617516e8312"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""efe39817-ed10-48fb-873c-835d089b38d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1c7b47b3-75b9-45ac-8a4c-0e21cb0d4892"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72cc9850-7b00-480c-aa33-e233a606fd28"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
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
        m_FPS_Crouch = m_FPS.FindAction("Crouch", throwIfNotFound: true);
        m_FPS_Sprint = m_FPS.FindAction("Sprint", throwIfNotFound: true);
        m_FPS_Jump = m_FPS.FindAction("Jump", throwIfNotFound: true);
        m_FPS_Primary = m_FPS.FindAction("Primary", throwIfNotFound: true);
        // Simple2D
        m_Simple2D = asset.FindActionMap("Simple2D", throwIfNotFound: true);
        m_Simple2D_Move = m_Simple2D.FindAction("Move", throwIfNotFound: true);
        m_Simple2D_Jump = m_Simple2D.FindAction("Jump", throwIfNotFound: true);
        // Spectator Camera
        m_SpectatorCamera = asset.FindActionMap("Spectator Camera", throwIfNotFound: true);
        m_SpectatorCamera_Look = m_SpectatorCamera.FindAction("Look", throwIfNotFound: true);
        m_SpectatorCamera_LookDelta = m_SpectatorCamera.FindAction("LookDelta", throwIfNotFound: true);
        m_SpectatorCamera_MoveHorizontal = m_SpectatorCamera.FindAction("MoveHorizontal", throwIfNotFound: true);
        m_SpectatorCamera_MoveVertical = m_SpectatorCamera.FindAction("MoveVertical", throwIfNotFound: true);
        m_SpectatorCamera_Zoom = m_SpectatorCamera.FindAction("Zoom", throwIfNotFound: true);
        m_SpectatorCamera_Sprint = m_SpectatorCamera.FindAction("Sprint", throwIfNotFound: true);
        m_SpectatorCamera_ModeToggle = m_SpectatorCamera.FindAction("ModeToggle", throwIfNotFound: true);
        m_SpectatorCamera_TargetToggle = m_SpectatorCamera.FindAction("TargetToggle", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_Look = m_Camera.FindAction("Look", throwIfNotFound: true);
        m_Camera_LookDelta = m_Camera.FindAction("LookDelta", throwIfNotFound: true);
        m_Camera_Zoom = m_Camera.FindAction("Zoom", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Pause = m_Menu.FindAction("Pause", throwIfNotFound: true);
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
    private readonly InputAction m_FPS_Crouch;
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
        public InputAction @Crouch => m_Wrapper.m_FPS_Crouch;
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
                @Crouch.started -= m_Wrapper.m_FPSActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_FPSActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_FPSActionsCallbackInterface.OnCrouch;
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
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
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

    // Simple2D
    private readonly InputActionMap m_Simple2D;
    private ISimple2DActions m_Simple2DActionsCallbackInterface;
    private readonly InputAction m_Simple2D_Move;
    private readonly InputAction m_Simple2D_Jump;
    public struct Simple2DActions
    {
        private @PlayerInput m_Wrapper;
        public Simple2DActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Simple2D_Move;
        public InputAction @Jump => m_Wrapper.m_Simple2D_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Simple2D; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Simple2DActions set) { return set.Get(); }
        public void SetCallbacks(ISimple2DActions instance)
        {
            if (m_Wrapper.m_Simple2DActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_Simple2DActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Simple2DActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Simple2DActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_Simple2DActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Simple2DActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Simple2DActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_Simple2DActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public Simple2DActions @Simple2D => new Simple2DActions(this);

    // Spectator Camera
    private readonly InputActionMap m_SpectatorCamera;
    private ISpectatorCameraActions m_SpectatorCameraActionsCallbackInterface;
    private readonly InputAction m_SpectatorCamera_Look;
    private readonly InputAction m_SpectatorCamera_LookDelta;
    private readonly InputAction m_SpectatorCamera_MoveHorizontal;
    private readonly InputAction m_SpectatorCamera_MoveVertical;
    private readonly InputAction m_SpectatorCamera_Zoom;
    private readonly InputAction m_SpectatorCamera_Sprint;
    private readonly InputAction m_SpectatorCamera_ModeToggle;
    private readonly InputAction m_SpectatorCamera_TargetToggle;
    public struct SpectatorCameraActions
    {
        private @PlayerInput m_Wrapper;
        public SpectatorCameraActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_SpectatorCamera_Look;
        public InputAction @LookDelta => m_Wrapper.m_SpectatorCamera_LookDelta;
        public InputAction @MoveHorizontal => m_Wrapper.m_SpectatorCamera_MoveHorizontal;
        public InputAction @MoveVertical => m_Wrapper.m_SpectatorCamera_MoveVertical;
        public InputAction @Zoom => m_Wrapper.m_SpectatorCamera_Zoom;
        public InputAction @Sprint => m_Wrapper.m_SpectatorCamera_Sprint;
        public InputAction @ModeToggle => m_Wrapper.m_SpectatorCamera_ModeToggle;
        public InputAction @TargetToggle => m_Wrapper.m_SpectatorCamera_TargetToggle;
        public InputActionMap Get() { return m_Wrapper.m_SpectatorCamera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SpectatorCameraActions set) { return set.Get(); }
        public void SetCallbacks(ISpectatorCameraActions instance)
        {
            if (m_Wrapper.m_SpectatorCameraActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnLook;
                @LookDelta.started -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnLookDelta;
                @LookDelta.performed -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnLookDelta;
                @LookDelta.canceled -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnLookDelta;
                @MoveHorizontal.started -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnMoveHorizontal;
                @MoveHorizontal.performed -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnMoveHorizontal;
                @MoveHorizontal.canceled -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnMoveHorizontal;
                @MoveVertical.started -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnMoveVertical;
                @MoveVertical.performed -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnMoveVertical;
                @MoveVertical.canceled -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnMoveVertical;
                @Zoom.started -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnZoom;
                @Sprint.started -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnSprint;
                @ModeToggle.started -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnModeToggle;
                @ModeToggle.performed -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnModeToggle;
                @ModeToggle.canceled -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnModeToggle;
                @TargetToggle.started -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnTargetToggle;
                @TargetToggle.performed -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnTargetToggle;
                @TargetToggle.canceled -= m_Wrapper.m_SpectatorCameraActionsCallbackInterface.OnTargetToggle;
            }
            m_Wrapper.m_SpectatorCameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @LookDelta.started += instance.OnLookDelta;
                @LookDelta.performed += instance.OnLookDelta;
                @LookDelta.canceled += instance.OnLookDelta;
                @MoveHorizontal.started += instance.OnMoveHorizontal;
                @MoveHorizontal.performed += instance.OnMoveHorizontal;
                @MoveHorizontal.canceled += instance.OnMoveHorizontal;
                @MoveVertical.started += instance.OnMoveVertical;
                @MoveVertical.performed += instance.OnMoveVertical;
                @MoveVertical.canceled += instance.OnMoveVertical;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @ModeToggle.started += instance.OnModeToggle;
                @ModeToggle.performed += instance.OnModeToggle;
                @ModeToggle.canceled += instance.OnModeToggle;
                @TargetToggle.started += instance.OnTargetToggle;
                @TargetToggle.performed += instance.OnTargetToggle;
                @TargetToggle.canceled += instance.OnTargetToggle;
            }
        }
    }
    public SpectatorCameraActions @SpectatorCamera => new SpectatorCameraActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_Look;
    private readonly InputAction m_Camera_LookDelta;
    private readonly InputAction m_Camera_Zoom;
    public struct CameraActions
    {
        private @PlayerInput m_Wrapper;
        public CameraActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_Camera_Look;
        public InputAction @LookDelta => m_Wrapper.m_Camera_LookDelta;
        public InputAction @Zoom => m_Wrapper.m_Camera_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnLook;
                @LookDelta.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnLookDelta;
                @LookDelta.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnLookDelta;
                @LookDelta.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnLookDelta;
                @Zoom.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @LookDelta.started += instance.OnLookDelta;
                @LookDelta.performed += instance.OnLookDelta;
                @LookDelta.canceled += instance.OnLookDelta;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Pause;
    public struct MenuActions
    {
        private @PlayerInput m_Wrapper;
        public MenuActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_Menu_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
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
        void OnCrouch(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnPrimary(InputAction.CallbackContext context);
    }
    public interface ISimple2DActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface ISpectatorCameraActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnLookDelta(InputAction.CallbackContext context);
        void OnMoveHorizontal(InputAction.CallbackContext context);
        void OnMoveVertical(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnModeToggle(InputAction.CallbackContext context);
        void OnTargetToggle(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnLookDelta(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
}
