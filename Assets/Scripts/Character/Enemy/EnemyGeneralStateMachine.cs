#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class EnemyGeneralStateMachine : CharacterGeneralStateMachine
    {
        [SerializeReference]
        [ResolveComponent]
        private EnemyController enemyController = null!;

        [SerializeField]
        [Range(0.5F, 5)]
        private float minIdleTime = 2;

        [SerializeField]
        [Range(2, 10)]
        private float maxIdleTime = 5;

        public EnemyGeneralStateMachine()
        {
            this.IdleState = new EnemyIdleState(this);
            this.RunState = new EnemyRunState(this);
        }

        public EnemyController EnemyController => this.enemyController;

        public override CharacterController CharacterController => this.enemyController;

        public float MinIdleTime => this.minIdleTime;

        public float MaxIdleTime => this.maxIdleTime;

        public EnemyIdleState IdleState { get; }

        public EnemyRunState RunState { get; }

        public override ICharacterState InitialState => this.IdleState;
    }
}
