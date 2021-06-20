using System;
using Scripts.Player;
using Scripts.Secure;
using UnityEngine;
using TMPro;

namespace Scripts.Services
{
   public class GameController : MonoBehaviour 
    {
        public static event Action<int> ChangeCoinsEvent;
        public static event Action<bool> ChangeActivationEvent;
        public static event Action<MusicEvents.AllEvents> MusicEvent;
        
        [SerializeField] private LoadSelectBird _saves;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private TextMeshProUGUI _txtGameName, _txtRecord, _txtCounter;
        
        private WorkWithCoinsAndCout _secure;
        private SafeInt _coin;
        private int _loseStrik;

        private void OnEnable()
        {
            _spawner.GameLose.onChanged += StartGame;
            _spawner.GameLose.onChanged += StopGame;
            MainBird.OnCoinTake += AddCoin;
        }

        private void OnDisable()
        {
            _spawner.GameLose.onChanged -= StartGame;
            _spawner.GameLose.onChanged -= StopGame;
            MainBird.OnCoinTake -= AddCoin;
        }

        private void StartGame(bool newValue, bool oldValue) 
        {
            if (newValue) 
                return;

            ChangeActivationEvent?.Invoke(false);
            _txtCounter.gameObject.SetActive(true);
            _coin = new SafeInt(0);
            _saves.Birds[_saves.BirdSelect].GetComponent<AudioSource>().volume = 0.6f;
        }

        private void StopGame(bool newValue, bool oldValue) 
        {
            if (!newValue) 
                return;
            
            ChangeActivationEvent?.Invoke(true);
            _txtCounter.gameObject.SetActive(false);
            if (_secure.Coin > 0) 
            {
                _secure.CoinUpdate();
                GetAchivAndUpdateLeaderBoard.LoadCoinsInGPS(_secure.Coin);
            } 
            else 
            {
                _secure.CoinUpdate();
            }
            ChangeCoinsEvent?.Invoke(_secure.Coin);
            _loseStrik++;
            if (_loseStrik > 4) 
            {
                if (PlayerPrefsSafe.GetInt("ADS") != 1)
                {
                    AdsLoader.ShowAds();   
                }
                _loseStrik = 0;
            }
            SaveRecord(_spawner.Record);
        }

        private void AddCoin() 
        {
            if (_spawner.GameLose.Value) 
                return;

            _secure.AddCoin(1);
            _coin += new SafeInt(1);
            PlayerPrefsSafe.SetInt("savescoins", _secure.Coin);
            ChangeCoinsEvent?.Invoke(_coin);
        }
        
        private int LoadRecord()
        {
            return PlayerPrefsSafe.HasKey("savescore") ? PlayerPrefsSafe.GetInt("savescore") : 0;
        }

        private void SaveRecord(int newRecord)
        {
            var record = LoadRecord();
            if (newRecord <= record) 
            {
                MusicEvent?.Invoke(MusicEvents.AllEvents.gameLose);
                _txtGameName.text = "Game Over\n" + _txtCounter.text;
                _txtRecord.text = "Your record\n" + record;
            } 
            else 
            {
                MusicEvent?.Invoke(MusicEvents.AllEvents.coinTake);
                new GetAchivAndUpdateLeaderBoard(newRecord);
                _txtRecord.text = ("Your new Record\n" + newRecord);
                _txtGameName.text = "Game Over";
                PlayerPrefsSafe.SetInt("savescore", newRecord);
            }
        }
    } 
}
