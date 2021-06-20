using Scripts.Player;
using Scripts.Services;
using UnityEngine;
using TMPro;

namespace Scripts.UI
{
    public class CoinsText : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        
        private TextMeshProUGUI _text = null;

        private void Awake()
        {
            _text = gameObject.GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            GameController.ChangeCoinsEvent += ChangeCoins;
            _spawner.GameLose.onChanged += ResetCoins;  
        }

        private void OnDisable()
        {
            GameController.ChangeCoinsEvent -= ChangeCoins;
            _spawner.GameLose.onChanged -= ResetCoins;
        }

        private void ChangeCoins(int newCoins)
        {
            _text.text = newCoins.ToString();
        }

        private void ResetCoins(bool newValue, bool oldValue)
        {
            if (newValue)
            {
                _text.text = "0";
            }
        }
    }
}
