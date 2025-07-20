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
        public float MoveSpeed { get; private set; } = 8;

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
            var moveSpeedX = this.inputHandler.MoveInputX;
            var linearVelocityX = moveSpeedX * this.MoveSpeed;

            this.rigidbody2D.linearVelocityX = linearVelocityX;

            if (Mathf.Abs(linearVelocityX) > Mathf.Epsilon)
            {
                this.ChangeAnimation("Run");
            }
            else
            {
                this.ChangeAnimation("Idle");
            }

            this.FlipController(linearVelocityX);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, this.transform.position + (Vector3.down * 1.2F));
        }
    }
}
