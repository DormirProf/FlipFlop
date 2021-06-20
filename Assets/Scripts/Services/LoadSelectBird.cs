using Scripts.Secure;
using UnityEngine;

namespace Scripts.Services
{
    public class LoadSelectBird : MonoBehaviour
    {
        [SerializeField] private GameObject[] _birds;
        
        private int _birdSelect;

        public GameObject[] Birds => _birds;
        public int BirdSelect => _birdSelect;

        public void ChangeSelect(int bird, bool set)
        {
            if (bird > 0) 
                _birds[bird - 1].SetActive(set);
            _birdSelect = bird;
        }

        public void LoadSave()
        {
            if (PlayerPrefsSafe.HasKey("saveselect"))
            {
                var saveSelect = PlayerPrefsSafe.GetInt("saveselect");
                _birdSelect = saveSelect - 1;
                _birds[_birdSelect].SetActive(true);
            }
            else
            {
                _birdSelect = 0;
                _birds[_birdSelect].SetActive(true);
            }
        }

        public void DeactivateAllBirds()
        {
            foreach (var bird in _birds)
            {
                bird.SetActive(false);
            }
        }
    }
}
