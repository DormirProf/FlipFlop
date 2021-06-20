using Scripts.Player;
using UnityEngine;

namespace Scripts.UI
{
    public class MoveBG : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _acceleration;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private float _speed = 1;
        
        private Transform _transform = null;
        private float _currentSpeed;
        private float _posX;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            if (_transform.position.x < -20)
            {
                _posX = _transform.position.x + 20 - Time.deltaTime;
                _transform.position = new Vector2(20 + _posX, 0);
            }

            if (_spawner.GameLose.Value)
            {
                _currentSpeed = 1;
                _speed = 1;
            }
            else
            {
                _currentSpeed += Time.deltaTime;
                _speed = _acceleration.Evaluate(_currentSpeed);
            }

            _transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
    }
}
