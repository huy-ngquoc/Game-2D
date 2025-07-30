#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerController : CharacterController
    {
        [Header("Player Specific Component")]

        [SerializeReference]
        [ResolveComponent]
        private PlayerInputHandler inputHandler = null!;

        [SerializeReference]
        [ResolveComponent]
        private PlayerGeneralStateMachine playerGeneralStateMachine = null!;

        [SerializeReference]
        [ResolveComponent]
        private PlayerStats playerStats = null!;

        [SerializeReference]
        [ResolveComponent]
        private PlayerSkillManager playerSkillManager = null!;

        private int coinCounter = 0;

        public Vector3 SavePoint { get; private set; } = Vector3.zero;

        public PlayerInputHandler InputHandler => this.inputHandler;

        public PlayerGeneralStateMachine PlayerGeneralStateMachine => this.playerGeneralStateMachine;

        public override CharacterGeneralStateMachine CharacterGeneralStateMachine => this.playerGeneralStateMachine;

        public PlayerStats PlayerStats => this.playerStats;

        public override CharacterStats CharacterStats => this.playerStats;

        public PlayerSkillManager PlayerSkillManager => this.playerSkillManager;

        public override CharacterSkillManager CharacterSkillManager => this.playerSkillManager;

        protected override void OnCharacterControllerSetup()
        {
            UIManager.Instance.UnityAccess(i => i.SetCoin(this.coinCounter));
            this.transform.position = this.SavePoint;
        }

        protected override void OnCharacterControllerDie()
        {
            this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.DeadState);
        }

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
                UIManager.Instance.UnityAccess(i => i.SetCoin(this.coinCounter));
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
    }
}
