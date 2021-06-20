using Scripts.Scriptables;
using UnityEngine;

namespace Scripts.Services
{
    public class AllMusic : MonoBehaviour
    {
        [SerializeField] private AllMusicData _allMusicData;
        
        private AudioSource _audioSource = null;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = _allMusicData.AllMainMusic[Random.Range(0, _allMusicData.AllMainMusic.Length)];
            _audioSource.Play();
        }

        private void Update()
        {
            if (_audioSource.isPlaying) 
                return;

            _audioSource.clip = _allMusicData.AllMainMusic[Random.Range(0, _allMusicData.AllMainMusic.Length)];
            _audioSource.Play();
        }
    }
}
