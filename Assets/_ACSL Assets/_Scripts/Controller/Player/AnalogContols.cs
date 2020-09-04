// GENERATED AUTOMATICALLY FROM 'Assets/_ACSL Assets/_Scripts/Controller/Player/AnalogContols.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GamePadController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GamePadController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""AnalogContols"",
    ""maps"": [
        {
            ""name"": ""AnalogController"",
            ""id"": ""a243f1f0-49aa-4365-a8f0-27c485c8c1a5"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""60339b34-7724-47e1-a00a-8a930e2f72fe"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PivotCamera"",
                    ""type"": ""Button"",
                    ""id"": ""4b0e240e-2870-4ef5-b190-4c362980e7ba"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchEnemyTarget"",
                    ""type"": ""Button"",
                    ""id"": ""62caa72d-bbf6-49a9-8777-6fd330ae96c1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ResetTargetToSelf"",
                    ""type"": ""Button"",
                    ""id"": ""af26c446-6e2f-45e1-96da-0cc379644a09"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftArmAbility"",
                    ""type"": ""Button"",
                    ""id"": ""55469ce9-b24e-47e4-8cc5-666368a9112c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightArmAbility"",
                    ""type"": ""Button"",
                    ""id"": ""842cb3b4-f966-4447-85d7-6082d0f078c5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BodyAbility"",
                    ""type"": ""Button"",
                    ""id"": ""232a4895-6643-4bc8-bbeb-d7ba1bdbeb4e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LegAbility"",
                    ""type"": ""Button"",
                    ""id"": ""f38e23f8-683c-4ac5-b83a-5c3034bbd8df"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ResetCameraState"",
                    ""type"": ""Button"",
                    ""id"": ""bf956898-63dc-45e0-9fa8-05e09e1c0843"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Exit"",
                    ""type"": ""Button"",
                    ""id"": ""729d8fb7-9a64-46c2-bd50-4a3241eca68a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectMenuElement"",
                    ""type"": ""Button"",
                    ""id"": ""16e345da-6864-4cdf-a87c-a4ed156eb49a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StopMovement"",
                    ""type"": ""Button"",
                    ""id"": ""3480c84a-94ca-4429-a9e9-4ef4cbe61fe3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraZoomIn"",
                    ""type"": ""Button"",
                    ""id"": ""3e8c5f29-5ab3-40b9-a598-4c961c558db8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraZoomOut"",
                    ""type"": ""Button"",
                    ""id"": ""6f174a71-de8c-417d-9f73-665a631b423d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""ce67fddd-9ec3-4241-b824-f064109389a7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fa8c1a50-e6ff-426d-b79b-375c2f083b9c"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""deed6010-42c0-4199-a79c-6fe89d6e565f"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3cc0c93c-17ea-4f0c-8521-08c28bbb77f4"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PivotCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d6c4e593-7ece-4740-9338-e8bccadc565f"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PivotCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc4d255e-54b0-4435-867a-d56771639319"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchEnemyTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""753b6a99-220d-4ccd-b6eb-769d0b86eaf0"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchEnemyTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1095d11-ada8-44c6-9f9e-7aee6973d1fb"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetTargetToSelf"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4c0a9ff-c0ce-4822-9dee-5c88c907a11f"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetTargetToSelf"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab12f009-6f3e-4619-8f4c-bd4d001d7a12"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftArmAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""51105350-f2f3-4718-953a-cbec2d38b83c"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftArmAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00f5dde7-e566-4b32-98a4-7644bbee8911"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightArmAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae1cf005-f634-48ee-8058-eefeb638fc2d"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightArmAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62e9e589-65d4-4ea7-9380-ffac63b93796"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BodyAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""088df3f2-147c-4158-90a5-8389f5508c64"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BodyAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec275345-6d2e-4109-a1d8-ab5e5b5fe955"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LegAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d5fd87f-b99d-4ba1-92cd-5cb8b8a6844e"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LegAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bad14540-0e2a-4f37-8f1e-2c1ebd6e83e2"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetCameraState"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8132d898-c1a6-4c71-acfc-8f4503ad04c2"",
                    ""path"": ""<XInputController>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetCameraState"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2b559e1-e8f7-420f-a806-66e6068d01e4"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc100133-e831-47c4-9aa4-d13c3f46c37a"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""746d8bc3-c4c8-49e0-8c8d-fdc1d8f3ef7a"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectMenuElement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fff0e083-34ff-4a8a-847e-271afc7e4266"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectMenuElement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6a76cda-2699-46fe-ac0a-1df0ce2f2f5b"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectMenuElement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41ffee81-e3c1-4e83-a659-75699591c2c3"",
                    ""path"": ""<XInputController>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectMenuElement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58df0a59-5c7a-447d-adad-9cc13ef27536"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""StopMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a706ba39-daad-4d46-8381-7c095d8b06b5"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""StopMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a9aebeb-e99b-4cc9-8851-f0cf7e34f33a"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraZoomIn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a836460-c2ed-4e12-a7e5-32922b9402e9"",
                    ""path"": ""<XInputController>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraZoomIn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2b5b7f2-d622-44f6-9943-e7d0780fbf4d"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraZoomOut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90dc0cdb-5015-48bf-8117-889c7ab88cf2"",
                    ""path"": ""<XInputController>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraZoomOut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b511e101-8f09-4474-aa9d-d1d7a2226673"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // AnalogController
        m_AnalogController = asset.FindActionMap("AnalogController", throwIfNotFound: true);
        m_AnalogController_Move = m_AnalogController.FindAction("Move", throwIfNotFound: true);
        m_AnalogController_PivotCamera = m_AnalogController.FindAction("PivotCamera", throwIfNotFound: true);
        m_AnalogController_SwitchEnemyTarget = m_AnalogController.FindAction("SwitchEnemyTarget", throwIfNotFound: true);
        m_AnalogController_ResetTargetToSelf = m_AnalogController.FindAction("ResetTargetToSelf", throwIfNotFound: true);
        m_AnalogController_LeftArmAbility = m_AnalogController.FindAction("LeftArmAbility", throwIfNotFound: true);
        m_AnalogController_RightArmAbility = m_AnalogController.FindAction("RightArmAbility", throwIfNotFound: true);
        m_AnalogController_BodyAbility = m_AnalogController.FindAction("BodyAbility", throwIfNotFound: true);
        m_AnalogController_LegAbility = m_AnalogController.FindAction("LegAbility", throwIfNotFound: true);
        m_AnalogController_ResetCameraState = m_AnalogController.FindAction("ResetCameraState", throwIfNotFound: true);
        m_AnalogController_Exit = m_AnalogController.FindAction("Exit", throwIfNotFound: true);
        m_AnalogController_SelectMenuElement = m_AnalogController.FindAction("SelectMenuElement", throwIfNotFound: true);
        m_AnalogController_StopMovement = m_AnalogController.FindAction("StopMovement", throwIfNotFound: true);
        m_AnalogController_CameraZoomIn = m_AnalogController.FindAction("CameraZoomIn", throwIfNotFound: true);
        m_AnalogController_CameraZoomOut = m_AnalogController.FindAction("CameraZoomOut", throwIfNotFound: true);
        m_AnalogController_Fire = m_AnalogController.FindAction("Fire", throwIfNotFound: true);
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

    // AnalogController
    private readonly InputActionMap m_AnalogController;
    private IAnalogControllerActions m_AnalogControllerActionsCallbackInterface;
    private readonly InputAction m_AnalogController_Move;
    private readonly InputAction m_AnalogController_PivotCamera;
    private readonly InputAction m_AnalogController_SwitchEnemyTarget;
    private readonly InputAction m_AnalogController_ResetTargetToSelf;
    private readonly InputAction m_AnalogController_LeftArmAbility;
    private readonly InputAction m_AnalogController_RightArmAbility;
    private readonly InputAction m_AnalogController_BodyAbility;
    private readonly InputAction m_AnalogController_LegAbility;
    private readonly InputAction m_AnalogController_ResetCameraState;
    private readonly InputAction m_AnalogController_Exit;
    private readonly InputAction m_AnalogController_SelectMenuElement;
    private readonly InputAction m_AnalogController_StopMovement;
    private readonly InputAction m_AnalogController_CameraZoomIn;
    private readonly InputAction m_AnalogController_CameraZoomOut;
    private readonly InputAction m_AnalogController_Fire;
    public struct AnalogControllerActions
    {
        private @GamePadController m_Wrapper;
        public AnalogControllerActions(@GamePadController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_AnalogController_Move;
        public InputAction @PivotCamera => m_Wrapper.m_AnalogController_PivotCamera;
        public InputAction @SwitchEnemyTarget => m_Wrapper.m_AnalogController_SwitchEnemyTarget;
        public InputAction @ResetTargetToSelf => m_Wrapper.m_AnalogController_ResetTargetToSelf;
        public InputAction @LeftArmAbility => m_Wrapper.m_AnalogController_LeftArmAbility;
        public InputAction @RightArmAbility => m_Wrapper.m_AnalogController_RightArmAbility;
        public InputAction @BodyAbility => m_Wrapper.m_AnalogController_BodyAbility;
        public InputAction @LegAbility => m_Wrapper.m_AnalogController_LegAbility;
        public InputAction @ResetCameraState => m_Wrapper.m_AnalogController_ResetCameraState;
        public InputAction @Exit => m_Wrapper.m_AnalogController_Exit;
        public InputAction @SelectMenuElement => m_Wrapper.m_AnalogController_SelectMenuElement;
        public InputAction @StopMovement => m_Wrapper.m_AnalogController_StopMovement;
        public InputAction @CameraZoomIn => m_Wrapper.m_AnalogController_CameraZoomIn;
        public InputAction @CameraZoomOut => m_Wrapper.m_AnalogController_CameraZoomOut;
        public InputAction @Fire => m_Wrapper.m_AnalogController_Fire;
        public InputActionMap Get() { return m_Wrapper.m_AnalogController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AnalogControllerActions set) { return set.Get(); }
        public void SetCallbacks(IAnalogControllerActions instance)
        {
            if (m_Wrapper.m_AnalogControllerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnMove;
                @PivotCamera.started -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnPivotCamera;
                @PivotCamera.performed -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnPivotCamera;
                @PivotCamera.canceled -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnPivotCamera;
                @SwitchEnemyTarget.started -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnSwitchEnemyTarget;
                @SwitchEnemyTarget.performed -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnSwitchEnemyTarget;
                @SwitchEnemyTarget.canceled -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnSwitchEnemyTarget;
                @ResetTargetToSelf.started -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnResetTargetToSelf;
                @ResetTargetToSelf.performed -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnResetTargetToSelf;
                @ResetTargetToSelf.canceled -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnResetTargetToSelf;
                @LeftArmAbility.started -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnLeftArmAbility;
                @LeftArmAbility.performed -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnLeftArmAbility;
                @LeftArmAbility.canceled -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnLeftArmAbility;
                @RightArmAbility.started -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnRightArmAbility;
                @RightArmAbility.performed -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnRightArmAbility;
                @RightArmAbility.canceled -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnRightArmAbility;
                @BodyAbility.started -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnBodyAbility;
                @BodyAbility.performed -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnBodyAbility;
                @BodyAbility.canceled -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnBodyAbility;
                @LegAbility.started -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnLegAbility;
                @LegAbility.performed -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnLegAbility;
                @LegAbility.canceled -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnLegAbility;
                @ResetCameraState.started -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnResetCameraState;
                @ResetCameraState.performed -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnResetCameraState;
                @ResetCameraState.canceled -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnResetCameraState;
                @Exit.started -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnExit;
                @Exit.performed -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnExit;
                @Exit.canceled -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnExit;
                @SelectMenuElement.started -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnSelectMenuElement;
                @SelectMenuElement.performed -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnSelectMenuElement;
                @SelectMenuElement.canceled -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnSelectMenuElement;
                @StopMovement.started -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnStopMovement;
                @StopMovement.performed -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnStopMovement;
                @StopMovement.canceled -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnStopMovement;
                @CameraZoomIn.started -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnCameraZoomIn;
                @CameraZoomIn.performed -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnCameraZoomIn;
                @CameraZoomIn.canceled -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnCameraZoomIn;
                @CameraZoomOut.started -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnCameraZoomOut;
                @CameraZoomOut.performed -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnCameraZoomOut;
                @CameraZoomOut.canceled -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnCameraZoomOut;
                @Fire.started -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_AnalogControllerActionsCallbackInterface.OnFire;
            }
            m_Wrapper.m_AnalogControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @PivotCamera.started += instance.OnPivotCamera;
                @PivotCamera.performed += instance.OnPivotCamera;
                @PivotCamera.canceled += instance.OnPivotCamera;
                @SwitchEnemyTarget.started += instance.OnSwitchEnemyTarget;
                @SwitchEnemyTarget.performed += instance.OnSwitchEnemyTarget;
                @SwitchEnemyTarget.canceled += instance.OnSwitchEnemyTarget;
                @ResetTargetToSelf.started += instance.OnResetTargetToSelf;
                @ResetTargetToSelf.performed += instance.OnResetTargetToSelf;
                @ResetTargetToSelf.canceled += instance.OnResetTargetToSelf;
                @LeftArmAbility.started += instance.OnLeftArmAbility;
                @LeftArmAbility.performed += instance.OnLeftArmAbility;
                @LeftArmAbility.canceled += instance.OnLeftArmAbility;
                @RightArmAbility.started += instance.OnRightArmAbility;
                @RightArmAbility.performed += instance.OnRightArmAbility;
                @RightArmAbility.canceled += instance.OnRightArmAbility;
                @BodyAbility.started += instance.OnBodyAbility;
                @BodyAbility.performed += instance.OnBodyAbility;
                @BodyAbility.canceled += instance.OnBodyAbility;
                @LegAbility.started += instance.OnLegAbility;
                @LegAbility.performed += instance.OnLegAbility;
                @LegAbility.canceled += instance.OnLegAbility;
                @ResetCameraState.started += instance.OnResetCameraState;
                @ResetCameraState.performed += instance.OnResetCameraState;
                @ResetCameraState.canceled += instance.OnResetCameraState;
                @Exit.started += instance.OnExit;
                @Exit.performed += instance.OnExit;
                @Exit.canceled += instance.OnExit;
                @SelectMenuElement.started += instance.OnSelectMenuElement;
                @SelectMenuElement.performed += instance.OnSelectMenuElement;
                @SelectMenuElement.canceled += instance.OnSelectMenuElement;
                @StopMovement.started += instance.OnStopMovement;
                @StopMovement.performed += instance.OnStopMovement;
                @StopMovement.canceled += instance.OnStopMovement;
                @CameraZoomIn.started += instance.OnCameraZoomIn;
                @CameraZoomIn.performed += instance.OnCameraZoomIn;
                @CameraZoomIn.canceled += instance.OnCameraZoomIn;
                @CameraZoomOut.started += instance.OnCameraZoomOut;
                @CameraZoomOut.performed += instance.OnCameraZoomOut;
                @CameraZoomOut.canceled += instance.OnCameraZoomOut;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
            }
        }
    }
    public AnalogControllerActions @AnalogController => new AnalogControllerActions(this);
    public interface IAnalogControllerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnPivotCamera(InputAction.CallbackContext context);
        void OnSwitchEnemyTarget(InputAction.CallbackContext context);
        void OnResetTargetToSelf(InputAction.CallbackContext context);
        void OnLeftArmAbility(InputAction.CallbackContext context);
        void OnRightArmAbility(InputAction.CallbackContext context);
        void OnBodyAbility(InputAction.CallbackContext context);
        void OnLegAbility(InputAction.CallbackContext context);
        void OnResetCameraState(InputAction.CallbackContext context);
        void OnExit(InputAction.CallbackContext context);
        void OnSelectMenuElement(InputAction.CallbackContext context);
        void OnStopMovement(InputAction.CallbackContext context);
        void OnCameraZoomIn(InputAction.CallbackContext context);
        void OnCameraZoomOut(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
    }
}