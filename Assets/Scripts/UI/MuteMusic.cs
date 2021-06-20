using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class MuteMusic : MonoBehaviour
    {
        [SerializeField] private AudioSource _music;
        
        private RectTransform _transform = null;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
        }

        public void OnMouseDown()
        {
            _transform.localScale = new Vector3(_transform.localScale.x + 0.1f, 1.1f, 0);
        }

        public void OnMouseUp()
        {
            var image = GetComponent<Image>();
            if (_music.volume != 0)
            {
                _music.volume = 0;
                image.sprite = Resources.Load<Sprite>("MusicOff_Simple_Icons_UI");
                _transform.localScale = new Vector2(1.3f, 0.9f);
            }
            else
            {
                _music.volume = 0.7f;
                image.sprite = Resources.Load<Sprite>("MusicOn_Simple_Icons_UI");
                _transform.localScale = new Vector2(0.9f, 0.9f);
            }
        }
    }
}
