using System;
using Health;
using Turret;
using UnityEngine;

namespace Grid
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private BaseTurret _turret;
        private Outline _outline;

        private void Awake()
        {
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
        }

        public void SelectTile(bool state = true)
        {
            _outline.enabled = state;
        }

        public void SetTurret(BaseTurret turret)
        {
            if (turret)
            {
                _turret = Instantiate(turret, transform.position, Quaternion.identity, transform);
            }
        }
        
        public void DestroyTurret()
        {
            if(_turret)
            {
                var turretHealth = _turret.GetComponent<EntityHealth>();
                turretHealth.ReceiveDamage(turretHealth.CurrentHealth);
            }
        }
        public bool IsAvailable()
        {
            return _turret == null;
        }
    }
}