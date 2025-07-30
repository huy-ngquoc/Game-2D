#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class EnemyController : CharacterController
    {
        [Header("Enemy Specific Component")]

        [SerializeReference]
        [ResolveComponent]
        private EnemyGeneralStateMachine enemyGeneralStateMachine = null!;

        [SerializeReference]
        [ResolveComponent]
        private EnemyStats enemyStats = null!;

        [SerializeReference]
        [ResolveComponent]
        private EnemySkillManager enemySkillManager = null!;

        public EnemyGeneralStateMachine EnemyGeneralStateMachine => this.enemyGeneralStateMachine;

        public override CharacterGeneralStateMachine CharacterGeneralStateMachine => this.enemyGeneralStateMachine;

        public EnemyStats EnemyStats => this.enemyStats;

        public override CharacterStats CharacterStats => this.enemyStats;

        public EnemySkillManager EnemySkillManager => this.enemySkillManager;

        public override CharacterSkillManager CharacterSkillManager => this.enemySkillManager;

        public RaycastHit2D TargetRaycastHit2D
            => Physics2D.Raycast(
                this.transform.position,
                Vector2.right * this.FacingDirection,
                this.EnemySkillManager.AttackSkill.DetectionRange,
                this.AttackTargetLayerMask);
    }
}
