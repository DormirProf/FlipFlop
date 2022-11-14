using Scripts.Secure;
using TMPro;
using UnityEngine;

namespace Scripts.Services
{
    class GameIsOpen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _txtRecord, _txtCoinsInMenu;
        
        private WorkWithCoinsAndCout _secureCoin;
        private LoadSelectBird _loadSelectBird = null;

        private void Awake()
        {
            _loadSelectBird = GetComponent<LoadSelectBird>();
            Application.targetFrameRate = 120;
        }

        private void Start()
        {
            LoadRecord();
            LoadCoins();
            _loadSelectBird.LoadSave();
        }

        private void LoadRecord()
        {
            if (!PlayerPrefsSafe.HasKey("savescore")) 
                return;
            
            _txtRecord.text = "Your record\n" + PlayerPrefsSafe.GetInt("savescore");
        }

        private void LoadCoins() => 
            _txtCoinsInMenu.text = PlayerPrefsSafe.GetInt("savescoins").ToString();
        }
}

