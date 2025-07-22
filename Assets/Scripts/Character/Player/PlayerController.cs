#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerController : CharacterController
    {
        [field: SerializeField]
        [field: ResolveComponent]
        private PlayerInputHandler inputHandler = null!;

        private Vector3 savePoint = Vector3.zero;
        private Vector2 platformVelocity = Vector2.zero;
        private int coinCounter = 0;
        private bool isJumping = false;
        private bool isAttacking = false;
        private bool isThrowing = false;
        private bool isDead = false;

        [field: SerializeField]
        [field: Range(200, 800)]
        public float JumpForce { get; private set; } = 400;

        public void AttackFinishTrigger() => this.isAttacking = false;

        public void ThrowFinishTrigger() => this.isThrowing = false;

        protected override void OnCharacterControllerInit()
        {
            this.isJumping = false;
            this.isAttacking = false;
            this.isThrowing = false;
            this.isDead = false;

            this.transform.position = this.savePoint;
            this.ChangeAnimation("Idle");
        }

        protected override void OnCharacterControllerAwake()
        {
            this.savePoint = this.transform.position;
        }

        protected override void OnCharacterControllerFixedUpdate()
        {
            if (this.isDead || this.isAttacking || this.isThrowing)
            {
                this.Rigidbody2D.linearVelocityX = this.platformVelocity.x;
                return;
            }

            var moveSpeedX = this.inputHandler.MoveInputX * this.MoveSpeed;
            var linearVelocityX = moveSpeedX + this.platformVelocity.x;
            this.Rigidbody2D.linearVelocityX = linearVelocityX;
            this.FlipController(moveSpeedX);

            if (this.Rigidbody2D.linearVelocityY <= 0)
            {
                this.isJumping = false;
            }

            if (!this.IsGroundDetected)
            {
                this.ChangeAnimation("Fall");
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

            if (Mathf.Abs(moveSpeedX) > Mathf.Epsilon)
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
                this.Invoke(nameof(this.Init), 1);
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

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (Utils.IsLayerInMask(collision.gameObject, this.GroundLayerMask))
            {
                if (collision.gameObject.TryGetComponent<MovingPlatformController>(out var platform))
                {
                    this.platformVelocity = platform.PlatformVelocity;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (Utils.IsLayerInMask(collision.gameObject, this.GroundLayerMask))
            {
                this.platformVelocity = Vector2.zero;
            }
        }

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
            this.Rigidbody2D.AddForceY(this.JumpForce);
            this.ChangeAnimation("Jump");
        }
    }
}
