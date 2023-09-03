using UnityEngine;

namespace Item
{
    public abstract class Pickable : MonoBehaviour
    {
        [SerializeField]protected GameObject model;
        public float timeToCook;
    }
}