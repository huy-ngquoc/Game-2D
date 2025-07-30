#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerKunaiController : MonoBehaviour
    {
        [SerializeField]
        [ResolveComponent]
        private new Rigidbody2D rigidbody2D = null!;

        private LayerMask targetLayerMask = new();
        private float speed = 5;
        private float maxExistanceSeconds = 5;
        private int damage = 10;

        public void Setup(LayerMask targetLayerMask, float speed, float maxExistanceSeconds, int damage)
        {
            this.targetLayerMask = targetLayerMask;
            this.speed = speed;
            this.maxExistanceSeconds = maxExistanceSeconds;
            this.damage = damage;
        }

        public void Destroy()
        {
            Object.Destroy(this.gameObject);
        }

        private void Awake()
        {
            this.Invoke(nameof(this.Destroy), this.maxExistanceSeconds);
        }

        private void Update()
        {
            this.rigidbody2D.linearVelocity = this.transform.right * this.speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var go = collision.gameObject;
            if (!Utils.IsLayerInMask(go, this.targetLayerMask))
            {
                return;
            }

            if (!go.TryGetComponent<EnemyStats>(out var enemyStats))
            {
                return;
            }

            enemyStats.TakeDamage(this.damage);
            this.Destroy();
        }
    }
}
