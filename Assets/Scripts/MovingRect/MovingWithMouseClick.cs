#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class MovingWithMouseClick : MonoBehaviour
    {
        [SerializeReference]
        [ResolveComponent]
        private UiInputHandler uiInputHandler = null!;

        [SerializeField]
        [Range(5, 200)]
        private float moveSpeed = 10;

        [SerializeField]
        private bool allowInteruption = false;

        private Vector3 currentTargetPosition = Vector3.zero;

        private bool isMoving = false;

        public bool IsInputChangeTarget
            => this.uiInputHandler.LeftMousePressed
             && ((!this.isMoving) || this.allowInteruption);

        private void Awake()
        {
            this.currentTargetPosition = this.transform.position;
        }

        private void Update()
        {
            this.HandleMouseInputChangeTargetPosition();
            this.UpdatePosition();
        }

        private void HandleMouseInputChangeTargetPosition()
        {
            if (!this.IsInputChangeTarget)
            {
                return;
            }

            var mousePositionInScreen = this.uiInputHandler.MousePositionInScreen;
            if (mousePositionInScreen == null)
            {
                return;
            }

            var mousePositionInWorld3D = Camera.main.ScreenToWorldPoint(mousePositionInScreen.Value);
            var mousePositionInWorld2D = new Vector2(mousePositionInWorld3D.x, mousePositionInWorld3D.y);

            this.currentTargetPosition = mousePositionInWorld2D;
            this.isMoving = true;
        }

        private void UpdatePosition()
        {
            if (!this.isMoving)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            var currentPosition = this.transform.position;
            var newPosition = Vector3.MoveTowards(
                currentPosition,
                this.currentTargetPosition,
                this.moveSpeed * deltaTime);

            this.transform.position = newPosition;
            if (newPosition == this.currentTargetPosition)
            {
                this.isMoving = false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(this.currentTargetPosition, 0.1F);
        }
    }
}
