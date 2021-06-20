using System;
using Scripts.Player;
using Scripts.Scriptables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Services
{
    public class MusicEvents : MonoBehaviour
    {
        [SerializeField] private AudioSource _mainMusicAudioSource;
        [SerializeField] private AllMusicData _allMusicData;
        
        private AudioSource _audioSource = null;
        private float _mainMusicValue;
        private bool _checkMusicActive;

        public enum AllEvents
        {
            gameWin,
            gameLose,
            coinTake
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            MainBird.MusicEvent += StartPlayMusic;
            GameController.MusicEvent += StartPlayMusic;
        }

        private void OnDisable()
        {
            MainBird.MusicEvent -= StartPlayMusic;
            GameController.MusicEvent -= StartPlayMusic;
        }

        private void StartPlayMusic(AllEvents value)
        {
            _audioSource.clip = value switch
            {
                AllEvents.gameWin => _allMusicData.MusicEventGameWin[
                    Random.Range(0, _allMusicData.MusicEventGameWin.Length)],
                AllEvents.gameLose => _allMusicData.MusicEventGameLose[
                    Random.Range(0, _allMusicData.MusicEventGameLose.Length)],
                _ => _allMusicData.MusicEventTekeCoin[0]
            };
            if (value != 0)
            {
                _mainMusicValue = _mainMusicAudioSource.volume;
                _mainMusicAudioSource.volume = 0;
                _checkMusicActive = true;
            }
            _audioSource.Play();
        }

        private void Update()
        {
            if (_audioSource.isPlaying || !_checkMusicActive) return;

            _checkMusicActive = false;
            _mainMusicAudioSource.volume = _mainMusicValue;
        }
    }
}
