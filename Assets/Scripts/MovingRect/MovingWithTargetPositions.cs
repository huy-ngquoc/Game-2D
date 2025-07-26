#nullable enable

namespace Game
{
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class MovingWithTargetPositions : MonoBehaviour
    {
        [SerializeReference]
        private List<Vector3> targetPositions = new();

        private int targetPositionsIdx = 0;

        [SerializeField]
        [Range(5, 200)]
        private float moveSpeed = 10;

        [SerializeField]
        [Range(-45, 45)]
        private float rotationSpeed = 0;

        [SerializeField]
        [Range(0, 10)]
        private float delayTime = 0;

        [SerializeField]
        private bool isRandom = false;

        private float timeDelayed = 0;

        private bool isMoving = true;

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

            var deltaTime = Time.deltaTime;
            if (!this.isMoving)
            {
                this.timeDelayed += deltaTime;
                if (this.timeDelayed < this.delayTime)
                {
                    return;
                }

                this.timeDelayed = 0;
                this.isMoving = true;
            }

            var currentPosition = this.transform.localPosition;
            this.targetPositionsIdx = Utils.ModPositive(this.targetPositionsIdx, amountTargetTransforms);

            var newPosition = Vector3.MoveTowards(
                currentPosition,
                this.targetPositions[this.targetPositionsIdx],
                this.moveSpeed * deltaTime);

            this.transform.localPosition = newPosition;

            this.transform.Rotate(new Vector3(0, 0, this.rotationSpeed));

            if (newPosition == this.targetPositions[this.targetPositionsIdx])
            {
                this.isMoving = false;

                if (this.isRandom)
                {
                    var newIdx = Random.Range(0, amountTargetTransforms);
                    if (newIdx == this.targetPositionsIdx)
                    {
                        ++newIdx;
                    }
                    this.targetPositionsIdx = newIdx;
                }
                else
                {
                    ++this.targetPositionsIdx;
                }
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
