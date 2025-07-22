#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class EnemyController : CharacterController
    {
        [SerializeField]
        private float attackRange = 0;
    }
}
