#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerController : MonoBehaviour
    {
        [field: SerializeField]
        [field: ResolveComponent]
        private PlayerInputHandler inputHandler = null!;

        [field: SerializeField]
        [field: ResolveComponent]
        private new Rigidbody2D rigidbody2D = null!;

        [field: SerializeField]
        [field: LayerMaskIsNothingOrEverythingWarning]
        private LayerMask groundLayerMask = new();

        [field: SerializeField]
        [field: ResolveComponentInChildren]
        private Animator animator = null!;

        private string currentAnimationName = "Idle";
        private bool isJumping = false;
        private bool isAttacking = false;

        [field: SerializeField]
        [field: Range(5, 40)]
        public float MoveSpeed { get; private set; } = 8;

        [field: SerializeField]
        [field: Range(200, 800)]
        public float JumpForce { get; private set; } = 400;

        public bool FacingRight { get; private set; } = true;

        public int FacingDirection => this.FacingRight ? 1 : -1;

        private bool IsGrounded => Physics2D.Raycast(this.transform.position, Vector2.down, 1.2F, this.groundLayerMask);

        private void Attack()
        {
        }

        private void Throw()
        {
        }

        private void Jump()
        {
        }

        private void FlipController(float x)
        {
            bool flip = this.FacingRight ? (x < 0) : (x > 0);
            if (flip)
            {
                this.Flip();
            }
        }

        private void Flip()
        {
            this.FacingRight = !this.FacingRight;
            this.transform.Rotate(0, 180, 0);
        }

        private void ChangeAnimation(string newAnimationName)
        {
            if (this.currentAnimationName != newAnimationName)
            {
                this.animator.ResetTrigger(this.currentAnimationName);
                this.currentAnimationName = newAnimationName;
                this.animator.SetTrigger(this.currentAnimationName);
            }
        }

        private void Start()
        {
        }

        private void FixedUpdate()
        {
            var isGrounded = this.IsGrounded;

            var moveSpeedX = this.inputHandler.MoveInputX;
            var linearVelocityX = moveSpeedX * this.MoveSpeed;
            this.rigidbody2D.linearVelocityX = linearVelocityX;
            this.FlipController(linearVelocityX);

            if (!isGrounded)
            {
                if (this.rigidbody2D.linearVelocityY <= 0)
                {
                    this.isJumping = false;
                    this.ChangeAnimation("Fall");
                }

                return;
            }

            if (this.inputHandler.JumpPressed)
            {
                this.inputHandler.CancelJumpInputAction();
                this.isJumping = true;

                this.rigidbody2D.AddForceY(this.JumpForce);
                this.ChangeAnimation("Jump");

                return;
            }

            if (this.isJumping)
            {
                return;
            }

            if (Mathf.Abs(linearVelocityX) > Mathf.Epsilon)
            {
                this.ChangeAnimation("Run");
            }
            else
            {
                this.ChangeAnimation("Idle");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, this.transform.position + (Vector3.down * 1.2F));
        }
    }
}
