using System;
using Cysharp.Threading.Tasks;
using Scripts.Services;
using UnityEngine;

namespace Scripts.Player
{
    public class MainBird : MonoBehaviour
    {
        public static event Action OnCoinTake;
        public static event Action OnBirdCrashed;
        public static event Action<bool> ChangeCheckGameIsLose;
        public static event Action<MusicEvents.AllEvents> MusicEvent;
        
        [SerializeField] private float _returnSpeed = 1.5f;
        
        private RectTransform _transform = null;
        private AudioSource _audioSource = null;
        private Rigidbody2D _rigidbody2D = null;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _transform = GetComponent<RectTransform>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            Spawner.Instance.GameLose.onChanged += SetBirdTurnOnGravity;
            Detect.JumpEvent += JumpBird;
        }

        private void OnDisable()
        {
            Spawner.Instance.GameLose.onChanged -= SetBirdTurnOnGravity;
            Detect.JumpEvent -= JumpBird;
        }

        private void OnTriggerEnter2D(Collider2D enemy)
        {
            var tag = enemy.gameObject.tag;
            if (Spawner.Instance.GameLose.Value) 
                return;
            
            if (enemy.GetComponent<EnemyBird>() || enemy.CompareTag("Barrier"))
            {
                Spawner.Instance.GameLose.Value = true;
                OnBirdCrashed?.Invoke();
            }
            else if (enemy.GetComponent<MoveCoins>())
            {
                OnCoinTake?.Invoke();
                MusicEvent?.Invoke(MusicEvents.AllEvents.gameWin);
            }
        }

        private void SetBirdTurnOnGravity(bool newValue, bool oldValue)
        {
            if (!newValue)
            {
                _rigidbody2D.simulated = true;
            }
            else
            {
                _rigidbody2D.simulated = false;
                RefreshBird();
                ReturnTheBirdToPosition();
            }
        }

        private void JumpBird()
        {
            _audioSource.Play();
            _rigidbody2D.AddForce(_transform.up * 25);
        }

        private void RefreshBird()
        {
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }

        private async UniTask ReturnTheBirdToPosition()
        {
            _audioSource.volume = 0;
            while (Spawner.Instance.GameLose.Value)
            {
                var positionY = _transform.position.y;
                if (positionY > 0.2f)
                {
                    _transform.position -= new Vector3(0,(_transform.position.y * _returnSpeed * Time.deltaTime),0);
                }
                else if (positionY < -0.2f)
                {
                    _transform.position += new Vector3(0,(_transform.position.y * _returnSpeed * Time.deltaTime * -1),0);
                }
                if ((positionY < 0.2f) && (positionY > -0.2f))
                {
                    await UniTask.Delay(30);
                    ChangeCheckGameIsLose?.Invoke(true);
                    break;
                }
                await UniTask.Delay(10);
            }
        }
    }
}

