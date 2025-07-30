#nullable enable

namespace Game
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public sealed class HealthBarUIController : MonoBehaviour
    {
        [field: SerializeField]
        [field: ResolveComponentInParent]
        private CharacterController characterController = null!;

        [field: SerializeField]
        [field: ResolveComponent]
        private RectTransform rectTransform = null!;

        [field: SerializeField]
        [field: ResolveComponentInChildren]
        private Slider slider = null!;

        public CharacterStats CharacterStats => this.characterController.CharacterStats;

        private void OnEnable()
        {
            this.characterController.Flipped += this.CharacterController_Flipped;
            this.CharacterStats.HealthChanged += this.CharacterStats_HealthChanged;
        }

        private void OnDisable()
        {
            this.characterController.Flipped -= this.CharacterController_Flipped;
            this.CharacterStats.HealthChanged -= this.CharacterStats_HealthChanged;
        }

        private void CharacterController_Flipped(object sender, EventArgs e)
        {
            this.rectTransform.Rotate(0, 180, 0);
        }

        private void CharacterStats_HealthChanged(object sender, EventArgs e)
        {
            this.slider.maxValue = this.CharacterStats.BaseHealth;
            this.slider.value = this.CharacterStats.CurrentHealth;
        }
    }
}
