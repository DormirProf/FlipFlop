using Cysharp.Threading.Tasks;
using Scripts.Player;
using UnityEngine;

namespace Scripts.UI
{
    public class ScrollO : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        
        private RectTransform _transform = null;
        private float _speedСhange = 3f;
        private bool _isButtonsUp = true;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            _spawner.GameLose.onChanged += CheckPosition;
        }

        private void OnDisable()
        {
            _spawner.GameLose.onChanged -= CheckPosition;
        }

        private void CheckPosition(bool newValue, bool oldValue)
        {
            if (_isButtonsUp)
            {
                _isButtonsUp = false;
                StartScroll();
            }
            else
            {
                _isButtonsUp = true;
                StartScroll();
            }
        }

        private async UniTask StartScroll()
        {
            while (_transform.offsetMin.y < 0 && _isButtonsUp)
            {
                _transform.Translate(Vector3.up * _speedСhange * Time.deltaTime);
                await UniTask.Delay(10);
            }

            while (_transform.offsetMin.y > -270 && !_isButtonsUp)
            {
                _transform.Translate(Vector3.down * _speedСhange * Time.deltaTime);
                await UniTask.Delay(10);
            }
        }
    }
}
