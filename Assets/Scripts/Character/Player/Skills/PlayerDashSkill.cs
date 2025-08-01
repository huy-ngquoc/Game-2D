#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerDashSkill : PlayerSkill
    {
        [field: SerializeField]
        [field: Range(5, 200)]
        public float DashSpeed { get; private set; } = 25;

        [field: SerializeField]
        [field: Range(0.01F, 1)]
        public float DashDuration { get; private set; } = 0.2F;

        protected override void CastLogic()
        {
            this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.DashState);
        }
    }
}
