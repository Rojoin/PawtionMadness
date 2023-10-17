using Turret;
using UnityEngine;

namespace Grid
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private BaseTurret _turret;
        [SerializeField] private Transform _turretPosition;
        [SerializeField] private Transform model;
     
        
        public void ChangeModel(Color color = default)
        {
            int randomPos = Random.Range(0, 4);
            switch (randomPos)
            {
                case 0:
                    break;
                case 1:
                    model.eulerAngles = new Vector3(90, 90, 0);
                    break;
                case 2:
                    model.eulerAngles = new Vector3(90, 180, 0);
                    break;
                case 3:
                    model.eulerAngles = new Vector3(90, 270, 0);
                    break;
            }

            model.GetComponent<SpriteRenderer>().color = color;

        }
        public void SetTurret(BaseTurret turret)
        {
            if (turret)
            {
                _turret = Instantiate(turret, _turretPosition.position, Quaternion.identity, transform);
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