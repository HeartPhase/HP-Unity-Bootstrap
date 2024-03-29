//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Framework/Resources/Settings/DefaultInputAction.inputactions
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

public partial class @DefaultInputAction: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @DefaultInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DefaultInputAction"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""a1090f62-94f9-4fd8-be43-918a8f5f175b"",
            ""actions"": [
                {
                    ""name"": ""MouseWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9f28abd0-6150-4f82-85ac-24d4e3c9c0d2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""45708462-919b-48b5-977b-580c9263a181"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""ea4b16d2-0eda-40a5-b8e9-06c6d8bea002"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""cb2f093f-13de-4db1-b3f4-7f6cc5477f2d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Anykey"",
                    ""type"": ""Button"",
                    ""id"": ""b572aeba-9baf-4c79-bd4e-5faa0b4a1140"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""3677b10b-263f-434d-8737-044696f6b285"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""b8eda591-b8c1-4855-9c4d-5207ce4f39fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2498a362-2896-49ed-b89c-4b84b280ea7a"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""MouseWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""1986ab60-e72e-45e5-8ebb-82e8341a4c00"",
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
                    ""id"": ""3c564226-c938-4357-9be4-71d3889e27df"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""56a53d5b-1ee1-4d83-8b29-5e7232b535e5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5eb3187b-edb5-4094-aec0-2282823b27fb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""affbe793-0158-4dca-9f47-cb40e35aadc0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""c3b136e8-7004-42d3-986d-a2beb210b95d"",
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
                    ""id"": ""e01ed54d-d86b-45fa-bbf3-33b11c184964"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""643be913-77c6-42eb-a930-38f7706359b5"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8155c806-f038-458c-8311-449f1c1b752a"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ff36ae72-5881-4fd3-9632-f740722c7211"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7db55acb-465e-4eca-a99f-d4da7b9e72bd"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""52b2c00a-77a3-4dd4-a7b3-bec147d1a048"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db2189b2-3d16-4fda-b148-443495b4e30a"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Anykey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b202f165-1b57-48dd-ab89-bbeef48e5afe"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc46c0bc-7a35-415f-a282-29f00e7d7596"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PersonPerspective"",
            ""id"": ""f612467c-0bdd-4972-a0c2-aad76d2fadf8"",
            ""actions"": [
                {
                    ""name"": ""Camera"",
                    ""type"": ""Value"",
                    ""id"": ""ae785a6d-3c9e-4496-b06f-32a4286b08e4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""03e0e379-67a7-49eb-9868-389a0c34ac72"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_MouseWheel = m_Gameplay.FindAction("MouseWheel", throwIfNotFound: true);
        m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
        m_Gameplay_LeftClick = m_Gameplay.FindAction("LeftClick", throwIfNotFound: true);
        m_Gameplay_RightClick = m_Gameplay.FindAction("RightClick", throwIfNotFound: true);
        m_Gameplay_Anykey = m_Gameplay.FindAction("Anykey", throwIfNotFound: true);
        m_Gameplay_Interact = m_Gameplay.FindAction("Interact", throwIfNotFound: true);
        m_Gameplay_Use = m_Gameplay.FindAction("Use", throwIfNotFound: true);
        // PersonPerspective
        m_PersonPerspective = asset.FindActionMap("PersonPerspective", throwIfNotFound: true);
        m_PersonPerspective_Camera = m_PersonPerspective.FindAction("Camera", throwIfNotFound: true);
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
    private List<IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<IGameplayActions>();
    private readonly InputAction m_Gameplay_MouseWheel;
    private readonly InputAction m_Gameplay_Movement;
    private readonly InputAction m_Gameplay_LeftClick;
    private readonly InputAction m_Gameplay_RightClick;
    private readonly InputAction m_Gameplay_Anykey;
    private readonly InputAction m_Gameplay_Interact;
    private readonly InputAction m_Gameplay_Use;
    public struct GameplayActions
    {
        private @DefaultInputAction m_Wrapper;
        public GameplayActions(@DefaultInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseWheel => m_Wrapper.m_Gameplay_MouseWheel;
        public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
        public InputAction @LeftClick => m_Wrapper.m_Gameplay_LeftClick;
        public InputAction @RightClick => m_Wrapper.m_Gameplay_RightClick;
        public InputAction @Anykey => m_Wrapper.m_Gameplay_Anykey;
        public InputAction @Interact => m_Wrapper.m_Gameplay_Interact;
        public InputAction @Use => m_Wrapper.m_Gameplay_Use;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void AddCallbacks(IGameplayActions instance)
        {
            if (instance == null || m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
            @MouseWheel.started += instance.OnMouseWheel;
            @MouseWheel.performed += instance.OnMouseWheel;
            @MouseWheel.canceled += instance.OnMouseWheel;
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @LeftClick.started += instance.OnLeftClick;
            @LeftClick.performed += instance.OnLeftClick;
            @LeftClick.canceled += instance.OnLeftClick;
            @RightClick.started += instance.OnRightClick;
            @RightClick.performed += instance.OnRightClick;
            @RightClick.canceled += instance.OnRightClick;
            @Anykey.started += instance.OnAnykey;
            @Anykey.performed += instance.OnAnykey;
            @Anykey.canceled += instance.OnAnykey;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @Use.started += instance.OnUse;
            @Use.performed += instance.OnUse;
            @Use.canceled += instance.OnUse;
        }

        private void UnregisterCallbacks(IGameplayActions instance)
        {
            @MouseWheel.started -= instance.OnMouseWheel;
            @MouseWheel.performed -= instance.OnMouseWheel;
            @MouseWheel.canceled -= instance.OnMouseWheel;
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @LeftClick.started -= instance.OnLeftClick;
            @LeftClick.performed -= instance.OnLeftClick;
            @LeftClick.canceled -= instance.OnLeftClick;
            @RightClick.started -= instance.OnRightClick;
            @RightClick.performed -= instance.OnRightClick;
            @RightClick.canceled -= instance.OnRightClick;
            @Anykey.started -= instance.OnAnykey;
            @Anykey.performed -= instance.OnAnykey;
            @Anykey.canceled -= instance.OnAnykey;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @Use.started -= instance.OnUse;
            @Use.performed -= instance.OnUse;
            @Use.canceled -= instance.OnUse;
        }

        public void RemoveCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameplayActions instance)
        {
            foreach (var item in m_Wrapper.m_GameplayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // PersonPerspective
    private readonly InputActionMap m_PersonPerspective;
    private List<IPersonPerspectiveActions> m_PersonPerspectiveActionsCallbackInterfaces = new List<IPersonPerspectiveActions>();
    private readonly InputAction m_PersonPerspective_Camera;
    public struct PersonPerspectiveActions
    {
        private @DefaultInputAction m_Wrapper;
        public PersonPerspectiveActions(@DefaultInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Camera => m_Wrapper.m_PersonPerspective_Camera;
        public InputActionMap Get() { return m_Wrapper.m_PersonPerspective; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PersonPerspectiveActions set) { return set.Get(); }
        public void AddCallbacks(IPersonPerspectiveActions instance)
        {
            if (instance == null || m_Wrapper.m_PersonPerspectiveActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PersonPerspectiveActionsCallbackInterfaces.Add(instance);
            @Camera.started += instance.OnCamera;
            @Camera.performed += instance.OnCamera;
            @Camera.canceled += instance.OnCamera;
        }

        private void UnregisterCallbacks(IPersonPerspectiveActions instance)
        {
            @Camera.started -= instance.OnCamera;
            @Camera.performed -= instance.OnCamera;
            @Camera.canceled -= instance.OnCamera;
        }

        public void RemoveCallbacks(IPersonPerspectiveActions instance)
        {
            if (m_Wrapper.m_PersonPerspectiveActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPersonPerspectiveActions instance)
        {
            foreach (var item in m_Wrapper.m_PersonPerspectiveActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PersonPerspectiveActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PersonPerspectiveActions @PersonPerspective => new PersonPerspectiveActions(this);
    public interface IGameplayActions
    {
        void OnMouseWheel(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnLeftClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnAnykey(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnUse(InputAction.CallbackContext context);
    }
    public interface IPersonPerspectiveActions
    {
        void OnCamera(InputAction.CallbackContext context);
    }
}
