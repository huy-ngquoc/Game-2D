#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerAnimationTrigger : MonoBehaviour
    {
        [field: SerializeField]
        [field: ResolveComponentInParent]
        private PlayerController controller = null!;

        private void AttackFinishTrigger() => this.controller.AttackFinishTrigger();

        private void ThrowFinishTrigger() => this.controller.ThrowFinishTrigger();
    }
}
