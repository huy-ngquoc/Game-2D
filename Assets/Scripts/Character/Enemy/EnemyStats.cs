#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class EnemyStats : CharacterStats
    {
        [SerializeReference]
        [ResolveComponent]
        private EnemyController enemyController = null!;

        [SerializeField]
        [Range(5, 20)]
        private float detectionRange = 10;

        [SerializeField]
        [Range(0.5F, 5)]
        private float attackRange = 5;

        public EnemyController EnemyController => this.enemyController;

        public override CharacterController CharacterController => this.enemyController;

        public float DetectionRange => this.detectionRange;

        public float AttackRange => this.attackRange;
    }
}
