#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class EnemySkillManager : CharacterSkillManager
    {
        [SerializeReference]
        [ResolveComponent]
        private EnemyController enemyController = null!;

        [SerializeReference]
        [ResolveComponent]
        private EnemyAttackSkill attackSkill = null!;

        public EnemyController EnemyController => this.enemyController;

        public override CharacterController CharacterController => this.enemyController;

        public EnemyAttackSkill AttackSkill => this.attackSkill;
    }
}
