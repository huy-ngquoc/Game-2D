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

        public bool IsHeadingToFirstPosition { get; private set; } = false;

        public Transform TargetTransform => this.IsHeadingToFirstPosition ?
            this.firstTransform : this.secondTransform;

        private void Awake()
        {
            this.transform.position = this.firstTransform.position;
        }

        private void FixedUpdate()
        {
            var targetPosition = this.TargetTransform.position;

            var newPosition = Vector3.MoveTowards(
                this.transform.position,
                targetPosition,
                this.Speed * Time.fixedDeltaTime);

            var platformVelocity = (newPosition - this.transform.position) / Time.fixedDeltaTime;
            this.rigidbody2D.linearVelocity = platformVelocity;

            if (Vector2.Distance(newPosition, targetPosition) < Mathf.Epsilon)
            {
                this.IsHeadingToFirstPosition = !this.IsHeadingToFirstPosition;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var gameObject = collision.gameObject;
            if (Utils.IsLayerInMask(gameObject, this.PlayerLayerMask))
            {
                gameObject.transform.SetParent(this.transform);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            var gameObject = collision.gameObject;
            if (Utils.IsLayerInMask(gameObject, this.PlayerLayerMask))
            {
                gameObject.transform.SetParent(null);
            }
        }
    }
}
