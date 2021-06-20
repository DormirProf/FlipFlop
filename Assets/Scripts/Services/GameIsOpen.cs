using GooglePlayGames;
using System;
using System.Text;
using Scripts.Secure;
using TMPro;
using UnityEngine;

namespace Scripts.Services
{
    class GameIsOpen : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonLeaderBoard;
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
           PlayGamesPlatform.Activate();
           Social.localUser.Authenticate((bool success) =>
           {
               if (success)
               {
                   _buttonLeaderBoard.SetActive(true);
               }
           });
           if (PlayerPrefsSafe.GetInt("ADS") != 1)
           {
               if (AdsLoader.BannerActive != true)
               {
                   AdsLoader.LoadBaner();
               }
               else
               {
                   AdsLoader.Banner.Show();
               }
           }
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

