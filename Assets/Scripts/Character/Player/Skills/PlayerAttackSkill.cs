#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerAttackSkill : PlayerSkill, IAttackSkill
    {
        [SerializeField]
        [Range(1, 100)]
        private int damage = 30;

        public int Damage => this.damage;

        protected override void CastLogic()
        {
            this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.AttackState);
        }
    }
}
