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

        public PlayerThrowSkill ThrowSkill => this.throwSkill;

        public PlayerController PlayerController => this.playerController;

        public override CharacterController CharacterController => this.playerController;
    }
}
