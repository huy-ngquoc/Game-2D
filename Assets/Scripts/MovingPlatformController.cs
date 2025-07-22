#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class MovingPlatformController : MonoBehaviour
    {
        [field: SerializeField]
        [field: ResolveComponent]
        private new Rigidbody2D rigidbody2D = null!;

        [field: SerializeField]
        [field: RequireReference]
        private Transform firstTransform = null!;

        [field: SerializeField]
        [field: RequireReference]
        private Transform secondTransform = null!;

        private MovingPlatformController()
        {
        }

        [field: SerializeField]
        [field: Range(5, 50)]
        public float Speed { get; private set; } = 10;

        [field: SerializeField]
        [field: LayerMaskIsNothingOrEverythingWarning]
        public LayerMask PlayerLayerMask { get; private set; } = new();

        public Vector2 PlatformVelocity { get; private set; } = Vector2.zero;

        public bool IsHeadingToFirstPosition { get; private set; } = false;

        public Vector3 TargetPosition => this.IsHeadingToFirstPosition ?
            this.firstTransform.position : this.secondTransform.position;

        private void Awake()
        {
            this.transform.position = this.firstTransform.position;
        }

        private void FixedUpdate()
        {
            var targetPosition = this.TargetPosition;

            var newPosition = Vector3.MoveTowards(
                this.transform.position,
                targetPosition,
                this.Speed * Time.fixedDeltaTime);

            this.PlatformVelocity = (newPosition - this.transform.position) / Time.fixedDeltaTime;
            this.rigidbody2D.linearVelocity = this.PlatformVelocity;

            if (Vector2.Distance(newPosition, targetPosition) < Mathf.Epsilon)
            {
                this.IsHeadingToFirstPosition = !this.IsHeadingToFirstPosition;
            }
        }
    }
}
