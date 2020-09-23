// GENERATED AUTOMATICALLY FROM 'Assets/Debug_Cheat_Console/InputSystem/Input_Player.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Input_Player : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input_Player()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input_Player"",
    ""maps"": [
        {
            ""name"": ""Cheat_Box Map"",
            ""id"": ""94091d23-b944-4088-b36f-25d13e605f1b"",
            ""actions"": [
                {
                    ""name"": ""Toggle Cheat_Box"",
                    ""type"": ""Button"",
                    ""id"": ""64520543-a68a-4725-89ad-387fcb198aa5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Confirm Input"",
                    ""type"": ""Button"",
                    ""id"": ""e7341a4e-5993-4d32-9a37-466795bfbf38"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b5f642c4-4580-411e-8cfd-e2fecaaefc32"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard-Mouse"",
                    ""action"": ""Toggle Cheat_Box"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dcbf3023-fcf9-4f44-a073-d0923ede0101"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard-Mouse"",
                    ""action"": ""Confirm Input"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard-Mouse"",
            ""bindingGroup"": ""Keyboard-Mouse"",
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
        }
    ]
}");
        // Cheat_Box Map
        m_Cheat_BoxMap = asset.FindActionMap("Cheat_Box Map", throwIfNotFound: true);
        m_Cheat_BoxMap_ToggleCheat_Box = m_Cheat_BoxMap.FindAction("Toggle Cheat_Box", throwIfNotFound: true);
        m_Cheat_BoxMap_ConfirmInput = m_Cheat_BoxMap.FindAction("Confirm Input", throwIfNotFound: true);
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

    // Cheat_Box Map
    private readonly InputActionMap m_Cheat_BoxMap;
    private ICheat_BoxMapActions m_Cheat_BoxMapActionsCallbackInterface;
    private readonly InputAction m_Cheat_BoxMap_ToggleCheat_Box;
    private readonly InputAction m_Cheat_BoxMap_ConfirmInput;
    public struct Cheat_BoxMapActions
    {
        private @Input_Player m_Wrapper;
        public Cheat_BoxMapActions(@Input_Player wrapper) { m_Wrapper = wrapper; }
        public InputAction @ToggleCheat_Box => m_Wrapper.m_Cheat_BoxMap_ToggleCheat_Box;
        public InputAction @ConfirmInput => m_Wrapper.m_Cheat_BoxMap_ConfirmInput;
        public InputActionMap Get() { return m_Wrapper.m_Cheat_BoxMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Cheat_BoxMapActions set) { return set.Get(); }
        public void SetCallbacks(ICheat_BoxMapActions instance)
        {
            if (m_Wrapper.m_Cheat_BoxMapActionsCallbackInterface != null)
            {
                @ToggleCheat_Box.started -= m_Wrapper.m_Cheat_BoxMapActionsCallbackInterface.OnToggleCheat_Box;
                @ToggleCheat_Box.performed -= m_Wrapper.m_Cheat_BoxMapActionsCallbackInterface.OnToggleCheat_Box;
                @ToggleCheat_Box.canceled -= m_Wrapper.m_Cheat_BoxMapActionsCallbackInterface.OnToggleCheat_Box;
                @ConfirmInput.started -= m_Wrapper.m_Cheat_BoxMapActionsCallbackInterface.OnConfirmInput;
                @ConfirmInput.performed -= m_Wrapper.m_Cheat_BoxMapActionsCallbackInterface.OnConfirmInput;
                @ConfirmInput.canceled -= m_Wrapper.m_Cheat_BoxMapActionsCallbackInterface.OnConfirmInput;
            }
            m_Wrapper.m_Cheat_BoxMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ToggleCheat_Box.started += instance.OnToggleCheat_Box;
                @ToggleCheat_Box.performed += instance.OnToggleCheat_Box;
                @ToggleCheat_Box.canceled += instance.OnToggleCheat_Box;
                @ConfirmInput.started += instance.OnConfirmInput;
                @ConfirmInput.performed += instance.OnConfirmInput;
                @ConfirmInput.canceled += instance.OnConfirmInput;
            }
        }
    }
    public Cheat_BoxMapActions @Cheat_BoxMap => new Cheat_BoxMapActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard-Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface ICheat_BoxMapActions
    {
        void OnToggleCheat_Box(InputAction.CallbackContext context);
        void OnConfirmInput(InputAction.CallbackContext context);
    }
}
