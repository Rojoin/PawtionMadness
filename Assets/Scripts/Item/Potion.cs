using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class Potion : Pickable
    {
        [SerializeField] private Image imageIcon;
        private bool _isItemIconVisible = false;
        public BaseTurretSO _baseTurretSo;
        
        public bool IsItemIconVisible() => _isItemIconVisible;
        
        public void SetIconVisible(bool state = true)
        {
            _isItemIconVisible = state;
            imageIcon.sprite = _isItemIconVisible ? _baseTurretSo.sprite : null;
            imageIcon.enabled = state;
        }
    }
}