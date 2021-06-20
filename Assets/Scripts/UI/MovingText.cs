using UnityEngine;

namespace Scripts.UI
{
    public class MovingText : MonoBehaviour
    {
        [SerializeField] private float _minimum;
        [SerializeField] private float _maximum;
        
        private Transform _transform = null;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }
        
        private void FixedUpdate()
        {
            _transform.localScale = new Vector3(Mathf.PingPong(Time.time, _maximum - _minimum) + 
                                                _minimum, Mathf.PingPong(Time.time, _maximum - _minimum) + _minimum,0);   
        }
    }
}
