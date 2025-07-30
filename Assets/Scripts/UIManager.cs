#nullable enable

namespace Game
{
    using System.Globalization;
    using TMPro;
    using UnityEngine;

    public sealed class UIManager : SingletonMonoBehaviour<UIManager>
    {
        [SerializeReference]
        [ResolveComponentInChildren("Coin Text")]
        private TextMeshProUGUI coinText = null!;

        public void SetCoin(int coin)
        {
            this.coinText.text = coin.ToString(CultureInfo.InvariantCulture);
        }
    }
}
