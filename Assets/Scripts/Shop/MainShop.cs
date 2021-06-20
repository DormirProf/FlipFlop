using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using Scripts.Secure;
using Scripts.Services;
using Scripts.Shop;

namespace Scripts.Payes
{
    public class MainShop : MonoBehaviour 
    {
        [SerializeField] private LoadSelectBird _saves;
        [SerializeField] [Space] private Image[] _buttonsImages;
        [SerializeField] [Space] private TextMeshProUGUI[] _payBirdsText;
        [SerializeField] private GameObject _shop, _game;
        [SerializeField] private GameObject _bird4, _bird4Payed;
        [SerializeField] private TextMeshProUGUI _txtCoinsInShop, _txtCoinsInMenu;
        
        private Color32 _greenColor = new Color32(134, 207, 152, 255);
        private Color32 _redColor = new Color32(207, 134, 157, 255);
        private WorkWithCoinsAndCout _secureCoin;
        private int[] _save = { 1, 2, 3, 4 };

        private void Awake() 
        {
            UpdateColorButton();
            _secureCoin.CoinUpdate();
            _txtCoinsInShop.text = _secureCoin.Coin.ToString();
            var selectBird = 0;
            for (int i = 1; i < _save.Length; i++) 
            {
                selectBird = i + 1;
                if (!PlayerPrefsSafe.HasKey("savesPay" + selectBird)) continue;

                var loadedData = PlayerPrefsSafe.GetInt("savesPay" + selectBird);
                if (loadedData == _save[i]) 
                {
                    _payBirdsText[i - 1].GetComponent<TextMeshProUGUI>().text = " ";
                    if (selectBird == 4)
                    {
                        _bird4.SetActive(false);
                        _bird4Payed.SetActive(true);
                    }
                }
            }
        }

        private void OnEnable()
        {
            Purchases.PayBirdEvent += PayBird;
        }

        private void OnDisable()
        {
            Purchases.PayBirdEvent -= PayBird;
        }

        public void OpenAndCloseShop(bool open) 
        {
            _shop.SetActive(open);
            _game.SetActive(!open);
            _secureCoin.CoinUpdate();
            _txtCoinsInMenu.text = _secureCoin.Coin.ToString();
        }

        public void PayBird(int bird)
        {
            if (bird < 1 || bird > 4)
                throw new ArgumentOutOfRangeException("Birds cannot be less than 1 or more than 4");
            
            var birdPrice = GetBirdPrice(bird);
            _secureCoin.CoinUpdate();
            var checkBirdPurchased = CheckBirdPurchased(bird);
            var coinValue = _secureCoin.Coin;
            if ((checkBirdPurchased == false) && (coinValue >= birdPrice)) 
            {
                BuyBird(bird);
                if (bird == 4)
                {
                    _bird4.SetActive(false);
                    _bird4Payed.SetActive(true);
                }
            } 
            else if (checkBirdPurchased) 
            {
                PlayerPrefsSafe.SetInt("saveselect", bird);
                UpdateColorButton(bird);
                _saves.DeactivateAllBirds();
                _saves.ChangeSelect(bird, true);
            }
        }

        private void BuyBird(int bird) 
        {
            if (bird < 1 || bird > 4)
                throw new ArgumentOutOfRangeException("Birds cannot be less than 1 or more than 4");
            
            var birdPrice = GetBirdPrice(bird);
            _secureCoin.TakeCoin(birdPrice);
            PlayerPrefsSafe.SetInt("savesPay" + bird, bird);
            PlayerPrefsSafe.SetInt("saveselect", bird);
            UpdateColorButton(bird);
            _saves.ChangeSelect(bird, true);
            _saves.DeactivateAllBirds();
            _saves.ChangeSelect(bird, true);
            if (bird > 1 && bird != 4) 
            {
                _payBirdsText[bird - 2].GetComponent<TextMeshProUGUI>().text = " ";
            }
            _txtCoinsInShop.text = _secureCoin.Coin.ToString();
        }

        private void UpdateColorButton() 
        {
            if (!PlayerPrefsSafe.HasKey("saveselect")) return;

            var saveSelect = PlayerPrefsSafe.GetInt("saveselect");
            _buttonsImages[saveSelect - 1].color = _greenColor;
            for (int i = 0; i < _buttonsImages.Length; i++) 
            {
                if (i != (saveSelect - 1))
                {
                    _buttonsImages[i].color = _redColor;
                }
            }
        }
        
        private void UpdateColorButton(int bird) 
        {
            if (!PlayerPrefsSafe.HasKey("saveselect")) return;
            
            foreach (var button in _buttonsImages)
            {
                button.color = _redColor;
            }
            _buttonsImages[bird - 1].color = _greenColor;
        }

        private bool CheckBirdPurchased(int value) 
        {
            if (!PlayerPrefsSafe.HasKey("savesPay" + value)) return false;

            var loadedData = PlayerPrefsSafe.GetInt("savesPay" + value);
            for (int i = 0; i < _save.Length; i++) 
            {
                if (loadedData == _save[i]) 
                {
                    return true;
                }
            }
            return false;
        }

        private int GetBirdPrice(int bird) 
        {
            if (bird <= 0 || bird > 4)
                throw new ArgumentOutOfRangeException("Birds cannot be less than 1 or more than 4");
            
            var mapBirdPrice = new Dictionary<int, int>
            {
                { 1, 0 },
                { 2, 20 },
                { 3, 50 },
                { 4, 0 }
            };
            return mapBirdPrice[bird];
        }
    } 
}

