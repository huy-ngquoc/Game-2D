#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerController : CharacterController
    {
        [SerializeReference]
        [ResolveComponent]
        private PlayerInputHandler inputHandler = null!;

        [SerializeReference]
        [ResolveComponent]
        private PlayerGeneralStateMachine playerGeneralStateMachine = null!;

        [SerializeField]
        [Range(200, 2000)]
        private float jumpForce = 500;

        private int coinCounter = 0;

        public Vector3 SavePoint { get; private set; } = Vector3.zero;

        public Vector2 PlatformVelocity { get; private set; } = Vector2.zero;

        public PlayerInputHandler InputHandler => this.inputHandler;

        public PlayerGeneralStateMachine PlayerGeneralStateMachine => this.playerGeneralStateMachine;

        public override CharacterGeneralStateMachine CharacterGeneralStateMachine => this.playerGeneralStateMachine;

        public float JumpForce => this.jumpForce;

        protected override void OnCharacterControllerAwake()
        {
            this.SavePoint = this.transform.position;
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
                this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.DeadState);
            }
            else if (collision.CompareTag("SpawnPoint"))
            {
                this.SavePoint = collision.transform.position;
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
                    this.PlatformVelocity = platform.PlatformVelocity;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (Utils.IsLayerInMask(collision.gameObject, this.GroundLayerMask))
            {
                this.PlatformVelocity = Vector2.zero;
            }
        }
    }
}
