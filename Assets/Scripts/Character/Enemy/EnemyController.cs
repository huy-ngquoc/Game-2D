#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class EnemyController : CharacterController
    {
        [SerializeField]
        [ResolveComponent]
        private EnemyGeneralStateMachine enemyGeneralStateMachine = null!;

        [SerializeField]
        private float attackRange = 0;

        public override CharacterGeneralStateMachine CharacterGeneralStateMachine => this.enemyGeneralStateMachine;
    }
}
