#nullable enable

namespace Game
{
    using UnityEngine;

    public sealed class PlayerThrowSkill : PlayerSkill
    {
        [SerializeField]
        [ResolveComponentInChildren("Throw Point")]
        private Transform throwTransform = null!;

        [Header("Kunai Info")]

        [SerializeField]
        [RequireReference]
        private GameObject kunaiPrefab = null!;

        [SerializeField]
        [Range(0.5F, 10)]
        private float kunaiSpeed = 5;

        [SerializeField]
        [Range(1, 10)]
        private float kunaiMaxExistanceSeconds = 5;

        protected override void CastLogic()
        {
            var kunai = Object.Instantiate(this.kunaiPrefab, this.throwTransform.position, this.transform.rotation, null);
            if (!kunai.TryGetComponent<PlayerKunaiController>(out var kunaiController))
            {
                Debug.LogError($"No component {nameof(PlayerKunaiController)} on Kunai Prefab!");
            }

            kunaiController.Setup(this.PlayerController.AttackTargetLayerMask, this.kunaiSpeed, this.kunaiMaxExistanceSeconds);

            this.PlayerGeneralStateMachine.SetStateToChangeTo(this.PlayerGeneralStateMachine.ThrowState);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.darkRed;
            Gizmos.DrawSphere(this.throwTransform.position, 0.1F);
        }
    }
}
