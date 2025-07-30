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

        public PlayerThrowSkill ThrowSkill => this.throwSkill;

        public PlayerAttackSkill AttackSkill => this.attackSkill;

        public PlayerController PlayerController => this.playerController;

        public override CharacterController CharacterController => this.playerController;

        public override IAttackSkill CharacterAttackSkill => this.attackSkill;
    }
}
