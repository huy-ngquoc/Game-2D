#nullable enable

namespace Game
{
    public sealed class PlayerAttackSkill : PlayerSkill
    {
        protected override void CastLogic()
        {
            this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.AttackState);
        }
    }
}
