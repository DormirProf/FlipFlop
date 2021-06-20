using UnityEngine;

namespace Scripts.Payes
{
    public class CloseShop : MonoBehaviour
    {
        private MainShop _mainShop;

        private void Awake()
        {
            _mainShop = GetComponent<MainShop>();
        }

        void FixedUpdate()
        {
            if (Application.platform != RuntimePlatform.Android) 
                return;
        
            if (Input.GetKey(KeyCode.Escape))
            {
                _mainShop.OpenAndCloseShop(false);
            }
        }
    }   
}
