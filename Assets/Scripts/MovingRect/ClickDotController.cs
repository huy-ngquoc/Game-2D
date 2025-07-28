#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class ClickDotController : MonoBehaviour
    {
        private ClickDots clickDots = null!;
        private int level = 1;
        private bool isMerging = false;

        public void Setup(ClickDots clickDots, int level)
        {
            this.clickDots = clickDots;
            this.level = level;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (this.isMerging || (this.level >= this.clickDots.MaxLevel))
            {
                return;
            }

            if (!collision.gameObject.TryGetComponent<ClickDotController>(out var other))
            {
                return;
            }

            if (other.isMerging || (this.level != other.level))
            {
                return;
            }

            this.isMerging = other.isMerging = true;

            var mergePos = (this.transform.localPosition + other.transform.localPosition) / 2;

            Object.Destroy(other.gameObject);
            Object.Destroy(this.gameObject);

            this.clickDots.SpawnCircle(mergePos, this.level + 1);
        }
    }
}
