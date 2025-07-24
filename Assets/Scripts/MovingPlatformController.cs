#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class MovingPlatformController : MonoBehaviour
    {
        [SerializeReference]
        [ResolveComponent]
        private new Rigidbody2D rigidbody2D = null!;

        [SerializeReference]
        [RequireReference]
        private Transform firstTransform = null!;

        [SerializeReference]
        [RequireReference]
        private Transform secondTransform = null!;

        [SerializeField]
        [LayerMaskIsNothingOrEverythingWarning]
        private LayerMask playerLayerMask = new();

        [SerializeField]
        [Range(5, 50)]
        private float speed = 10;

        private bool isHeadingToFirstPosition = false;

        private MovingPlatformController()
        {
        }

        public Transform TargetTransform => this.isHeadingToFirstPosition ?
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
                this.speed * Time.fixedDeltaTime);

            var platformVelocity = (newPosition - this.transform.position) / Time.fixedDeltaTime;
            this.rigidbody2D.linearVelocity = platformVelocity;

            if (Vector2.Distance(newPosition, targetPosition) < Mathf.Epsilon)
            {
                this.isHeadingToFirstPosition = !this.isHeadingToFirstPosition;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var gameObject = collision.gameObject;
            if (Utils.IsLayerInMask(gameObject, this.playerLayerMask))
            {
                gameObject.transform.SetParent(this.transform);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            var gameObject = collision.gameObject;
            if (Utils.IsLayerInMask(gameObject, this.playerLayerMask))
            {
                gameObject.transform.SetParent(null);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(this.firstTransform.position, 0.5F);
            Gizmos.DrawSphere(this.secondTransform.position, 0.5F);
        }
    }
}
