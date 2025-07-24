#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class EnemyController : CharacterController
    {
        [SerializeReference]
        [ResolveComponent]
        private EnemyGeneralStateMachine enemyGeneralStateMachine = null!;

        [SerializeReference]
        [ResolveComponent]
        private EnemyStats enemyStats = null!;

        public EnemyGeneralStateMachine EnemyGeneralStateMachine => this.enemyGeneralStateMachine;

        public override CharacterGeneralStateMachine CharacterGeneralStateMachine => this.enemyGeneralStateMachine;

        public EnemyStats EnemyStats => this.enemyStats;

        public override CharacterStats CharacterStats => this.enemyStats;

        public RaycastHit2D IsPlayerDetected
            => Physics2D.Raycast(
                this.transform.position,
                Vector2.right * this.FacingDirection,
                this.EnemyStats.DetectionRange,
                this.AttackTargetLayerMask);
    }
}
