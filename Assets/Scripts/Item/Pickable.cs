using UnityEngine;

namespace Item
{
    public abstract class Pickable : MonoBehaviour
    {
        [SerializeField]protected GameObject model;

        public abstract void TransferData(Pickable pick);
    }
}