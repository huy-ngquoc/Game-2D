#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class EnemyGeneralStateMachine : CharacterGeneralStateMachine
    {
        [SerializeField]
        [ResolveComponent]
        private EnemyController enemyController = null!;

        public EnemyGeneralStateMachine()
        {
            this.IdleState = new EnemyIdleState(this);
        }

        public EnemyController EnemyController => this.enemyController;

        public override CharacterController CharacterController => this.enemyController;

        public EnemyIdleState IdleState { get; }

        public override ICharacterState InitialState => this.IdleState;
    }
}
