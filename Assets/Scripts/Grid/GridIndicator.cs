using System;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class GridIndicator : MonoBehaviour
    {
        [SerializeField] private GameObject[] posibleTurrets;
        private GameObject currentTurret;
        private Dictionary<string, GameObject> _turrets = new Dictionary<string, GameObject>();

        private void Awake()
        {
            foreach (GameObject turret in posibleTurrets)
            {
                _turrets.Add(turret.name,turret);
            }
        }

        public void SetCurrentTurret(string value)
        {
            if(currentTurret != null)
            {
                currentTurret.SetActive(false);
            }

            if (_turrets.TryGetValue(value,out GameObject Turret))
            {
                Turret.SetActive(true);
                currentTurret = Turret;
            }
        }
    }
}