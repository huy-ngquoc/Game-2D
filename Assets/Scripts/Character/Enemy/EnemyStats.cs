#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class EnemyStats : CharacterStats
    {
        [SerializeReference]
        [ResolveComponent]
        private EnemyController enemyController = null!;

        public EnemyController EnemyController => this.enemyController;

        public override CharacterController CharacterController => this.enemyController;
    }
}
