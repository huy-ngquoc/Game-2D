#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerAnimationTrigger : CharacterAnimationTrigger
    {
        [field: SerializeField]
        [field: ResolveComponentInParent]
        public PlayerController PlayerController { get; private set; } = null!;

        public override CharacterController CharacterController => this.PlayerController;
    }
}
