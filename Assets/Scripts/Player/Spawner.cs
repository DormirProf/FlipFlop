using TMPro;
using Cysharp.Threading.Tasks;
using Scripts.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Player
{
    public class Spawner : MonoBehaviour
    {
        private static Spawner _instance;
        public static Spawner Instance => _instance;
        
        [SerializeField] private GameObject _allEnemy, _baseEnemy, _objectCoin;
        [SerializeField] private TextMeshProUGUI _txtCounter;
        [SerializeField] [Space] private AnimationCurve _acceleration;
        [SerializeField] [Range(0, 10)] private int _spawnSpeedMin, _spawnSpeedMax;
        [SerializeField] [Range(0, 30)] private int _spawnSpeedCoinsMin, _spawnSpeedCoinsMax;
        
        private WorkWithCoinsAndCout _secureCout;
        private EventValue<bool> _gameLose = new EventValue<bool>(true);
        private int _spawnerObjectsState, _spawnerCoinState, _addCountState;
        private float _accelerationSpeed;

        public EventValue<bool> GameLose => _gameLose;
        public int Record => _secureCout.Cout;

        private void Awake() 
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void OnEnable()
        {
            _gameLose.onChanged += StartSpawners;
        }

        private void OnDisable()
        {
            _gameLose.onChanged -= StartSpawners;
        }

        private void StartSpawners(bool newValue, bool oldValue) 
        {
            if (newValue) 
                return;

            _secureCout.CleanCout();
            _accelerationSpeed = 0;
            SpawnEnemy();
            SpawnCoin();
            ChangeCout();
        }

        private async UniTask ChangeCout() 
        {
            if (_addCountState == 0) 
            {
                _addCountState++;
                while (!_gameLose.Value) 
                {
                    _secureCout.AddCout();
                    _txtCounter.text = _secureCout.Cout.ToString();
                    await UniTask.Delay(2100);
                }
            }
            if (_gameLose.Value) 
                _addCountState--;
        }

        private async UniTask SpawnCoin() 
        {
            if (_spawnerCoinState == 0) 
            {
                _spawnerCoinState++;
                while (!_gameLose.Value) 
                {
                    await UniTask.Delay(Random.Range(_spawnSpeedCoinsMin * 1000, _spawnSpeedCoinsMax * 1000));
                    if (!_gameLose.Value) 
                    {
                        Instantiate(_objectCoin, new Vector2(4.2f, Random.Range(-3.8f, 4.7f)), Quaternion.identity);
                    }
                }
            }
            if (_gameLose.Value) 
                _spawnerCoinState--;
        }

        private async UniTask SpawnEnemy() 
        {
            var spawnSpeedMax = _spawnSpeedMax * 1000;
            if (_spawnerObjectsState == 0) 
            {
                _spawnerObjectsState++;
                while (!_gameLose.Value) 
                {
                    Instantiate(_baseEnemy, _allEnemy.transform, false);
                    if (spawnSpeedMax > _spawnSpeedMin * 1000) 
                    {
                        _accelerationSpeed += Time.deltaTime;
                        spawnSpeedMax -= System.Convert.ToInt32(_acceleration.Evaluate(_accelerationSpeed)) * 10;
                    }
                    await UniTask.Delay(Random.Range(_spawnSpeedMin * 1000, spawnSpeedMax));
                }
            }
            if (_gameLose.Value) 
                _spawnerObjectsState--;
        }
    }
}

