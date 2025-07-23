#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerInputHandler : MonoBehaviour, System.IDisposable
    {
        private InputSystemAction inputSystemAction = null!;

        public Vector2 MoveInput { get; private set; } = Vector2.zero;

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

        public void CancelJumpInputAction() => this.JumpPressed = false;

        public void CancelAttackInputAction() => this.AttackPressed = false;

        public void CancelThrowInputAction() => this.ThrowPressed = false;

        public void Dispose()
        {
            this.inputSystemAction?.Dispose();
        }

        private void Awake()
        {
            this.inputSystemAction = new InputSystemAction();
            var playerActions = this.inputSystemAction.Player;

            playerActions.Move.performed += context => this.MoveInput = context.ReadValue<Vector2>();
            playerActions.Move.canceled += context => this.MoveInput = Vector2.zero;

            playerActions.Jump.performed += context => this.JumpPressed = true;
            playerActions.Jump.canceled += context => this.JumpPressed = false;

            playerActions.Attack.performed += context => this.AttackPressed = true;
            playerActions.Attack.canceled += context => this.AttackPressed = false;

            playerActions.Throw.performed += context => this.ThrowPressed = true;
            playerActions.Throw.canceled += context => this.ThrowPressed = false;
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
