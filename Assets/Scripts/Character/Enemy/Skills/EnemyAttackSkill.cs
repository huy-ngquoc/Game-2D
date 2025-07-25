#nullable enable

namespace Game
{
    public sealed class EnemyAttackSkill : EnemySkill
    {
        protected override void CastLogic()
        {
            this.EnemyGeneralStateMachine.SetStateToChangeTo(this.EnemyGeneralStateMachine.AttackState);
        }
    }
}
