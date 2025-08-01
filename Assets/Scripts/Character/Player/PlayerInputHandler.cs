#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerInputHandler : MonoBehaviour, System.IDisposable
    {
        private Vector2 moveInput = Vector2.zero;

        private InputSystemAction inputSystemAction = null!;

        public Vector2 MoveInput => this.moveInput;

        public float MoveInputX => this.MoveInput.x;

        public float MoveInputY => this.MoveInput.y;

        public Vector2Int MoveInputInt => new(this.MoveInputXInt, this.MoveInputYInt);

        public int MoveInputXInt
        {
            get
            {
                var moveInputX = this.MoveInputX;

                if (moveInputX > 0)
                {
                    return 1;
                }
                else if (moveInputX < 0)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int MoveInputYInt
        {
            get
            {
                var moveInputY = this.MoveInputY;

                if (moveInputY > 0)
                {
                    return 1;
                }
                else if (moveInputY < 0)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool JumpPressed { get; private set; } = false;

        public bool AttackPressed { get; private set; } = false;

        public bool ThrowPressed { get; private set; } = false;

        public bool DashPressed { get; private set; } = false;

        public void PerformMoveLeftInputAction() => this.moveInput.x = -1;

        public void PerformMoveRightInputAction() => this.moveInput.x = 1;

        public void PerformJumpInputAction() => this.JumpPressed = true;

        public void PerformAttackInputAction() => this.AttackPressed = true;

        public void PerformThrowInputAction() => this.ThrowPressed = true;

        public void PerformDashInputAction() => this.DashPressed = true;

        public void CancelMoveInputHorizonalAction() => this.moveInput.x = 0;

        public void CancelJumpInputAction() => this.JumpPressed = false;

        public void CancelAttackInputAction() => this.AttackPressed = false;

        public void CancelThrowInputAction() => this.ThrowPressed = false;

        public void CancelDashInputAction() => this.DashPressed = false;

        public void Dispose()
        {
            this.inputSystemAction?.Dispose();
        }

        private void Awake()
        {
            this.inputSystemAction = new InputSystemAction();
            var playerActions = this.inputSystemAction.Player;

            playerActions.Move.performed += context => this.moveInput = context.ReadValue<Vector2>();
            playerActions.Move.canceled += context => this.moveInput = Vector2.zero;

            playerActions.Jump.performed += context => this.JumpPressed = true;
            playerActions.Jump.canceled += context => this.JumpPressed = false;

            playerActions.Attack.performed += context => this.AttackPressed = true;
            playerActions.Attack.canceled += context => this.AttackPressed = false;

            playerActions.Throw.performed += context => this.ThrowPressed = true;
            playerActions.Throw.canceled += context => this.ThrowPressed = false;

            playerActions.Dash.performed += context => this.DashPressed = true;
            playerActions.Dash.canceled += context => this.DashPressed = false;
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
