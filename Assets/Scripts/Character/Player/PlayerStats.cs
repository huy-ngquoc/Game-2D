#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerStats : CharacterStats
    {
        [SerializeReference]
        [ResolveComponent]
        private PlayerController playerController = null!;

        [SerializeField]
        [Range(200, 2000)]
        private float jumpForce = 500;

        public PlayerController PlayerController => this.playerController;

        public override CharacterController CharacterController => this.playerController;

        public float JumpForce => this.jumpForce;
    }
}
