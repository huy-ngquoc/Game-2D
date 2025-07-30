#nullable enable

namespace Game
{
    using System.Globalization;
    using TMPro;
    using UnityEngine;

    public sealed class CombatTextUIController : MonoBehaviour
    {
        [SerializeReference]
        [ResolveComponentInChildren]
        private TextMeshProUGUI hpText = null!;

        [SerializeReference]
        [ResolveComponent]
        private Canvas canvas = null!;

        public void Setup(Camera camera, float hp)
        {
            this.canvas.worldCamera = camera;
            this.hpText.text = hp.ToString(CultureInfo.InvariantCulture);
            this.Invoke(nameof(this.Destroy), 1);
        }

        private void Destroy()
        {
            Object.Destroy(this.gameObject);
        }
    }
}
