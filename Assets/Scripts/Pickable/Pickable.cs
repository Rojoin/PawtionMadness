using UnityEngine;

namespace Pickable
{
    public abstract class Pickable : MonoBehaviour
    {
        [SerializeField]protected GameObject model;
        public float timeToCook;
    }
}