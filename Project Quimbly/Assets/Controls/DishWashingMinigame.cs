// GENERATED AUTOMATICALLY FROM 'Assets/Controls/DishWashingMinigame.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DishWashingMinigame : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DishWashingMinigame()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DishWashingMinigame"",
    ""maps"": [
        {
            ""name"": ""Dishwashing Mingame"",
            ""id"": ""2c59b1cc-e574-475b-ad71-ed7b567bf6ad"",
            ""actions"": [
                {
                    ""name"": ""Clean Dish"",
                    ""type"": ""Button"",
                    ""id"": ""451903b9-a69a-4c9d-8c8f-f5f77a7afa03"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c0728278-99d4-4ca4-823d-cb02f0570383"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Dishwashing Mouse"",
                    ""action"": ""Clean Dish"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Dishwashing Mouse"",
            ""bindingGroup"": ""Dishwashing Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Dishwashing Mingame
        m_DishwashingMingame = asset.FindActionMap("Dishwashing Mingame", throwIfNotFound: true);
        m_DishwashingMingame_CleanDish = m_DishwashingMingame.FindAction("Clean Dish", throwIfNotFound: true);
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

    // Dishwashing Mingame
    private readonly InputActionMap m_DishwashingMingame;
    private IDishwashingMingameActions m_DishwashingMingameActionsCallbackInterface;
    private readonly InputAction m_DishwashingMingame_CleanDish;
    public struct DishwashingMingameActions
    {
        private @DishWashingMinigame m_Wrapper;
        public DishwashingMingameActions(@DishWashingMinigame wrapper) { m_Wrapper = wrapper; }
        public InputAction @CleanDish => m_Wrapper.m_DishwashingMingame_CleanDish;
        public InputActionMap Get() { return m_Wrapper.m_DishwashingMingame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DishwashingMingameActions set) { return set.Get(); }
        public void SetCallbacks(IDishwashingMingameActions instance)
        {
            if (m_Wrapper.m_DishwashingMingameActionsCallbackInterface != null)
            {
                @CleanDish.started -= m_Wrapper.m_DishwashingMingameActionsCallbackInterface.OnCleanDish;
                @CleanDish.performed -= m_Wrapper.m_DishwashingMingameActionsCallbackInterface.OnCleanDish;
                @CleanDish.canceled -= m_Wrapper.m_DishwashingMingameActionsCallbackInterface.OnCleanDish;
            }
            m_Wrapper.m_DishwashingMingameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CleanDish.started += instance.OnCleanDish;
                @CleanDish.performed += instance.OnCleanDish;
                @CleanDish.canceled += instance.OnCleanDish;
            }
        }
    }
    public DishwashingMingameActions @DishwashingMingame => new DishwashingMingameActions(this);
    private int m_DishwashingMouseSchemeIndex = -1;
    public InputControlScheme DishwashingMouseScheme
    {
        get
        {
            if (m_DishwashingMouseSchemeIndex == -1) m_DishwashingMouseSchemeIndex = asset.FindControlSchemeIndex("Dishwashing Mouse");
            return asset.controlSchemes[m_DishwashingMouseSchemeIndex];
        }
    }
    public interface IDishwashingMingameActions
    {
        void OnCleanDish(InputAction.CallbackContext context);
    }
}
