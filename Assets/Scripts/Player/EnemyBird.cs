using Cysharp.Threading.Tasks;
using Scripts.Scriptables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Player
{
    class EnemyBird : MonoBehaviour
    {
        [SerializeField] private BirdAcceleration _allEnemy;
        [SerializeField] private AllEnemyData _enemyData;
        
        private Transform _transform = null;
        private SpriteRenderer _spriteRenderer = null;
        private float _moveSpeed = 0;
        private bool _isAnimationActiv = true;
        private int _selectEnemy = 0;
        private Vector3 _targetPosition;
        
        private void Update() => _transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime);
        
        private void Start()
        {
            _transform = GetComponent<Transform>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _targetPosition = _transform.position - Vector3.left * 20;
            if (Spawner.Instance.GameLose.Value) 
                return;
            
            Spawner.Instance.GameLose.onChanged += DestroyEnemy;
            _transform.position = new Vector3(_transform.position.x, Random.Range(-3.8f, 4.7f), _transform.position.z);
            GetSelectBird();
            _transform.localScale = new Vector2(_enemyData.Bird[_selectEnemy].Scale, _enemyData.Bird[_selectEnemy].Scale);
            _spriteRenderer.sprite = _enemyData.Bird[_selectEnemy].Sprite;
            _moveSpeed = _enemyData.Bird[_selectEnemy].Speed + _allEnemy.Acceleration;
            AddCollider();
            StartAnimation();
            DestroyEnemyAfterOut();
        }

        private void GetSelectBird()
        {
            _selectEnemy = Random.Range(0, _enemyData.Bird.Count);
            if (_enemyData.BirdsLastSpawn == _selectEnemy)
            {
                _selectEnemy = Random.Range(0, _enemyData.Bird.Count);
                _enemyData.BirdsLastSpawn = _selectEnemy;
            }
            _enemyData.BirdsLastSpawn = _selectEnemy;
        }

        private void AddCollider()
        {
            if (_enemyData.Bird[_selectEnemy].ChoiceOfTheCollier == 0)
            {
                gameObject.AddComponent<BoxCollider2D>();
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else if (_enemyData.Bird[_selectEnemy].ChoiceOfTheCollier != 0)
            {
                gameObject.AddComponent<CircleCollider2D>();
                gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            }
        }

        private async UniTask StartAnimation()
        {
            var birds = _enemyData.Bird;
            var animationSpeed = 200;
            var check = true;
            var lengthAllSprites = birds[_selectEnemy].AllSprites.Length;
            var i = 0;
            if (lengthAllSprites > 2) animationSpeed = 150;
            while (_isAnimationActiv)
            {
                if (i < lengthAllSprites && check)
                {
                    i++;
                    if (_isAnimationActiv)
                    {
                        _spriteRenderer.sprite = birds[_selectEnemy].AllSprites[i];
                    }
                    else
                    {
                        break;
                    }
                    if (i == lengthAllSprites - 1)
                    {
                        check = false;
                    }
                    await UniTask.Delay(animationSpeed);
                }
                else if (i > 0 && !check)
                {
                    i--;
                    if (_isAnimationActiv)
                    {
                        _spriteRenderer.sprite = birds[_selectEnemy].AllSprites[i];
                    }
                    else
                    {
                        break;
                    }
                    if (i == 0)
                    {
                        check = true;
                    }
                    await UniTask.Delay(animationSpeed);
                }
            }
        }

        private async UniTask DestroyEnemyAfterOut()
        {
            while (_isAnimationActiv)
            {
                if (_transform.position.x < -4f && _isAnimationActiv)
                {
                    DestroyEnemy(true, false);
                    break;
                }
                await UniTask.Delay(2000);
            }
        }

        private void DestroyEnemy(bool newValue, bool oldValue)
        {
            if (!newValue) 
                return;
            
            Spawner.Instance.GameLose.onChanged -= DestroyEnemy;
            _isAnimationActiv = false;
            Destroy(this.gameObject);
        }
    }
}


