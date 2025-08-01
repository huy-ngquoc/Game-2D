#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerSkillManager : CharacterSkillManager
    {
        [SerializeReference]
        [ResolveComponent]
        private PlayerController playerController = null!;

        [SerializeReference]
        [ResolveComponent]
        private PlayerThrowSkill throwSkill = null!;

        [SerializeReference]
        [ResolveComponent]
        private PlayerAttackSkill attackSkill = null!;

        [SerializeReference]
        [ResolveComponent]
        private PlayerDashSkill dashSkill = null!;

        public PlayerThrowSkill ThrowSkill => this.throwSkill;

        public PlayerAttackSkill AttackSkill => this.attackSkill;

        public PlayerDashSkill DashSkill => this.dashSkill;

        public PlayerController PlayerController => this.playerController;

        public override CharacterController CharacterController => this.playerController;

        public override IAttackSkill CharacterAttackSkill => this.attackSkill;
    }
}
