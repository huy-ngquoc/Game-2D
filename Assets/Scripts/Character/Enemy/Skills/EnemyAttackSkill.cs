#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class EnemyAttackSkill : EnemySkill, IAttackSkill
    {
        [SerializeField]
        [Range(5, 20)]
        private float detectionRange = 10;

        [SerializeField]
        [Range(0.5F, 5)]
        private float attackRange = 5;

        [SerializeField]
        [Range(1, 100)]
        private int damage = 30;

        public float DetectionRange => this.detectionRange;

        public float AttackRange => this.attackRange;

        public int Damage => this.damage;

        protected override void CastLogic()
        {
            this.EnemyGeneralStateMachine.SetStateToChangeTo(this.EnemyGeneralStateMachine.AttackState);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.transform.position, new Vector3(this.transform.position.x + (this.detectionRange * this.EnemyController.FacingDirection), this.transform.position.y));

            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, new Vector3(this.transform.position.x + (this.attackRange * this.EnemyController.FacingDirection), this.transform.position.y));
        }
    }
}
