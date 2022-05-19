// GENERATED AUTOMATICALLY FROM 'Assets/MDDSkillEngine/Scripts/Runtime/Input/MDDInputControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MDDInputControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MDDInputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MDDInputControls"",
    ""maps"": [
        {
            ""name"": ""Heros_Normal"",
            ""id"": ""2cc36c73-6edc-43b6-a83a-7f22f0d2d979"",
            ""actions"": [
                {
                    ""name"": ""Skill_1"",
                    ""type"": ""Button"",
                    ""id"": ""9453a061-f72f-4a05-a0ae-3a7f67e5b35d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill_2"",
                    ""type"": ""Button"",
                    ""id"": ""f00ed875-554b-483e-8695-60808fa988ad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill_3"",
                    ""type"": ""Button"",
                    ""id"": ""e5ebda7d-e63e-4ddc-b805-e8117cff9b54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill_4"",
                    ""type"": ""Button"",
                    ""id"": ""39b870e8-ba61-4e29-87a7-acc47eef6ed3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""353aed27-22d4-4d8c-9f78-7c5359ea6c5c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""c6f3cbe2-1596-4282-93e6-24d2f5fa0a2b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""S"",
                    ""type"": ""Button"",
                    ""id"": ""6e492507-9c51-49be-a8ef-c8152161a692"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4e0d771c-2e44-4493-9e6e-a456708e8a2c"",
                    ""path"": ""<Keyboard>/#(Q)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""427e05ae-692e-40ad-a492-ac8da1e3a6be"",
                    ""path"": ""<Keyboard>/#(W)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill_2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6209af45-122f-4724-9a8f-a2847e0bde1e"",
                    ""path"": ""<Keyboard>/#(E)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill_3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18c22144-843a-4408-87b7-5685854b8d38"",
                    ""path"": ""<Keyboard>/#(R)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill_4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cadd6a85-c82d-4c6c-bf0d-33cb67043ca1"",
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
                    ""id"": ""23fad014-3d7c-4589-8891-063aff78a292"",
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
                    ""id"": ""011d128e-5bc0-4a83-a619-b0929b79c0d3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""S"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Heros_Normal
        m_Heros_Normal = asset.FindActionMap("Heros_Normal", throwIfNotFound: true);
        m_Heros_Normal_Skill_1 = m_Heros_Normal.FindAction("Skill_1", throwIfNotFound: true);
        m_Heros_Normal_Skill_2 = m_Heros_Normal.FindAction("Skill_2", throwIfNotFound: true);
        m_Heros_Normal_Skill_3 = m_Heros_Normal.FindAction("Skill_3", throwIfNotFound: true);
        m_Heros_Normal_Skill_4 = m_Heros_Normal.FindAction("Skill_4", throwIfNotFound: true);
        m_Heros_Normal_LeftClick = m_Heros_Normal.FindAction("LeftClick", throwIfNotFound: true);
        m_Heros_Normal_RightClick = m_Heros_Normal.FindAction("RightClick", throwIfNotFound: true);
        m_Heros_Normal_S = m_Heros_Normal.FindAction("S", throwIfNotFound: true);
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

    // Heros_Normal
    private readonly InputActionMap m_Heros_Normal;
    private IHeros_NormalActions m_Heros_NormalActionsCallbackInterface;
    private readonly InputAction m_Heros_Normal_Skill_1;
    private readonly InputAction m_Heros_Normal_Skill_2;
    private readonly InputAction m_Heros_Normal_Skill_3;
    private readonly InputAction m_Heros_Normal_Skill_4;
    private readonly InputAction m_Heros_Normal_LeftClick;
    private readonly InputAction m_Heros_Normal_RightClick;
    private readonly InputAction m_Heros_Normal_S;
    public struct Heros_NormalActions
    {
        private @MDDInputControls m_Wrapper;
        public Heros_NormalActions(@MDDInputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Skill_1 => m_Wrapper.m_Heros_Normal_Skill_1;
        public InputAction @Skill_2 => m_Wrapper.m_Heros_Normal_Skill_2;
        public InputAction @Skill_3 => m_Wrapper.m_Heros_Normal_Skill_3;
        public InputAction @Skill_4 => m_Wrapper.m_Heros_Normal_Skill_4;
        public InputAction @LeftClick => m_Wrapper.m_Heros_Normal_LeftClick;
        public InputAction @RightClick => m_Wrapper.m_Heros_Normal_RightClick;
        public InputAction @S => m_Wrapper.m_Heros_Normal_S;
        public InputActionMap Get() { return m_Wrapper.m_Heros_Normal; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Heros_NormalActions set) { return set.Get(); }
        public void SetCallbacks(IHeros_NormalActions instance)
        {
            if (m_Wrapper.m_Heros_NormalActionsCallbackInterface != null)
            {
                @Skill_1.started -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnSkill_1;
                @Skill_1.performed -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnSkill_1;
                @Skill_1.canceled -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnSkill_1;
                @Skill_2.started -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnSkill_2;
                @Skill_2.performed -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnSkill_2;
                @Skill_2.canceled -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnSkill_2;
                @Skill_3.started -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnSkill_3;
                @Skill_3.performed -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnSkill_3;
                @Skill_3.canceled -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnSkill_3;
                @Skill_4.started -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnSkill_4;
                @Skill_4.performed -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnSkill_4;
                @Skill_4.canceled -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnSkill_4;
                @LeftClick.started -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnLeftClick;
                @RightClick.started -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnRightClick;
                @S.started -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnS;
                @S.performed -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnS;
                @S.canceled -= m_Wrapper.m_Heros_NormalActionsCallbackInterface.OnS;
            }
            m_Wrapper.m_Heros_NormalActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Skill_1.started += instance.OnSkill_1;
                @Skill_1.performed += instance.OnSkill_1;
                @Skill_1.canceled += instance.OnSkill_1;
                @Skill_2.started += instance.OnSkill_2;
                @Skill_2.performed += instance.OnSkill_2;
                @Skill_2.canceled += instance.OnSkill_2;
                @Skill_3.started += instance.OnSkill_3;
                @Skill_3.performed += instance.OnSkill_3;
                @Skill_3.canceled += instance.OnSkill_3;
                @Skill_4.started += instance.OnSkill_4;
                @Skill_4.performed += instance.OnSkill_4;
                @Skill_4.canceled += instance.OnSkill_4;
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @S.started += instance.OnS;
                @S.performed += instance.OnS;
                @S.canceled += instance.OnS;
            }
        }
    }
    public Heros_NormalActions @Heros_Normal => new Heros_NormalActions(this);
    public interface IHeros_NormalActions
    {
        void OnSkill_1(InputAction.CallbackContext context);
        void OnSkill_2(InputAction.CallbackContext context);
        void OnSkill_3(InputAction.CallbackContext context);
        void OnSkill_4(InputAction.CallbackContext context);
        void OnLeftClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnS(InputAction.CallbackContext context);
    }
}
