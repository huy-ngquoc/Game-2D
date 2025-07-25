#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerSkillManager : CharacterSkillManager
    {
        [SerializeReference]
        [ResolveComponent]
        private PlayerController playerController = null!;

        public PlayerController PlayerController => this.playerController;

        public override CharacterController CharacterController => this.playerController;
    }
}
