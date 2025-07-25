#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class EnemyAnimationTrigger : CharacterAnimationTrigger
    {
        [SerializeReference]
        [ResolveComponentInParent]
        private EnemyController enemyController = null!;

        public EnemyController EnemyController => this.enemyController;

        public override CharacterController CharacterController => this.enemyController;
    }
}
