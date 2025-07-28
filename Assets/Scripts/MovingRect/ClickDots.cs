#nullable enable

namespace Game
{
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class ClickDots : MonoBehaviour
    {
        [SerializeField]
        [ResolveComponent]
        private UiInputHandler uiInputHandler = null!;

        [SerializeField]
        private List<GameObject> levelPrefabs = new();

        public int MaxLevel => this.levelPrefabs.Count;

        public void SpawnCircle(Vector3 localPosition, int level)
        {
            level = System.Math.Clamp(level, 1, this.MaxLevel);
            var prefab = this.levelPrefabs[level - 1];

            var go = Instantiate(prefab, localPosition, Quaternion.identity, this.transform);
            if (!go.TryGetComponent<ClickDotController>(out var controller))
            {
                Debug.LogError($"Missing ClickDotController in prefabs of level {level}.");
                Object.Destroy(go);
            }

            controller.Setup(this, level);
        }

        private void Update()
        {
            if (!this.uiInputHandler.LeftMousePressed)
            {
                return;
            }

            this.uiInputHandler.CancelLeftMousePressed();
            var mousePositionInScreen = this.uiInputHandler.MousePositionInScreen;
            if (!mousePositionInScreen.HasValue)
            {
                return;
            }

            var mousePositionInWorld3D = Camera.main.ScreenToWorldPoint(mousePositionInScreen.Value);
            mousePositionInWorld3D.z = 0;
            this.SpawnCircle(mousePositionInWorld3D, 1);
        }
    }
}
