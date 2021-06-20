using Scripts.Services;
using UnityEngine;

namespace Scripts.UI
{
    public class SetActivation : MonoBehaviour
    {
        private void Awake()
        {
            GameController.ChangeActivationEvent += SetActive;
        }

        private void SetActive(bool Active) => 
            gameObject.SetActive(Active);
    }
}
