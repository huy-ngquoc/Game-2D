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

        private Vector3 savePoint = Vector3.zero;
        private string currentAnimationName = "Idle";
        private int coinCounter = 0;
        private bool isJumping = false;
        private bool isAttacking = false;
        private bool isThrowing = false;
        private bool isDead = false;

        [field: SerializeField]
        [field: Range(5, 40)]
        public float MoveSpeed { get; private set; } = 8;

        [field: SerializeField]
        [field: Range(200, 800)]
        public float JumpForce { get; private set; } = 400;

        public bool FacingRight { get; private set; } = true;

        public int FacingDirection => this.FacingRight ? 1 : -1;

        private bool IsGrounded => Physics2D.Raycast(this.transform.position, Vector2.down, 1.2F, this.groundLayerMask);

        public void OnInit()
        {
            this.isJumping = false;
            this.isAttacking = false;
            this.isThrowing = false;
            this.isDead = false;

            this.transform.position = this.savePoint;
            this.ChangeAnimation("Idle");
        }

        public void AttackFinishTrigger() => this.isAttacking = false;

        public void ThrowFinishTrigger() => this.isThrowing = false;

        private void Attack()
        {
            this.isAttacking = true;
            this.inputHandler.CancelAttackInputAction();
            this.ChangeAnimation("Attack");
        }

        private void Throw()
        {
            this.isThrowing = true;
            this.inputHandler.CancelThrowInputAction();
            this.ChangeAnimation("Throw");
        }

        private void Jump()
        {
            this.inputHandler.CancelJumpInputAction();

            this.isJumping = true;
            this.rigidbody2D.AddForceY(this.JumpForce);
            this.ChangeAnimation("Jump");
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

        private void Awake()
        {
            this.savePoint = this.transform.position;
            this.OnInit();
        }

        private void FixedUpdate()
        {
            if (this.isDead || this.isAttacking || this.isThrowing)
            {
                this.rigidbody2D.linearVelocityX = 0;
                return;
            }

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

            if (this.isJumping)
            {
                return;
            }

            if (this.inputHandler.JumpPressed)
            {
                this.Jump();
                return;
            }

            if (this.inputHandler.AttackPressed)
            {
                this.Attack();
                return;
            }

            if (this.inputHandler.ThrowPressed)
            {
                this.Throw();
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Coin"))
            {
                Object.Destroy(collision.gameObject);
                ++this.coinCounter;
            }
            else if (collision.CompareTag("DeathZone"))
            {
                this.isDead = true;
                this.ChangeAnimation("Die");
                this.Invoke(nameof(this.OnInit), 1);
            }
            else if (collision.CompareTag("SpawnPoint"))
            {
                this.savePoint = collision.transform.position;
            }
            else
            {
                // Do nothing...
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, this.transform.position + (Vector3.down * 1.2F));
        }
    }
}
