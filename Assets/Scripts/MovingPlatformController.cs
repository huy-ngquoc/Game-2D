#nullable enable

namespace Game
{
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class MovingPlatformController : MonoBehaviour
    {
        [SerializeReference]
        private List<Vector3> targetPositions = new();

        private int targetPositionsIdx = 0;

        [SerializeField]
        [LayerMaskIsNothingOrEverythingWarning]
        private LayerMask characterLayerMask = new();

        [SerializeField]
        [Range(5, 50)]
        private float speed = 10;

        private MovingPlatformController()
        {
        }

        private void Awake()
        {
            if (this.targetPositions.Count > 0)
            {
                this.transform.position = this.targetPositions[0];
            }
            this.targetPositionsIdx = 1;
        }

        private void Update()
        {
            var amountTargetTransforms = this.targetPositions.Count;
            if (amountTargetTransforms <= 1)
            {
                return;
            }

            var currentPosition = this.transform.localPosition;
            this.targetPositionsIdx = Utils.ModPositive(this.targetPositionsIdx, amountTargetTransforms);

            var deltaTime = Time.deltaTime;
            var newPosition = Vector3.MoveTowards(
                currentPosition,
                this.targetPositions[this.targetPositionsIdx],
                this.speed * deltaTime);

            this.transform.localPosition = newPosition;

            if (newPosition == this.targetPositions[this.targetPositionsIdx])
            {
                ++this.targetPositionsIdx;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var collisionGameObject = collision.gameObject;
            if (!Utils.IsLayerInMask(collisionGameObject, this.characterLayerMask))
            {
                return;
            }

            if (!collisionGameObject.TryGetComponent<CharacterController>(out var characterController))
            {
                return;
            }

            if (!characterController.IsGroundDetected)
            {
                return;
            }

            collisionGameObject.transform.SetParent(this.transform);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            var gameObject = collision.gameObject;
            if (Utils.IsLayerInMask(gameObject, this.characterLayerMask))
            {
                gameObject.transform.SetParent(null);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;

            var parentTransform = this.transform.parent;
            if (parentTransform != null)
            {
                foreach (var targetPosition in this.targetPositions)
                {
                    Gizmos.DrawSphere(this.transform.parent.TransformPoint(targetPosition), 0.1F);
                }
            }
            else
            {
                foreach (var targetPosition in this.targetPositions)
                {
                    Gizmos.DrawSphere(targetPosition, 0.1F);
                }
            }
        }
    }
}
