using Turret;
using UnityEngine;

namespace Grid
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private BaseTurret _turret;
        
        public void SetTurret(BaseTurret turret)
        {
            if (turret)
            {
                _turret = Instantiate(turret, transform.position, Quaternion.identity, transform);
                _turret.transform.Rotate(0,90,0);
            }
        }
        
        public void DestroyTurret()
        {
            if(_turret)
            {
                _turret.ReceiveDamage(_turret.CurrentHealth);
            }
        }
        public bool IsAvailable()
        {
            return _turret == null;
        }
    }
}