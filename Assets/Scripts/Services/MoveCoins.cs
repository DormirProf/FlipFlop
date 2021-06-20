using Scripts.Player;
using UnityEngine;

namespace Scripts.Services
{
    public class MoveCoins : MonoBehaviour
    {
        [SerializeField] private float _movespeed;
        
        private Transform _transform = null;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void OnEnable()
        {
            MainBird.OnBirdCrashed += DestroyCoins;
        }

        private void OnDestroy()
        {
            MainBird.OnBirdCrashed -= DestroyCoins;
        }

        private void Update()
        {
            if (_transform.position.x < -4f) 
                DestroyCoins();
            _transform.position -= new Vector3(_movespeed * Time.deltaTime, 0, 0);
        }

        private void OnTriggerEnter2D(Collider2D enemy)
        {
            var tag = enemy.gameObject.tag;
            if (enemy.GetComponent<MainBird>()) 
                DestroyCoins();
        }

        private void DestroyCoins()
        {
            Destroy(this.gameObject);
        }
    }
}