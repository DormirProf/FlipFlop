using UnityEngine;

namespace Scripts.Player
{
    public class BirdAcceleration : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _acceleration;
        [SerializeField] private Spawner _spawner;
        
        private float _currentSpeed;

        public float Acceleration => _acceleration.Evaluate(_currentSpeed);

        private void Update()
        {
            if(_spawner.GameLose.Value == false)
            {
                _currentSpeed += Time.deltaTime;   
            }
            else
            {
                _currentSpeed = 0;
            }
        }
    }   
}
