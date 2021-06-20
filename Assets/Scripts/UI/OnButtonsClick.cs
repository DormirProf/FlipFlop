using UnityEngine;

namespace Scripts.UI
{
    public class OnButtonsClick : MonoBehaviour
    {
        [SerializeField] private float _yMin, _yMax;
        [SerializeField] private float _xMin, _xMax;
        
        private Transform _transform = null;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void OnMouseDown()
        {
            _transform.localScale = new Vector3(_xMax, _yMax, 0);
        }

        private void OnMouseUp()
        {
            _transform.localScale = new Vector3(_yMin, _xMin, 0);
        }
    }
}
