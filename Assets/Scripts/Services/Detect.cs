using System;
using Scripts.Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Services
{
    public class Detect : MonoBehaviour, IPointerDownHandler
    {
        public static event Action JumpEvent;

        private bool _detectClickActivation = true;

        private void OnEnable()
        {
            MainBird.ChangeCheckGameIsLose += ChangeDetectClickActivation; 
        }

        private void OnDisable()
        {
            MainBird.ChangeCheckGameIsLose -= ChangeDetectClickActivation;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_detectClickActivation)
            {
                _detectClickActivation = false;
                Spawner.Instance.GameLose.Value = false;
                return;
            }
            JumpEvent?.Invoke();
        }
        
        public void OpenLeader() => 
            Social.ShowLeaderboardUI();
        private void ChangeDetectClickActivation(bool Check) => 
            _detectClickActivation = Check;
    }
}
