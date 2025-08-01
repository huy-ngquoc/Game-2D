#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerGeneralStateMachine : CharacterGeneralStateMachine
    {
        [SerializeReference]
        [ResolveComponent]
        private PlayerController playerController = null!;

        public PlayerGeneralStateMachine()
        {
            this.GroundState = new PlayerGroundedStateMachine(this);
            this.FallState = new PlayerFallState(this);
            this.DeadState = new PlayerDeadState(this);
            this.JumpState = new PlayerJumpState(this);
            this.ThrowState = new PlayerThrowState(this);
            this.AttackState = new PlayerAttackState(this);
            this.DashState = new PlayerDashState(this);
        }

        public PlayerController PlayerController => this.playerController;

        public override CharacterController CharacterController => this.PlayerController;

        public PlayerStats PlayerStats => this.PlayerController.PlayerStats;

        public PlayerGroundedStateMachine GroundState { get; }

        public PlayerFallState FallState { get; }

        public PlayerDeadState DeadState { get; }

        public PlayerJumpState JumpState { get; }

        public PlayerThrowState ThrowState { get; }

        public PlayerAttackState AttackState { get; }

        public PlayerDashState DashState { get; }

        public override ICharacterState InitialState => this.GroundState;
    }
}
