using UnityEngine;

namespace Scripts.Scriptables
{
    [CreateAssetMenu(menuName = "Data/AllMusicData")]
    public class AllMusicData : ScriptableObject
    {
        public AudioClip[] AllMainMusic;
        public AudioClip[] MusicEventGameWin;
        public AudioClip[] MusicEventGameLose;
        public AudioClip[] MusicEventTekeCoin;
    }
}

