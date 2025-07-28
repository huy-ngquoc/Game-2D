#nullable enable

namespace Game
{
    using System;
    using UnityEngine;

    public sealed class UiInputHandler : MonoBehaviour, IDisposable
    {
        private InputSystemAction inputSystemAction = null!;

        public Vector2? MousePositionInScreen { get; private set; } = null;

        public bool LeftMousePressed { get; private set; } = false;

        public void CancelLeftMousePressed() => this.LeftMousePressed = false;

        public void Dispose()
        {
            this.inputSystemAction?.Dispose();
        }

        private void Awake()
        {
            this.inputSystemAction = new InputSystemAction();
            var uiActions = this.inputSystemAction.Ui;

            uiActions.MousePosition.performed += context => this.MousePositionInScreen = context.ReadValue<Vector2>();
            uiActions.MousePosition.canceled += context => this.MousePositionInScreen = null;

            uiActions.LeftMousePressed.performed += context => this.LeftMousePressed = true;
            uiActions.LeftMousePressed.canceled += context => this.LeftMousePressed = false;
        }

        private void OnEnable()
        {
            this.inputSystemAction!.Enable();
        }

        private void OnDisable()
        {
            this.inputSystemAction!.Disable();
        }

        private void OnDestroy()
        {
            this.inputSystemAction!.Dispose();
        }
    }
}
